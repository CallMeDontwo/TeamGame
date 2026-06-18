using ET.Client;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听子弹 Unit 创建事件 → 创建 GameObject 视图
    /// </summary>
    [Event(SceneType.Current)]
    internal class AfterBulletCreate_CreateView : AEvent<Scene, AfterBulletCreate>
    {
        protected override async ETTask Run(Scene scene, AfterBulletCreate a)
        {
            if (a.BulletUnit.IsDisposed) return;

            await UnitViewFactory.CreateView(a.BulletUnit);
        }
    }
}
