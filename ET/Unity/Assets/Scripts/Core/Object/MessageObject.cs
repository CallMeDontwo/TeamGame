using System;
using System.ComponentModel;
using MongoDB.Bson.Serialization.Attributes;
using ProtoBuf;

namespace ET
{
    [DisableNew]
    public class MessageObject: ProtoObject, IMessage, IDisposable, IPool
    {
        [BsonIgnore]
        [ProtoIgnore]
        public bool IsFromPool { get; set; }

        [BsonIgnore]
        [ProtoIgnore]
        public int Error { get; set; }

        [BsonIgnore]
        [ProtoIgnore]
        public string Message { get; set; }

        public virtual void Dispose()
        {
        }
    }
}