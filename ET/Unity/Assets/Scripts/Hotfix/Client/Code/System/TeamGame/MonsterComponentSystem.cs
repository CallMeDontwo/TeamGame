namespace ET.TeamGame
{
    [EntitySystemOf(typeof(MonsterComponent))]
    [FriendOf(typeof(MonsterComponent))]
    public static partial class MonsterComponentSystem
    {
        [EntitySystem]
        private static void Awake(this MonsterComponent self)
        {
            self.DropItemIds = System.Array.Empty<int>();
            self.PatrolPath  = string.Empty;
            self.AggroRange  = 5f;
            self.RespawnTime = 0f;
            self.IsElite     = false;
            self.IsBoss      = false;
            self.AIConfigId  = 0;
        }

        [EntitySystem]
        private static void Destroy(this MonsterComponent self)
        {
            self.DropItemIds = System.Array.Empty<int>();
            self.PatrolPath  = null;
            self.AIConfigId  = 0;
        }

        /// <summary>
        /// 初始化怪物参数，并挂载 AI 组件
        /// </summary>
        public static void InitFromConfig(this MonsterComponent self, int configId, MonsterConfig monsterConfig)
        {
            var unit = self.GetParent<Unit>();

            int aiConfigId = monsterConfig.AIconfigId;
            self.AIConfigId = aiConfigId;

            // 挂载 AI 组件
            if (aiConfigId > 0)
            {
                unit.AddComponent<AIComponent, int>(aiConfigId);

                // 同步感知参数
                var perception = unit.GetComponent<PerceptionComponent>();
                if (perception != null)
                {
                    var aiConfig = AIConfigCategory.Instance.Get(aiConfigId);
                    perception.InitFromConfig(aiConfig.SightRange);
                }
            }
        }

        /// <summary>
        /// 处理怪物死亡 → 掉落物品、触发重生计时器
        /// </summary>
        public static async ETTask OnDeath(this MonsterComponent self)
        {
            // 1. 掉落物品（发送给战斗结算系统）
            // 2. 若 RespawnTime > 0，启动重生计时器
            await ETTask.CompletedTask;
        }
    }
}
