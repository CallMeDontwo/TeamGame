using ET.TeamGame;
using FairyGUI;
using System.Threading.Tasks;

namespace ET
{
    /// <summary>
    /// TeamGame 场景视图层 — 加载 Unity 场景
    /// Order=10，在 ADataSceneCreater (Order=0) 之后执行
    /// </summary>
    internal class TeamGameSceneCreater1 : AViewSceneCreater
    {
        public override string GetSceneName() => "TeamGame";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            // 加载 FGUI 包
            await UIPackageHelper.AddPackageAsync(FUIPackage.TeamGame);

            // 加载共享 UI 资源包（如果存在）
            var sharedPkg = UIPackage.GetByName(FUIPackage.TeamGame);
            sharedPkg?.LoadAllAssets();

            // 先加载 Unity 场景，再创建测试 Unit（避免场景加载时销毁 GameObject）
            await this.LoadScene(parent, scene, this.GetSceneName());

            // 添加 AI 调试 Overlay（按 F1 开关）
            var debugGo = new UnityEngine.GameObject("AIDebugger");
            debugGo.AddComponent<AIDebugger>().scene = scene;

            // 添加碰撞调试可视化（按 F2 开关）
            var collisionGo = new UnityEngine.GameObject("CollisionDebugger");
            collisionGo.AddComponent<CollisionDebugger>().scene = scene;

            await scene.GetComponent<UnitManager>().CreateTestUnits(scene);
        }

        public override async ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            UIPackageHelper.RemovePackage(FUIPackage.TeamGame);
            await ETTask.CompletedTask;
        }
    }
}
