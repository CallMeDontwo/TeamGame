namespace ET.TeamGame
{
    /// <summary>
    /// 游戏流程组件 — 挂载在 Scene 上，管理战斗阶段状态机
    /// </summary>
    [ComponentOf(typeof(Scene))]
    [EnableMethod]
    public sealed partial class GameFlowComponent : Entity, IAwake, IDestroy
    {
        /// <summary>当前流程阶段</summary>
        public GameFlowPhase Phase { get; set; } = GameFlowPhase.None;

        /// <summary>协程取消令牌</summary>
        public ETCancellationToken CancellationToken { get; set; }

        /// <summary>可刷怪配置 ID 列表</summary>
        public int[] SpawnMonsterIds = { 2001 };

        /// <summary>可刷新英雄配置 ID 列表</summary>
        public int[] SpawnHeroIds = { 1001,1003 };

        /// <summary>场上英雄最少数量（低于此数自动补充）</summary>
        public int MinHeroCount = 5;

        /// <summary>怪物最大倍率（场上怪物数 ≤ 英雄数 × 此值）</summary>
        public int MaxMonsterRatio = 1;

        /// <summary>刷怪间隔（毫秒）</summary>
        public int SpawnIntervalMs = 3000;
    }
}
