namespace ET
{
    [ComponentOf(typeof(Scene))]
    public sealed class SceneArgumentsComponent : Entity, IAwake, IDestroy
    {
        public DictionaryComponent<string, object> Arguments { get; private set; }

        private sealed class SceneArgumentsComponentAwake : AwakeSystem<SceneArgumentsComponent>
        {
            protected override void Awake(SceneArgumentsComponent self)
            {
                self.Arguments = DictionaryComponent<string, object>.Create();
            }
        }

        private sealed class SceneArgumentsComponentDestroy : DestroySystem<SceneArgumentsComponent>
        {
            protected override void Destroy(SceneArgumentsComponent self)
            {
                self.Arguments?.Dispose();
                self.Arguments = null;
            }
        }
    }

    [FriendOf(typeof(SceneArgumentsComponent))]
    public static class SceneArgumentsComponentSystem
    {
        public static void Add(this SceneArgumentsComponent self, string key, object value)
        {
            self.Arguments.Add(key, value);
        }

        public static void Add<T>(this SceneArgumentsComponent self, T value)
        {
            self.Arguments.Add(typeof(T).Name, value);
        }

        public static void Set<T>(this SceneArgumentsComponent self, T value)
        {
            self.Arguments[typeof(T).Name] = value;
        }

        public static T Get<T>(this SceneArgumentsComponent self, string key)
        {
            return self.Arguments.TryGetValue(key, out object value) ? (T)value : default;
        }

        public static T Get<T>(this SceneArgumentsComponent self)
        {
            return self.Get<T>(typeof(T).Name);
        }

        public static bool TryGet<T>(this SceneArgumentsComponent self, string key, out T value)
        {
            if (self.Arguments.ContainsKey(key))
            {
                value = self.Get<T>(key);
                return true;
            }
            value = default;
            return false;
        }

        public static bool TryGet<T>(this SceneArgumentsComponent self, out T value)
        {
            return self.TryGet(typeof(T).Name, out value);
        }
    }
}