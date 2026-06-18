using ET.Client;

namespace ET
{
    [Event(SceneType.Main)]
    internal class SceneChangeFinish_Event : AEvent<Scene, SceneChangeFinish>
    {
        protected override async ETTask Run(Scene scene, SceneChangeFinish a)
        {
            LoadingWindow.Instance.Hide();
            await ETTask.CompletedTask;
        }
    }
}