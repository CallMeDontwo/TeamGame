using System;

namespace ET
{
    [SceneEvent]
    public abstract class ASceneEvent : Object
    {
        public abstract Type GetEventType();
        public abstract string GetSceneName();
    }

    public abstract class ASceneEvent<T> : ASceneEvent
    {
        public override Type GetEventType() => typeof(T);
        public abstract ETTask Handle(Scene scene, T arg);
    }
}