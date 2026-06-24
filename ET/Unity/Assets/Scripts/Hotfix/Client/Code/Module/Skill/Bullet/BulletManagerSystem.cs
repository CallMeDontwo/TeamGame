using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(BulletManagerComponent))]
    [FriendOf(typeof(BulletManagerComponent))]
    public static partial class BulletManagerSystem
    {
        [EntitySystem]
        private static void Awake(this BulletManagerComponent self)
        {
            self.Bullets = new List<BulletRuntimeData>(64);
            self.FreeSlots = new Queue<int>(64);
            self.BulletGrid = new Dictionary<int, List<int>>(128);
            self.EnemyGrid = new Dictionary<int, List<int>>(128);
            self.EnemiesSnapshot = new List<EnemyCollisionSnapshot>(32);
            self.HitsThisFrame = new List<HitResult>(16);
            self.GridCellSize = 1f;
            self.LastUpdateTime = TimeInfo.Instance.ClientFrameTime();
        }

        [EntitySystem]
        private static void Destroy(this BulletManagerComponent self)
        {
            self.Bullets?.Clear();
            self.FreeSlots?.Clear();
            self.BulletGrid?.Clear();
            self.EnemyGrid?.Clear();
            self.EnemiesSnapshot?.Clear();
            self.HitsThisFrame?.Clear();
        }

        // ═══════════════════════════════════════════════════
        //  固定步长主循环
        // ═══════════════════════════════════════════════════

        [EntitySystem]
        private static void Update(this BulletManagerComponent self)
        {
            if (self.Bullets.Count == 0) return;

            long now = TimeInfo.Instance.ClientFrameTime();
            float dt = (now - self.LastUpdateTime) / 1000f;
            self.LastUpdateTime = now;
            if (dt <= 0f || dt > 0.1f) dt = 1f / 60f;

            self.TimeAccumulator += dt;

            // 固定步长推进（支持变帧率，CCD 防穿透）
            while (self.TimeAccumulator >= ConstValue.FixedDt)
            {
                BuildEnemySnapshot(self);
                UpdateAllBullets(self, ConstValue.FixedDt);
                self.TimeAccumulator -= ConstValue.FixedDt;
            }

            // 每帧执行一次碰撞检测（含步长内所有移动）
            BuildSpatialGrid(self);
            CheckCollisions(self);
            ProcessHits(self);
            CleanupDeadBullets(self);
        }

        // ═══════════════════════════════════════════════════
        //  对外接口：注册子弹
        // ═══════════════════════════════════════════════════

            /// <summary>注册一颗子弹到管理器的集中循环中</summary>
        public static void Register(this BulletManagerComponent self, BulletRuntimeData data)
        {
            data.isActive = 1;
            if (self.FreeSlots.Count > 0)
            {
                int slot = self.FreeSlots.Dequeue();
                self.Bullets[slot] = data;
                //Log.Debug($"[Bullet] Register slot={slot} unitId={data.bulletUnitId} pos=({data.position.x:F2},{data.position.y:F2}) speed={math.length(data.velocity):F1} maxDist={data.maxDist:F1}");
            }
            else
            {
                self.Bullets.Add(data);
                //Log.Debug($"[Bullet] Register new idx={self.Bullets.Count-1} unitId={data.bulletUnitId} pos=({data.position.x:F2},{data.position.y:F2}) speed={math.length(data.velocity):F1} maxDist={data.maxDist:F1}");
            }
        }

        // ═══════════════════════════════════════════════════
        //  Step 1：收集敌人位置快照
        // ═══════════════════════════════════════════════════

        private static void BuildEnemySnapshot(BulletManagerComponent self)
        {
            self.EnemiesSnapshot.Clear();
            Scene scene = self.Scene();
            if (scene == null) return;

            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return;

            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit unit) continue;
                if (unit.IsDisposed) continue;

                var identity = unit.GetComponent<IdentityComponent>();
                if (identity == null) continue;

                float radius = GetUnitCollisionRadius(unit, identity.UnitType);

                self.EnemiesSnapshot.Add(new EnemyCollisionSnapshot
                {
                    center  = new float2(unit.Position.x, unit.Position.y + radius),
                    radius  = radius,
                    unitId  = unit.Id,
                    unitType = (byte)identity.UnitType,
                    height  = identity.Height,
                });
            }
        }

        /// <summary>获取单位碰撞半径（int/100 → float）</summary>
        private static float GetUnitCollisionRadius(Unit unit, UnitType unitType)
        {
            int radius = unitType switch
            {
                UnitType.Hero    => HeroConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius,
                UnitType.Monster => MonsterConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius,
                _                => 40,
            };
            return radius / 100f;
        }

        // ═══════════════════════════════════════════════════
        //  Step 2：推进所有子弹（单线程 for 循环）
        // ═══════════════════════════════════════════════════

        private static void UpdateAllBullets(BulletManagerComponent self, float dt)
        {
            for (int i = 0; i < self.Bullets.Count; i++)
            {
                var b = self.Bullets[i];
                if (b.isActive == 0) continue;

                b.prevPosition = b.position;

                // 追踪弹：每帧重算朝向
                if (b.isHoming == 1)
                {
                    var targetUnit = GetUnitById(self, b.targetId);
                    if (targetUnit != null && !targetUnit.IsDisposed)
                    {
                        float2 toTarget = new float2(targetUnit.Position.x, targetUnit.Position.y) - b.position;
                        float speed = math.length(b.velocity);
                        if (math.lengthsq(toTarget) > 1e-8f)
                            b.velocity = math.normalize(toTarget) * speed;
                    }
                }

                if (b.flightType == (byte)BulletFlightType.Parabolic && b.flightTime > 0f)
                {
                    // 抛物线：解析解（消除数值积分误差，落点严格 = spawnPos.y）
                    // pos.x = spawn.x + vx * t
                    // pos.y = spawn.y + vy0 * t - 0.5 * g * t²
                    b.elapsed += dt;
                    if (b.elapsed >= b.flightTime)
                    {
                        // 钳制到落点（水平射程 maxDist，垂直回到 spawn.y）
                        b.elapsed = b.flightTime;
                        b.position = new float2(
                            b.spawnPos.x + b.vx * b.flightTime,
                            b.spawnPos.y);
                        b.traveledDist = b.maxDist;
                        b.isActive = 0;
                    }
                    else
                    {
                        b.position = new float2(
                            b.spawnPos.x + b.vx * b.elapsed,
                            b.spawnPos.y + b.vy0 * b.elapsed - 0.5f * b.gravity * b.elapsed * b.elapsed);
                        // 抛物线只计水平位移
                        b.traveledDist += math.abs(b.vx) * dt;
                        if (b.maxDist > 0 && b.traveledDist >= b.maxDist)
                            b.isActive = 0;
                    }
                }
                else
                {
                    // 直线 / 追踪：欧拉积分
                    b.position += b.velocity * dt;
                    b.traveledDist += math.length(b.velocity) * dt;
                    if (b.maxDist > 0 && b.traveledDist >= b.maxDist)
                        b.isActive = 0;
                }

                // 同步位置到 Unit（驱动 View 层移动 GameObject）
                SyncBulletPosition(self, b);

                self.Bullets[i] = b;
            }
        }

        // ═══════════════════════════════════════════════════
        //  Step 3：构建空间哈希网格
        // ═══════════════════════════════════════════════════

        private static void BuildSpatialGrid(BulletManagerComponent self)
        {
            float cellSize = self.GridCellSize;

            // 清空网格（复用内部 List<int>，不重建）
            foreach (var kv in self.BulletGrid)
                kv.Value.Clear();
            self.BulletGrid.Clear();

            foreach (var kv in self.EnemyGrid)
                kv.Value.Clear();
            self.EnemyGrid.Clear();

            // 子弹入网格
            for (int i = 0; i < self.Bullets.Count; i++)
            {
                if (self.Bullets[i].isActive == 0) continue;
                int key = GetCellKey(self.Bullets[i].position, cellSize);
                if (!self.BulletGrid.TryGetValue(key, out var list))
                {
                    list = new List<int>(4);
                    self.BulletGrid[key] = list;
                }
                list.Add(i);
            }

            // 敌人入网格
            for (int i = 0; i < self.EnemiesSnapshot.Count; i++)
            {
                int key = GetCellKey(self.EnemiesSnapshot[i].center, cellSize);
                if (!self.EnemyGrid.TryGetValue(key, out var list))
                {
                    list = new List<int>(4);
                    self.EnemyGrid[key] = list;
                }
                list.Add(i);
            }
        }

        /// <summary>世界坐标 → cell key（低 16 位=x，高 16 位=y）</summary>
        private static int GetCellKey(float2 pos, float cellSize)
        {
            int cx = (int)math.floor(pos.x / cellSize);
            int cy = (int)math.floor(pos.y / cellSize);
            return (cx & 0xFFFF) | ((cy & 0xFFFF) << 16);
        }

        // ═══════════════════════════════════════════════════
        //  Step 4：碰撞检测（网格加速 + CCD）
        // ═══════════════════════════════════════════════════

        private static void CheckCollisions(BulletManagerComponent self)
        {
            self.HitsThisFrame.Clear();
            float cellSize = self.GridCellSize;

            for (int bi = 0; bi < self.Bullets.Count; bi++)
            {
                var bullet = self.Bullets[bi];
                if (bullet.isActive == 0) continue;

                int cellKey = GetCellKey(bullet.position, cellSize);
                short cx = (short)(cellKey & 0xFFFF);
                short cy = (short)(cellKey >> 16);

                bool hit = false;

                for (int dy = -1; dy <= 1 && !hit; dy++)
                for (int dx = -1; dx <= 1 && !hit; dx++)
                {
                    int neighborKey = ((cx + dx) & 0xFFFF) | (((cy + dy) & 0xFFFF) << 16);

                    if (!self.EnemyGrid.TryGetValue(neighborKey, out var enemyIndices))
                        continue;

                    foreach (int ei in enemyIndices)
                    {
                        var enemy = self.EnemiesSnapshot[ei];

                        // 跳过同阵营（友方子弹不打友方）
                        long casterId = bullet.casterId;
                        if (IsSameSide(self, casterId, enemy.unitId, enemy.unitType))
                            continue;

                        // 高度不匹配：子弹高度层与敌人高度层不同则跳过
                        if (bullet.height != enemy.height)
                            continue;

                        if (BulletHitCheck(
                            bullet.position, bullet.prevPosition,
                            enemy.center, enemy.radius, bullet.radius))
                        {
                            self.HitsThisFrame.Add(new HitResult
                            {
                                bulletIndex = bi,
                                targetId    = enemy.unitId,
                                damage      = bullet.damage,
                            });
                            hit = true;
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>CCD 命中检测：静态圆-圆 + 线段扫描（防穿透）</summary>
        private static bool BulletHitCheck(
            float2 bulletPos, float2 prevBulletPos,
            float2 targetCenter, float targetRadius, float bulletRadius)
        {
            float combined = targetRadius + bulletRadius;
            float combinedSq = combined * combined;

            // 阶段 1：静态圆-圆
            if (math.distancesq(bulletPos, targetCenter) <= combinedSq)
                return true;

            // 阶段 2：线段-圆 CCD（高速弹不穿透）
            float2 move = bulletPos - prevBulletPos;
            float moveLenSq = math.lengthsq(move);
            if (moveLenSq < 1e-8f) return false;

            float t = math.saturate(
                math.dot(targetCenter - prevBulletPos, move) / moveLenSq);
            float2 closest = prevBulletPos + move * t;

            return math.distancesq(closest, targetCenter) <= combinedSq;
        }

        /// <summary>判断子弹发射者和目标是否同阵营（跳过友方）</summary>
        private static bool IsSameSide(BulletManagerComponent self, long casterId, long targetUnitId, byte targetUnitType)
        {
            // 发射者是英雄(UnitType=1)，目标是怪物(UnitType=2)：敌对，继续
            // 发射者是怪物(UnitType=2)，目标是英雄(UnitType=1)：敌对，继续
            // 其他同类型：跳过
            if (casterId == 0) return false;

            var casterUnit = GetUnitById(self, casterId);
            if (casterUnit == null || casterUnit.IsDisposed) return false;

            var casterIdComp = casterUnit.GetComponent<IdentityComponent>();
            if (casterIdComp == null) return false;

            int casterType = (int)casterIdComp.UnitType;
            int targetType = targetUnitType;

            // Hero(1) vs Monster(2) → 敌对
            if ((casterType == 1 && targetType == 2) || (casterType == 2 && targetType == 1))
                return false;

            return true; // 同阵营，跳过
        }

        // ═══════════════════════════════════════════════════
        //  Step 5：处理命中结果
        // ═══════════════════════════════════════════════════

        private static void ProcessHits(BulletManagerComponent self)
        {
            foreach (var hit in self.HitsThisFrame)
            {
                var bullet = self.Bullets[hit.bulletIndex];
                if (bullet.isActive == 0) continue;

                // 调用伤害逻辑
                var targetUnit = GetUnitById(self, hit.targetId);
                ApplyDamage(hit.targetId, hit.damage, targetUnit);

                // 弹射处理
                if (bullet.bulletType == 1 && bullet.ricochetRemain > 0)
                {
                    TryRicochet(self, hit.bulletIndex, hit.targetId);
                }
                else
                {
                    // 普通弹：标记失活
                    bullet.isActive = 0;
                    self.Bullets[hit.bulletIndex] = bullet;
                }
            }
        }

        /// <summary>应用子弹伤害到目标</summary>
        private static void ApplyDamage(long targetUnitId, int damage, Unit targetUnit)
        {
            if (targetUnit == null || targetUnit.IsDisposed || damage <= 0) return;
            var tn = targetUnit.GetComponent<NumericComponent>();
            if (tn == null) return;
            tn.TakeDamage(damage);
        }

        /// <summary>弹射：命中后跳向下一个最近敌人</summary>
        private static void TryRicochet(BulletManagerComponent self, int bulletIndex, long hitTargetId)
        {
            var bullet = self.Bullets[bulletIndex];
            bullet.ricochetRemain--;

            // 在弹射半径内找下一个敌人
            long nextTargetId = FindNextRicochetTarget(self, bullet.position, hitTargetId, bullet.ricochetRadius, bullet.casterId, bullet.height);
            if (nextTargetId != 0)
            {
                bullet.targetId = nextTargetId;
                // 重算朝向
                var nextUnit = GetUnitById(self, nextTargetId);
                if (nextUnit != null && !nextUnit.IsDisposed)
                {
                    float2 toTarget = new float2(nextUnit.Position.x, nextUnit.Position.y) - bullet.position;
                    float speed = math.length(bullet.velocity);
                    if (math.lengthsq(toTarget) > 1e-8f)
                        bullet.velocity = math.normalize(toTarget) * speed;
                }
            }
            else
            {
                // 无下一个目标：销毁
                bullet.isActive = 0;
            }

            self.Bullets[bulletIndex] = bullet;
        }

        /// <summary>在弹射半径内找最近的敌人</summary>
        private static long FindNextRicochetTarget(
            BulletManagerComponent self, float2 fromPos, long hitTargetId, float radius, long casterId, int bulletHeight)
        {
            long nearestId = 0;
            float nearestDistSq = float.MaxValue;
            float radiusSq = radius * radius;

            for (int i = 0; i < self.EnemiesSnapshot.Count; i++)
            {
                var enemy = self.EnemiesSnapshot[i];
                if (enemy.unitId == hitTargetId) continue;

                float distSq = math.distancesq(fromPos, enemy.center);
                if (distSq <= radiusSq && distSq < nearestDistSq)
                {
                    // 验证不是同阵营 且 高度匹配
                    if (!IsSameSide(self, casterId, enemy.unitId, enemy.unitType)
                        && enemy.height == bulletHeight)
                    {
                        nearestDistSq = distSq;
                        nearestId = enemy.unitId;
                    }
                }
            }

            return nearestId;
        }

        // ═══════════════════════════════════════════════════
        //  Step 6：清理超出距离/已命中的子弹
        // ═══════════════════════════════════════════════════

        private static void CleanupDeadBullets(BulletManagerComponent self)
        {
            for (int i = 0; i < self.Bullets.Count; i++)
            {
                var b = self.Bullets[i];
                if (b.isActive == 0 && b.bulletUnitId != 0)
                {
                    // 销毁 ET Unit → 连带 View GameObject
                    var bulletUnit = GetUnitById(self, b.bulletUnitId);
                    bulletUnit?.Dispose();

                    // 标记为空闲槽位
                    self.Bullets[i] = default;
                    self.FreeSlots.Enqueue(i);
                }
            }
        }

        // ═══════════════════════════════════════════════════
        //  辅助方法
        // ═══════════════════════════════════════════════════

        /// <summary>通过 EntityId 从 UnitManager 获取 Unit</summary>
        private static Unit GetUnitById(BulletManagerComponent self, long unitId)
        {
            if (unitId == 0) return null;
            Scene scene = self.Scene();
            var unitManager = scene.GetComponent<UnitManager>();
            unitManager.Children.TryGetValue(unitId, out Entity entity);
            return entity as Unit;
        }

        /// <summary>同步子弹运行时位置到对应 Unit（驱动 View 层移动 GameObject）</summary>
        private static void SyncBulletPosition(BulletManagerComponent self, BulletRuntimeData b)
        {
            if (b.bulletUnitId == 0) return;
            var bulletUnit = GetUnitById(self, b.bulletUnitId);
            if (bulletUnit == null || bulletUnit.IsDisposed) return;
            // SetPositon(true) 发布 ChangePosition 事件 → View 层移动 GameObject
            float3 pos = new float3(b.position.x, b.position.y, 0);
            bulletUnit.SetPositon(pos, true);
        }
    }
}
