using Unity.Mathematics;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(GameFlowComponent))]
    [FriendOf(typeof(GameFlowComponent))]
    public static partial class GameFlowSystem
    {
        [EntitySystem]
        private static void Awake(this GameFlowComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this GameFlowComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = null;
        }

        /// <summary>
        /// 启动游戏流程主循环（由视图层场景创建器在 Unity 场景加载完成后调用）
        /// </summary>
        public static void StartFlow(this GameFlowComponent self)
        {
            self.CancellationToken = new ETCancellationToken();
            self.Run().Coroutine();
        }

        private static async ETTask Run(this GameFlowComponent self)
        {
            Scene scene = self.GetParent<Scene>();
            var token = self.CancellationToken;
            var timer = scene.Root().GetComponent<TimerComponent>();

            // ══════════════════════════════════════
            // Phase 1: BattlePrepare（布阵阶段）
            // ══════════════════════════════════════
            self.Phase = GameFlowPhase.BattlePrepare;

            // 创建英雄（configId=1001，位置 x=-5）
            await UnitFactory.CreateHero(scene, 1001, new float3(-5, 0, 0));

            // 布阵阶段短暂等待
            await timer.WaitAsync(1000, token);

            // ══════════════════════════════════════
            // Phase 2: Fighting（战斗中）
            // ══════════════════════════════════════
            self.Phase = GameFlowPhase.Fighting;

            // 战斗循环：定时随机刷怪 + 补充英雄
            while (self.Phase == GameFlowPhase.Fighting && !token.IsCancel())
            {
                // 检查场上英雄数量，不足则补充
                int heroCount = CountHeroes(scene);
                if (heroCount < self.MinHeroCount)
                {
                    var heroIds = self.SpawnHeroIds;
                    int hid = heroIds[RandomGenerator.RandomNumber(0, heroIds.Length)];
                    float hy = RandomGenerator.RandFloat01() * 3f - 1.5f;
                    await UnitFactory.CreateHero(scene, hid, new float3(-5, hy, 0));
                    heroCount++; // 刚补了一个
                }

                // 怪物数量超过上限（英雄数×倍率）则跳过本轮刷怪
                int monsterCount = CountMonsters(scene);
                if (monsterCount < heroCount * self.MaxMonsterRatio)
                {
                    var monsterIds = self.SpawnMonsterIds;
                    int mid = monsterIds[RandomGenerator.RandomNumber(0, monsterIds.Length)];
                    float my = RandomGenerator.RandFloat01() * 3f - 1.5f;
                    await UnitFactory.CreateMonster(scene, mid, new float3(0, my, 0));
                }

                // 等待下一次刷怪间隔
                await timer.WaitAsync(self.SpawnIntervalMs, token);
            }

            // 此协程结束（Phase 被外部切换了或场景销毁）
        }

        /// <summary>统计场上存活的英雄数量</summary>
        private static int CountHeroes(Scene scene)
        {
            int count = 0;
            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return 0;

            foreach (var kvp in unitManager.Children)
            {
                var identity = kvp.Value.GetComponent<IdentityComponent>();
                if (identity != null && identity.UnitType == UnitType.Hero)
                    count++;
            }
            return count;
        }

        /// <summary>统计场上存活的怪物数量</summary>
        private static int CountMonsters(Scene scene)
        {
            int count = 0;
            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return 0;

            foreach (var kvp in unitManager.Children)
            {
                var identity = kvp.Value.GetComponent<IdentityComponent>();
                if (identity != null && identity.UnitType == UnitType.Monster)
                    count++;
            }
            return count;
        }
    }
}
