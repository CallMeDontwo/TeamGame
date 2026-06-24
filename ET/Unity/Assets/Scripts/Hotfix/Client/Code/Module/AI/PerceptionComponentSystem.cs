using Unity.Mathematics;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(PerceptionComponent))]
    [FriendOf(typeof(PerceptionComponent))]
    public static partial class PerceptionComponentSystem
    {
        [EntitySystem]
        private static void Awake(this PerceptionComponent self)
        {
            self.VisibleTargets.Clear();
            self.PrimaryTargetId = 0;
            self.LastScanTime = 0;
        }

        [EntitySystem]
        private static void Destroy(this PerceptionComponent self)
        {
            self.VisibleTargets.Clear();
            self.PrimaryTargetId = 0;
        }

        /// <summary>
        /// 初始化感知参数（从 AIConfig 同步）
        /// </summary>
        public static void InitFromConfig(this PerceptionComponent self, int sightRange)
        {
            self.SightRange = sightRange;
        }

        /// <summary>
        /// 扫描周围可见敌人，更新 VisibleTargets 和 PrimaryTarget
        /// 扫描逻辑：遍历场景中所有 Unit，筛选敌对类型 + 距离判断
        /// </summary>
        public static void ScanSurrounding(this PerceptionComponent self)
        {
            var unit = self.GetParent<Unit>();
            long now = TimeInfo.Instance.ClientFrameTime();

            // 控制扫描频率
            if (now - self.LastScanTime < self.ScanInterval)
                return;
            self.LastScanTime = now;

            self.VisibleTargets.Clear();
            self.PrimaryTargetId = 0;

            // 从当前 Scene 获取所有 Unit
            Scene scene = unit.Scene();
            if (scene == null) return;

            UnitManager unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return;

            float3 unitPos = unit.Position;
            float sightRange = self.SightRange / 100f;
            float sightRangeSq = sightRange * sightRange;

            // 获取当前 Unit 的类型
            var selfIdentity = unit.GetComponent<IdentityComponent>();
            UnitType selfUnitType = selfIdentity?.UnitType ?? UnitType.None;

            long nearestId = 0;
            float nearestDistSq = float.MaxValue;

            // 优先选未锁定目标
            long nearestUnlockedId = 0;
            float nearestUnlockedDistSq = float.MaxValue;

            // 英雄专属：按离城墙距离（x 负方向）排序
            bool isHero = selfUnitType == UnitType.Hero;
            long nearestLeftId = 0;
            float nearestLeftX = float.MaxValue;
            long nearestUnlockedLeftId = 0;
            float nearestUnlockedLeftX = float.MaxValue;

            // 遍历场景中所有 Unit（通过 UnitManager 的 Children）
            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit other) continue;
                if (other.Id == unit.Id) continue;

                // 跳过已死亡单位
                var otherState = other.GetComponent<StateComponent>();
                if (otherState == null || !otherState.IsAlive()) continue;

                // 判断是否为敌对目标
                var otherIdentity = other.GetComponent<IdentityComponent>();
                if (otherIdentity == null) continue;

                if (!IsEnemy(selfUnitType, otherIdentity.UnitType)) continue;

                // 高度过滤：攻击者 ReachHeight 不够则不可见/不可选为目标
                var selfAtk = unit.GetComponent<AttackComponent>();
                if (selfAtk != null)
                {
                    int heightDiff = math.abs(selfIdentity.Height - otherIdentity.Height);
                    if (heightDiff > selfAtk.ReachHeight)
                        continue;
                }

                // 距离检测
                float3 diff = other.Position - unitPos;
                float distSq = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (distSq <= sightRangeSq)
                {
                    self.VisibleTargets.Add(other.Id);

                    // 记录最近的目标（兜底）
                    if (distSq < nearestDistSq)
                    {
                        nearestDistSq = distSq;
                        nearestId = other.Id;
                    }

                    // 优先记录最近的未锁定目标（只排除被友方锁定的）
                    if (!unitManager.IsLockedByFriend(other.Id, selfUnitType, unit.Id) && distSq < nearestUnlockedDistSq)
                    {
                        nearestUnlockedDistSq = distSq;
                        nearestUnlockedId = other.Id;
                    }

                    // 英雄：按离城墙最近（x 最小）
                    if (isHero)
                    {
                        float otherX = other.Position.x;
                        if (otherX < nearestLeftX)
                        {
                            nearestLeftX = otherX;
                            nearestLeftId = other.Id;
                        }
                        if (!unitManager.IsLockedByFriend(other.Id, selfUnitType, unit.Id) && otherX < nearestUnlockedLeftX)
                        {
                            nearestUnlockedLeftX = otherX;
                            nearestUnlockedLeftId = other.Id;
                        }
                    }
                }
            }

            // 选择主目标
            // 自己是否被锁定（反击：只在没有锁定目标时才生效）
            long selfAiTargetId = unit.GetComponent<AIComponent>()?.LockedTargetId ?? 0;
            long myLockerId = 0;
            if (selfAiTargetId == 0)
                myLockerId = unitManager.GetLockerOf(unit.Id);

            if (isHero)
            {
                // 英雄：未锁定中 x 最小 > 反击 > x 最小兜底
                if (nearestUnlockedLeftId > 0)
                    self.PrimaryTargetId = nearestUnlockedLeftId;
                else if (myLockerId != 0 && self.VisibleTargets.Contains(myLockerId))
                    self.PrimaryTargetId = myLockerId;
                else
                    self.PrimaryTargetId = nearestLeftId;

                // 调试：排查"未锁定为空"的原因
                var lockInfo = new System.Text.StringBuilder();
                lockInfo.Append("[Perception] Hero unitId=").Append(unit.Id)
                    .Append(" primary=").Append(self.PrimaryTargetId)
                    .Append(" nearestUnlock(ltX=").Append(nearestUnlockedLeftX).Append(",id=").Append(nearestUnlockedLeftId)
                    .Append(") nearest(ltX=").Append(nearestLeftX).Append(",id=").Append(nearestLeftId)
                    .Append(") myLocker=").Append(myLockerId)
                    .Append(" visCnt=").Append(self.VisibleTargets.Count)
                    .Append(" | locks: ");
                foreach (var kv in unitManager.LockedTargets)
                    lockInfo.Append(kv.Key).Append("<-").Append(kv.Value).Append(" ");
                Log.Debug(lockInfo.ToString());
            }
            else
            {
                // 怪物：反击 > 未锁定中最近 > 最近兜底
                if (myLockerId != 0 && self.VisibleTargets.Contains(myLockerId))
                    self.PrimaryTargetId = myLockerId;
                else if (nearestUnlockedId > 0)
                    self.PrimaryTargetId = nearestUnlockedId;
                else
                    self.PrimaryTargetId = nearestId;
            }
        }

        /// <summary>判断两个 UnitType 是否敌对（双向判定）</summary>
        private static bool IsEnemy(UnitType selfType, UnitType otherType)
        {
            return (selfType == UnitType.Monster && otherType == UnitType.Hero)
                || (selfType == UnitType.Hero && otherType == UnitType.Monster);
        }
    }
}
