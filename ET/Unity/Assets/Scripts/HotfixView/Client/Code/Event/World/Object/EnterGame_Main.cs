namespace ET
{
    [Event(SceneType.Main)]
    internal class EnterGame_Main : AEvent<Scene, EnterGame>
    {
        protected override async ETTask Run(Scene scene, EnterGame a)
        {
            await CurrentSceneFactory.EnterGame(scene, a.GameName, a.RoomId, a.MachineId);
        }
    }
}