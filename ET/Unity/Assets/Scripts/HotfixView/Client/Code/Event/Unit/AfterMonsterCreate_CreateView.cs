using ET.Client;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听怪物 Unit 创建事件 → 创建 GameObject 视图
    /// </summary>
    [Event(SceneType.Current)]
    internal class AfterMonsterCreate_CreateView : AEvent<Scene, AfterMonsterCreate>
    {
        protected override async ETTask Run(Scene scene, AfterMonsterCreate a)
        {
            if (a.Unit.IsDisposed) return;

            await UnitViewFactory.CreateView(a.Unit);
        }
    }
}
