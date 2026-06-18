namespace ET.TeamGame
{
    /// <summary>
    /// AI 行为执行结果 — 主循环根据结果做下一步决策
    /// </summary>
    public enum AIHandlerResult
    {
        Success,        // 正常执行完毕
        TargetLost,     // 追击/攻击中目标丢失
        TargetDead,     // 攻击中目标死亡
        NoTarget,       // 执行时没有目标
    }

    /// <summary>
    /// AI 大脑组件 — 挂载在 Unit 上，纯调度：只决策不执行
    /// 具体行为由 Behavior/ 目录下各独立文件实现
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class AIComponent : Entity, IAwake<int>, IDestroy
    {
        /// <summary>当前使用的 AI 配置 ID（关联 AIConfig 表）</summary>
        public int AIConfigId { get; set; }

        /// <summary>协程取消令牌</summary>
        public ETCancellationToken CancellationToken { get; set; }

        /// <summary>暂停标志（眩晕/冰冻时置 true，跳过 AI 循环）</summary>
        public bool Paused { get; set; }

        /// <summary>当前执行的行为名称（调试用）</summary>
        public string CurrentBehaviorName { get; set; } = string.Empty;
    }
}
