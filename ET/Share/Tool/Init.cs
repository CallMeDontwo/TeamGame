using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using CommandLine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace ET.Server
{
    internal static class Init
    {
        private static int Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => Log.Error(e.ExceptionObject.ToString());
            
            try
            {
                // 命令行参数
                Parser.Default.ParseArguments<Options>(args)
                    .WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"))
                    .WithParsed((o)=>World.Instance.AddSingleton(o));
                
                World.Instance.AddSingleton<Logger>().Log = new NLogger(Options.Instance.AppType.ToString(), Options.Instance.Process, 0);                
                World.Instance.AddSingleton<CodeTypes, Assembly[]>([typeof (Init).Assembly]);
                World.Instance.AddSingleton<EventSystem>();
                
                // 强制调用一下mongo，避免mongo库被裁剪
                MongoHelper.ToJson(1);
                
                // 修复BSON float序列化截断问题: 注册宽松的FloatSerializer
                BsonSerializer.RegisterSerializer(typeof(float), new LenientSingleSerializer());
                
                ETTask.ExceptionHandler += Log.Error;
                
                Log.Info($"server start........................ ");
				
                switch (Options.Instance.AppType)
                {
                    case AppType.ExcelExporter:
                    {
                        Options.Instance.Console = 1;
                        ExcelExporter.Export();
                        return 0;
                    }
                    case AppType.Proto2CS:
                    {
                        Options.Instance.Console = 1;
                        Proto2CS.Export();
                        return 0;
                    }
                }
            }
            catch (Exception e)
            {
                Log.Console(e.ToString());
            }
            return 1;
        }
    }
    
    /// <summary>
    /// 宽松的float序列化器，允许从double到float的精度损失，避免TruncationException
    /// </summary>
    internal class LenientSingleSerializer : SerializerBase<float>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, float value)
        {
            context.Writer.WriteDouble(value);
        }

        public override float Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            var bsonType = context.Reader.GetCurrentBsonType();
            return bsonType switch
            {
                BsonType.Double => (float)context.Reader.ReadDouble(),
                BsonType.Int32 => context.Reader.ReadInt32(),
                BsonType.Int64 => context.Reader.ReadInt64(),
                BsonType.String => float.TryParse(context.Reader.ReadString(), out var f) ? f : 0f,
                _ => (float)context.Reader.ReadDouble()
            };
        }
    }
}