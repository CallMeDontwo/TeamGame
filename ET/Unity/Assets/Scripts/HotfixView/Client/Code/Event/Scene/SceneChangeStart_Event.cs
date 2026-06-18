using ET.Client;

namespace ET
{
    [Event(SceneType.Main)]
    internal class SceneChangeStart_Event : AEvent<Scene, SceneChangeStart>
    {
        protected override async ETTask Run(Scene scene, SceneChangeStart a)
        {
            LoadingWindow.Instance.Show();
            LoadingWindow.Instance.SelfUI.Bar_Loading.value = 0;
            await ETTask.CompletedTask;
        }
    }
}