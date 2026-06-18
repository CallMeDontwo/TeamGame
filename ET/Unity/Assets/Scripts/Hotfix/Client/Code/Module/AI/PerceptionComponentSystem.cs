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

            // 遍历场景中所有 Unit（通过 UnitManager 的 Children）
            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit other) continue;
                if (other.Id == unit.Id) continue;

                // 判断是否为敌对目标
                var otherIdentity = other.GetComponent<IdentityComponent>();
                if (otherIdentity == null) continue;

                if (!IsEnemy(selfUnitType, otherIdentity.UnitType)) continue;

                // 距离检测
                float3 diff = other.Position - unitPos;
                float distSq = diff.x * diff.x + diff.y * diff.y + diff.z * diff.z;
                if (distSq <= sightRangeSq)
                {
                    self.VisibleTargets.Add(other.Id);

                    // 记录最近的目标
                    if (distSq < nearestDistSq)
                    {
                        nearestDistSq = distSq;
                        nearestId = other.Id;
                    }
                }
            }

            // 选择最近的目标作为主目标
            if (nearestId > 0)
            {
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
