using System;
using System.Collections.Generic;

namespace ET
{
    [Code]
    public class SceneEventDispatcher : Singleton<SceneEventDispatcher>, ISingletonAwake
    {
        private readonly MultiDictionary<string, Type, ASceneEvent> events = new MultiDictionary<string, Type, ASceneEvent>();

        public void Awake()
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(SceneEventAttribute));
            foreach (Type type in types)
            {
                ASceneEvent sceneEvent = Activator.CreateInstance(type) as ASceneEvent;
                this.events.Add(sceneEvent.GetSceneName(), sceneEvent.GetEventType(), sceneEvent);
            }
        }

        public async ETTask Handle<T>(Scene scene, T arg)
        {
            if (this.events.TryGetValue(scene.Name, typeof(T), out ASceneEvent basehandler))
            {
                if (basehandler is ASceneEvent<T> handler)
                {
                    await handler.Handle(scene, arg);
                }
            }
        }
    }
}