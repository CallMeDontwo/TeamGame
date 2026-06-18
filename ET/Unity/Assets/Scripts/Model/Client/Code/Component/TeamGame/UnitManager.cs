namespace ET.TeamGame
{
    /// <summary>
    /// 单位管理器 — 挂载在 Scene 上，作为所有 Unit 的父容器
    /// Unit 通过 AddChild<Unit>() 成为其 Children，无需额外注册
    /// AI 感知系统通过 UnitManager.Children 遍历所有 Unit
    /// </summary>
    [ComponentOf(typeof(Scene))]
    [EnableMethod]
    public sealed partial class UnitManager : Entity, IAwake, IDestroy
    {
    }
}
