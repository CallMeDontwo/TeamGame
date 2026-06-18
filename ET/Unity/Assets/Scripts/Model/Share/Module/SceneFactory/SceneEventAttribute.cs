using System;

namespace ET
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SceneEventAttribute : BaseAttribute
    {
    }
}