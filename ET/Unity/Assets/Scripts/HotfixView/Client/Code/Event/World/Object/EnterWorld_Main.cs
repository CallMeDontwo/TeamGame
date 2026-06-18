namespace ET
{
    [Event(SceneType.Main)]
    internal class EnterWorld_Main : AEvent<Scene, EnterWorld>
    {
        protected override async ETTask Run(Scene scene, EnterWorld a)
        {
            await CurrentSceneFactory.EnterScene(scene, int.Parse(a.WorldId));
        }
    }
}