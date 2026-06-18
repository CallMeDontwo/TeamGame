namespace ET.TeamGame
{
    /// <summary>
    /// 单位行为状态组件 — 挂载在 Unit 上，管理 Idle/Move/Attack/Death 等状态切换
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class StateComponent : Entity, IAwake, IDestroy
    {
        /// <summary>当前行为状态</summary>
        public UnitState State { get; set; } = UnitState.None;
    }
}
