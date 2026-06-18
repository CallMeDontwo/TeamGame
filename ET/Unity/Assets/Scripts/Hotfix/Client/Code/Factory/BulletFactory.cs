using Unity.Mathematics;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹工厂 — 在 UnitManager 下创建子弹 Unit 实体
    /// 创建后注册到 BulletManagerComponent 的集中循环中（不再启动独立协程）
    /// </summary>
    public static class BulletFactory
    {
        /// <summary>
        /// 创建一颗子弹并注册到集中管理器
        /// </summary>
        public static async ETTask CreateBullet(Unit caster, int bulletConfigId)
        {
            Log.Debug($"[BulletFactory] CreateBullet called casterId={caster?.Id} configId={bulletConfigId}");
            if (caster == null || caster.IsDisposed) return;

            var bulletCfg = BulletConfigCategory.Instance.Get(bulletConfigId);
            Scene scene = caster.Scene();
            var unitManager = scene.GetComponent<UnitManager>();
            var bulletManager = scene.GetComponent<BulletManagerComponent>();
            if (unitManager == null) { Log.Error("[BulletFactory] unitManager is null"); return; }
            if (bulletManager == null) { Log.Error("[BulletFactory] bulletManager is null"); return; }

            // 确定目标
            Unit target = null;
            if (bulletCfg.TargetFinderId > 0)
            {
                var finderCfg = TargetFinderConfigCategory.Instance.Get(bulletCfg.TargetFinderId);
                target = TargetFinder.Find(caster, finderCfg);
            }
            else
            {
                var castComp = caster.GetComponent<SkillCastComponent>();
                target = castComp?.Target;
            }

            // 计算初始飞行方向
            float3 direction;
            if (target != null && !target.IsDisposed)
            {
                float3 diff = target.Position - caster.Position;
                direction = math.normalizesafe(diff, new float3(1, 0, 0));
            }
            else
            {
                float3 forward = math.forward(caster.Rotation);
                direction = new float3(forward.x, 0, 0);
                if (math.abs(direction.x) < 0.001f)
                    direction = new float3(1, 0, 0);
            }

            // 创建子弹 Unit
            var bulletUnit = unitManager.AddChild<Unit, int>(0);
            float3 spawnPos = caster.Position;

            // 计算碰撞半径（默认 0.5f）
            float radius = GetBulletRadius(bulletCfg);

            // 挂载子弹组件（仅存标识+配置引用）
            var bc = bulletUnit.AddComponent<BulletComponent>();
            bc.BulletConfigId = bulletConfigId;
            bc.Caster = caster;
            bc.PrefabPath = bulletCfg.PrefabPath;

            bulletUnit.SetPositon(spawnPos, false);

            // 通知视图层创建 GameObject
            await EventSystem.Instance.PublishAsync(scene, new AfterBulletCreate
            {
                BulletUnit = bulletUnit,
            });

            // 构建运行时数据
            float speed = bulletCfg.Speed;
            float maxDist = bulletCfg.MaxDistance;
            float gravity = 0f;    // 非抛物线不用

            float2 vel;
            if (bulletCfg.FlightType == 2)
            {
                // 抛物线：FlightValue[0]=向上速度(int/100), FlightValue[1]=角度(度)
                float vy = (bulletCfg.FlightValue != null && bulletCfg.FlightValue.Length > 0)
                    ? bulletCfg.FlightValue[0] / 100f : 1f;
                float angleDeg = (bulletCfg.FlightValue != null && bulletCfg.FlightValue.Length > 1)
                    ? bulletCfg.FlightValue[1] : 45f;
                float angleRad = angleDeg * math.PI / 180f;
                float vx = vy / math.tan(angleRad);

                vel = new float2(direction.x * vx, vy);

                // 重力从 vx, vy, maxDist 反推
                float flightTime = maxDist / vx;
                gravity = 2f * vy / flightTime;
            }
            else
            {
                vel = new float2(direction.x * speed, 0f);
            }

            // 弹射参数：BulletTypeValue[0]=弹射次数, [1]=弹射半径(int/100)
            int ricochetCount = 0;
            float ricochetRadius = 0f;
            if (bulletCfg.BulletType == 2 && bulletCfg.BulletTypeValue != null && bulletCfg.BulletTypeValue.Length >= 2)
            {
                ricochetCount  = bulletCfg.BulletTypeValue[0];
                ricochetRadius = bulletCfg.BulletTypeValue[1] / 100f;
            }

            var runtime = new BulletRuntimeData
            {
                position        = new float2(spawnPos.x, spawnPos.y),
                prevPosition    = new float2(spawnPos.x, spawnPos.y),
                velocity        = vel,
                radius          = radius,
                traveledDist    = 0f,
                maxDist         = maxDist,
                gravity         = gravity,
                casterId        = caster.Id,
                targetId        = target?.Id ?? 0,
                damage          = bulletCfg.Damage,
                ricochetRemain  = ricochetCount,
                ricochetRadius  = ricochetRadius,
                bulletUnitId    = bulletUnit.Id,
                flightType      = (byte)bulletCfg.FlightType,
                bulletType      = (byte)bulletCfg.BulletType,
                isHoming        = (byte)bulletCfg.IsHoming,
                isActive        = 1,
            };

            // 注册到集中管理器（替代 StartFlight 协程）
            bulletManager.Register(runtime);
        }

        /// <summary>获取子弹碰撞半径（int/100 → float）</summary>
        private static float GetBulletRadius(BulletConfig cfg)
        {
            return cfg.CollisionRadius / 100f;
        }
    }
}
