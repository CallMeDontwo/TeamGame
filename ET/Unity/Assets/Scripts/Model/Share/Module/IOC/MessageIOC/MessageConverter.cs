using System;
using System.Collections.Generic;

namespace ET
{
    [Code]
    public sealed class MessageConverter : Singleton<MessageConverter>, ISingletonAwake
    {
        /// <summary>
        /// key :Item1 EntityType Item2 MessageType
        /// </summary>
        public readonly Dictionary<(Type, Type), MessageConvert> Converts = new Dictionary<(Type, Type), MessageConvert>();

        public void Awake()
        {
            this.Converts.Clear();
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(MessageIOCAttribute));
            foreach (Type type in types)
            {
                MessageConvert convert = Activator.CreateInstance(type) as MessageConvert;
                this.Converts.Add((convert.ObjectType, convert.MessageType), convert);
            }
        }

        public void Handle(object entity, in MessageObject message)
        {
            try
            {
                if (entity is null)
                {
                    Log.Error("Entity is null");
                }
                if (message is null)
                {
                    Log.Error("Message is null");
                }
                if (this.Converts.TryGetValue((entity.GetType(), message.GetType()), out MessageConvert convert))
                {
                    convert.Handle(entity, message);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public void Handle(MessageObject message, in object entity)
        {
            try
            {
                if (this.Converts.TryGetValue((entity.GetType(), message.GetType()), out MessageConvert convert))
                {
                    convert.Handle(message, entity);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public TObject NewObject<TObject>(IMessage message) where TObject : new()
        {
            try
            {
                if (this.Converts.TryGetValue((typeof(TObject), message.GetType()), out MessageConvert convert))
                {
                    TObject entity = new TObject();
                    convert.Handle(entity, message);
                    return entity;
                }
                Log.Error($"没有{typeof(TObject)}和{message.GetType()}的IOC!");
                return default;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return default;
            }
        }

        public TEntity NewEntity<TParent, TEntity>(TParent parent, MessageObject message, bool fromPool = false) where TEntity : Entity, IAwake, new() where TParent : Entity
        {
            try
            {
                if (this.Converts.TryGetValue((typeof(TEntity), message.GetType()), out MessageConvert convert))
                {
                    TEntity entity = parent.AddChild<TEntity>(fromPool);
                    convert.Handle(entity, message);
                    return entity;
                }
                Log.Error($"没有{typeof(TEntity)}和{message.GetType()}的IOC!");
                return default;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return default;
            }
        }

        public TEntity NewEntity<TParent, TEntity>(TParent parent, long id, MessageObject message, bool fromPool = false) where TEntity : Entity, IAwake, new() where TParent : Entity
        {
            try
            {
                if (this.Converts.TryGetValue((typeof(TEntity), message.GetType()), out MessageConvert convert))
                {
                    TEntity entity = parent.AddChildWithId<TEntity>(id, fromPool);
                    convert.Handle(entity, message);
                    return entity;
                }
                Log.Error($"没有{typeof(TEntity)}和{message.GetType()}的IOC!");
                return default;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return default;
            }
        }

        public TMessage GetMessage<TMessage>(object entity) where TMessage : MessageObject, new()
        {
            try
            {
                if (this.Converts.TryGetValue((entity.GetType(), typeof(TMessage)), out MessageConvert convert))
                {
                    TMessage message = new TMessage();
                    convert.Handle(message, entity);
                    return message;
                }
                Log.Error($"没有{entity.GetType()}和{typeof(TMessage)}的IOC!");
                return default;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return default;
            }
        }
    }
}