using System;
using System.Collections.Generic;

namespace ET
{
    public sealed class ServerConfig : Singleton<ServerConfig>, ISingletonAwake
    {
        public readonly MultiDictionary<Type, long, object> Configs = new MultiDictionary<Type, long, object>();

        public void Awake()
        {
            this.Clear();
        }

        public void Add<T>(long id, T config) => this.Configs.Add(typeof(T), id, config);

        public void Add<T>(T config) where T : IConfig => this.Add<T>(config.Id, config);

        public void Set<T>(long id, T config)
        {
            this.Configs.Remove(typeof(T), id);
            this.Configs.Add(typeof(T), id, config);
        }

        public T Get<T>(long id) => this.Configs.TryGetValue(typeof(T), id, out object config) ? (T)config : default;

        public Dictionary<long, object> GetAll<T>() => this.Configs.TryGetDic(typeof(T), out Dictionary<long, object> dict) ? dict : default;

        public void Clear()
        {
            this.Configs.Values.Foreach(item => item.Clear());
            this.Configs.Clear();
        }

        protected override void Destroy()
        {
            this.Clear();
        }
    }
}