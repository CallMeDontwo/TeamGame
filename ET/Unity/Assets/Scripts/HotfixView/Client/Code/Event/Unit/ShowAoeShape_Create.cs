namespace ET.TeamGame
{
    /// <summary>
    /// AfterAoeFindTarget 事件处理 — 将 AOE 形状添加到调试可视化
    /// </summary>
    [Event(SceneType.Current)]
    public class ShowAoeShape_Create : AEvent<Scene ,AfterAoeFindTarget>
    {
        protected override async ETTask Run(Scene scene, AfterAoeFindTarget a)
        {
            var drawer = AoeShapeDrawer.Instance;
            if (drawer == null) return;

            drawer.AddShape(a.ShapeType, a.Center, a.Forward, a.Param1, a.Param2);
            await ETTask.CompletedTask;
        }
    }
}
