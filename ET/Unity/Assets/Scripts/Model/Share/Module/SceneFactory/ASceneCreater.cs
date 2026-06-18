namespace ET
{
    [SceneCreat]
    public abstract class ASceneCreater : Object
    {
        public abstract int GetOrder();
        public abstract string GetSceneName();
        public abstract ETTask<bool> TryCreate(Scene parent, SceneArguments args);
        public abstract ETTask OnCreate(Scene parent, Scene scene, SceneArguments args);
        public virtual async ETTask OnCreateComplete(Scene parent, Scene scene, SceneArguments args) => await ETTask.CompletedTask;
        public abstract ETTask OnDestroy(Scene parent, Scene scene, SceneArguments args);
    }
}