using System;

namespace ET
{
    public abstract class MessageConvert : Object
    {
        public abstract Type ObjectType { get; }
        public abstract Type MessageType { get; }
        public abstract void Handle(in object entity, in IMessage message);
        public abstract void Handle(in IMessage message, in object entity);
    }

    [MessageIOC]
    public abstract class MessageConvert<TObject, TMessage> : MessageConvert where TMessage : IMessage
    {
        public override Type ObjectType => typeof(TObject);

        public override Type MessageType => typeof(TMessage);

        public abstract void ToObject(TObject entity, TMessage message);

        public abstract void ToMessage(TMessage message, TObject entity);

        public override void Handle(in object entity, in IMessage message) => this.ToObject((TObject)entity, (TMessage)message);

        public override void Handle(in IMessage message, in object entity) => this.ToMessage((TMessage)message, (TObject)entity);
    }
}