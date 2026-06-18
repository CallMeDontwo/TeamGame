namespace ET
{
    public static class CurrentSceneFactory
    {
        public static async ETTask<Scene> EnterScene(Scene scene, int sceneId)
        {
            SceneConfig config = SceneConfigCategory.Instance.Get(sceneId);
            return await Create(scene, config.Id, config.SceneFile);
        }

        public static async ETTask<Scene> EnterGame(Scene parent, string name, int roomId, int machineId)
        {
            DictionaryComponent<string, object> args = DictionaryComponent<string, object>.Create();
            args.Add("RoomId", roomId);
            args.Add("MachineId", machineId);
            return await Create(parent, machineId, name, args);
        }

        public static async ETTask<Scene> Create(Scene parent, long id, string name, DictionaryComponent<string, object> args = null)
        {
            using CoroutineLock coroutineLock = await parent.GetComponent<CoroutineLockComponent>().Wait(CoroutineType_Lock.SceneSwitch, 0, 0);
            args ??= DictionaryComponent<string, object>.Create();
            bool result = await SceneCreatDispatcher.Instance.TryCreate(parent, new SceneArguments(id, name, args));
            if (result)
            {
                return await CreateInternal(id, name, parent.GetComponent<CurrentScenesComponent>(), args);
            }
            return default;
        }

        private static async ETTask<Scene> CreateInternal(long id, string name, CurrentScenesComponent parent, DictionaryComponent<string, object> args)
        {
            await EventSystem.Instance.PublishAsync(parent.Scene(), new SceneChangeStart());
            if (parent.Scene != null)
            {
                SceneArguments oldArguments = new SceneArguments(parent.Scene.Id, parent.Scene.Name, parent.Scene.GetComponent<SceneArgumentsComponent>().Arguments);
                await EventSystem.Instance.PublishAsync(parent.Scene, new BeforeDestroyCurrentScene());
                await SceneCreatDispatcher.Instance.OnDestory(parent.Scene(), parent.Scene, oldArguments);
                parent.Scene.Dispose();
            }
            parent.Scene = EntitySceneFactory.CreateScene(parent, id, IdGenerater.Instance.GenerateInstanceId(), SceneType.Current, name);
            parent.Scene.AddComponent<ObjectWait>();
            parent.Scene.AddComponent<TimerComponent>();
            parent.Scene.AddComponent<CurrentSceneSessionComponent>().Component = parent.Scene().GetComponent<SessionComponent>();
            SceneArgumentsComponent sceneArgs = parent.Scene.AddComponent<SceneArgumentsComponent>(true);
            args.Foreach(kv => sceneArgs.Add(kv.Key, kv.Value));
            args.Dispose();
            SceneArguments newArguments = new SceneArguments(id, name, sceneArgs.Arguments);
            await SceneCreatDispatcher.Instance.OnCreate(parent.Scene(), parent.Scene, newArguments);
            await EventSystem.Instance.PublishAsync(parent.Scene, new AfterCreateCurrentScene());
            await SceneCreatDispatcher.Instance.OnCreateComplete(parent.Scene(), parent.Scene, newArguments);
            parent.Scene.GetComponent<ObjectWait>().Notify(new CreateSceneComplete());
            await EventSystem.Instance.PublishAsync(parent.Scene(), new SceneChangeFinish());
            return parent.Scene;
        }
    }
}