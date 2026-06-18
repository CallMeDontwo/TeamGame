using System;
using System.Collections.Generic;

namespace ET
{
    public sealed class PackageBinder : Singleton<PackageBinder>, ISingletonAwake
    {
        public void Awake()
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(PackageBinderAttribute));
            foreach (var type in types)
            {
                if (typeof(IPackageBinder).IsAssignableFrom(type))
                {
                    (Activator.CreateInstance(type) as IPackageBinder).BindAll();
                }
            }
        }
    }
}