using System;
using System.Reflection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using TrueSync;
using Unity.Mathematics;

namespace ET
{
    public static class MongoRegister
    {
        public static void Init()
        {
            // 清理老的数据
            MethodInfo createSerializerRegistry = typeof(BsonSerializer).GetMethod("CreateSerializerRegistry", BindingFlags.Static | BindingFlags.NonPublic);
            createSerializerRegistry.Invoke(null, Array.Empty<object>());
            MethodInfo registerIdGenerators = typeof(BsonSerializer).GetMethod("RegisterIdGenerators", BindingFlags.Static | BindingFlags.NonPublic);
            registerIdGenerators.Invoke(null, Array.Empty<object>());
            // 自动注册IgnoreExtraElements
            ConventionRegistry.Register("IgnoreExtraElements", new ConventionPack() { new IgnoreExtraElementsConvention(true) }, type => true);

            RegisterStruct<float2>();
            RegisterStruct<float3>();
            RegisterStruct<float4>();
            RegisterStruct<quaternion>();
            RegisterStruct<FP>();
            RegisterStruct<TSVector>();
            RegisterStruct<TSVector2>();
            RegisterStruct<TSVector4>();
            RegisterStruct<TSQuaternion>();

            foreach (Type type in CodeTypes.Instance.GetTypes().Values)
            {
                TryLookupClassMap(type);
                TryRegisterStruct(type);
            }
        }

        private static void TryLookupClassMap(Type type)
        {
            if (type.IsSubclassOf(typeof(Object)) && !type.IsGenericType)
            {
                BsonClassMap.LookupClassMap(type);
            }
        }

        private static void TryRegisterStruct(Type type)
        {
            if (type.IsValueType && type.GetCustomAttribute<MongoRegisterAttribute>() != null)
            {
                BsonSerializer.RegisterSerializer(type, new StructBsonSerializer(type));
            }
        }

        private static void RegisterStruct<T>() where T : struct
        {
            BsonSerializer.RegisterSerializer(typeof(T), new StructBsonSerializer(typeof(T)));
        }
    }
}