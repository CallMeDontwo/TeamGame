using System.Collections.Generic;

namespace ET
{
    public readonly struct SceneArguments
    {
        public readonly long Id;
        public readonly string Name;
        public readonly Dictionary<string, object> Arguments;

        public SceneArguments(long id, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Arguments = DictionaryComponent<string, object>.Create();
        }

        public SceneArguments(long id, string name, Dictionary<string, object> arguments)
        {
            this.Id = id;
            this.Name = name;
            this.Arguments = arguments;
        }

        public void Add(string key, object value)
        {
            this.Arguments.Add(key, value);
        }

        public void Add<T>(T value)
        {
            this.Arguments.Add(typeof(T).Name, value);
        }

        public bool Has(string key)
        {
            return this.Arguments.ContainsKey(key);
        }

        public bool Has<T>()
        {
            return this.Has(typeof(T).Name);
        }

        public T Get<T>(string key)
        {
            return this.Arguments.TryGetValue(key, out object value) ? (T)value : default;
        }

        public T Get<T>()
        {
            return this.Get<T>(typeof(T).Name);
        }

        public T Get<T>(string key, T defaultValue)
        {
            return this.TryGet(key, out T value) ? value : defaultValue;
        }

        public T Get<T>(T defaultValue)
        {
            return this.Get(typeof(T).Name, defaultValue);
        }

        public bool TryGet<T>(string key, out T value)
        {
            bool res = this.Has(key);
            value = res ? this.Get<T>(key) : default;
            return res;
        }

        public bool TryGet<T>(out T value)
        {
            return this.TryGet(typeof(T).Name, out value);
        }
    }
}