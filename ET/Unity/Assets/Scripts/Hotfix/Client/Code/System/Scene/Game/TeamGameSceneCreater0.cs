using ET.TeamGame;

namespace ET.Game
{
    /// <summary>
    /// TeamGame 场景（测试核心玩法专用）
    /// - 无网络组件（SessionComponent / NetProtoComponent）
    /// - 无UI组件（后续设计）
    /// - TimerComponent 由 CurrentSceneFactory.CreateInternal 自动添加
    /// - UnitManager 已在此添加作为 Unit 容器
    /// - BulletManagerComponent 已在此添加作为子弹集中管理器
    /// </summary>
    internal class TeamGameSceneCreater0 : ADataSceneCreater
    {
        public override string GetSceneName() => "TeamGame";

        public override async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            scene.AddComponent<UnitManager>();
            scene.AddComponent<BulletManagerComponent>();

            await ETTask.CompletedTask;
        }

        public override async ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args)
        {
            await ETTask.CompletedTask;
        }
    }
}
