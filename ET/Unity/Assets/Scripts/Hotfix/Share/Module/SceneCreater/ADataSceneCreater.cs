namespace ET
{
    internal abstract class ADataSceneCreater : ASceneCreater
    {
        public override int GetOrder() => 0;

        public override async ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            await ETTask.CompletedTask;
            return true;
        }
    }
}