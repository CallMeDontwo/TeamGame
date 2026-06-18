using ET.Client;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听英雄 Unit 创建事件 → 创建 GameObject 视图
    /// </summary>
    [Event(SceneType.Current)]
    internal class AfterHeroCreate_CreateView : AEvent<Scene, AfterHeroCreate>
    {
        protected override async ETTask Run(Scene scene, AfterHeroCreate a)
        {
            if (a.Unit.IsDisposed) return;

            await UnitViewFactory.CreateView(a.Unit);
        }
    }
}
