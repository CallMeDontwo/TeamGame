namespace ET
{
    public static class ConvertHelper
    {
        public static T Handle<T>(in T obj, in MessageObject message)
        {
            MessageConverter.Instance.Handle(obj, message);
            return obj;
        }

        public static T Handle<T>(in T message, in object obj) where T : MessageObject
        {
            MessageConverter.Instance.Handle(message, obj);
            return message;
        }

        public static TMessage NewMessage<TMessage>(in object obj) where TMessage : MessageObject, new()
        {
            return MessageConverter.Instance.GetMessage<TMessage>(obj);
        }

        public static TObject NewObject<TObject>(in MessageObject message) where TObject : new()
        {
            return MessageConverter.Instance.NewObject<TObject>(message);
        }

        public static TEntity NewEntity<TParent, TEntity>(in TParent parent, in MessageObject message, bool fromPool = false) where TEntity : Entity, IAwake, new() where TParent : Entity
        {
            return MessageConverter.Instance.NewEntity<TParent, TEntity>(parent, message, fromPool);
        }

        public static TEntity NewEntity<TParent, TEntity>(in TParent parent, in long id, in MessageObject message, bool fromPool = false) where TEntity : Entity, IAwake, new() where TParent : Entity
        {
            return MessageConverter.Instance.NewEntity<TParent, TEntity>(parent, id, message, fromPool);
        }
    }
}