namespace ET
{
    [Invoke((long)SceneType.Main)]
    public class FiberCreate_Main : AInvokeHandler<FiberCreate, ETTask>
    {
        public override async ETTask Handle(FiberCreate fiberInit)
        {
            Scene root = fiberInit.Fiber.Root;
            await SceneCreatDispatcher.Instance.OnCreate(root, root, new SceneArguments(root.Id, root.Name, null));
            await SceneCreatDispatcher.Instance.OnCreateComplete(root, root, new SceneArguments(root.Id, root.Name, null));
        }
    }
}