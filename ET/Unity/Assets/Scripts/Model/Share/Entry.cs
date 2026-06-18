using ET.Client;

namespace ET
{
    public static class Entry
    {
        public static void Init()
        {
        }

        public static void Start()
        {
            StartAsync().Coroutine();
        }

        private static async ETTask StartAsync()
        {
            WinPeriod.Init();
            // 注册Mongo type
            MongoRegister.Init();
            // 注册Entity序列化器
            EntitySerializeRegister.Init();

            World.Instance.AddSingleton<IdGenerater>();
            World.Instance.AddSingleton<ProtoType>();
            World.Instance.AddSingleton<OpcodeType>();
            World.Instance.AddSingleton<ObjectPool>();
            World.Instance.AddSingleton<MessageQueue>();
            World.Instance.AddSingleton<NetServices>();
            World.Instance.AddSingleton<NavmeshComponent>();
            World.Instance.AddSingleton<LogMsg>();
            World.Instance.AddSingleton<ConfigLoader>();
            World.Instance.AddSingleton<ServerConfig>();

            // 创建需要reload的code singleton
            CodeTypes.Instance.CreateCode();

            await ConfigLoader.Instance.LoadAsync();
            await EventSystem.Instance.Invoke<AppInit, ETTask>(new AppInit());
            await FiberManager.Instance.Create(SchedulerType.Main, ConstFiberId.Main, 0, SceneType.Main, "Main");
        }
    }
}