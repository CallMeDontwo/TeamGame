using System;
using System.Collections.Generic;
using System.Reflection;

namespace ET
{
    public sealed class ProtoType : Singleton<ProtoType>, ISingletonAwake
    {
        public readonly Dictionary<int, Type> CodeType = new Dictionary<int, Type>();
        public readonly Dictionary<Type, int> TypeCode = new Dictionary<Type, int>();

        public void Awake()
        {
            HashSet<Type> type = CodeTypes.Instance.GetTypes(typeof(ProtoIdAttribute));
            foreach (Type t in type)
            {
                ProtoIdAttribute attribute = t.GetCustomAttribute<ProtoIdAttribute>();
                this.CodeType.Add(attribute.ID, t);
                this.TypeCode.Add(t, attribute.ID);
            }
        }
    }
}