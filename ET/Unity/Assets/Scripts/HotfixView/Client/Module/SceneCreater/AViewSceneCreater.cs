using YooAsset;

namespace ET
{
    internal abstract class AViewSceneCreater : ASceneCreater
    {
        public override int GetOrder() => 10;

        public override async ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            await ETTask.CompletedTask;
            return true;
        }

        protected async ETTask LoadScene(Scene parent, Scene scene, string sceneName)
        {
            SceneHandle handle = YooAssets.LoadSceneAsync(sceneName);
            do
            {
                await scene.GetComponent<TimerComponent>().WaitFrameAsync();
                LoadingWindow.Instance.SelfUI.Bar_Loading.value = handle.Progress * 100;
            } while (!handle.IsDone);
            UnityEngine.Object.FindObjectsOfType<SceneMonoBehaviour>().Foreach(item => { item.CurtScene = scene; item.MainScene = parent; });
        }
    }
}