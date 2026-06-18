using System;
using System.Collections.Generic;

namespace ET
{
    public readonly struct MessageSessionDispatcherInfo
    {
        public SceneType SceneType { get; }
        public IMessageSessionHandler IMHandler { get; }

        public MessageSessionDispatcherInfo(SceneType sceneType, IMessageSessionHandler imHandler)
        {
            this.SceneType = sceneType;
            this.IMHandler = imHandler;
        }
    }

    [Code]
    public class MessageSessionDispatcher : Singleton<MessageSessionDispatcher>, ISingletonAwake
    {
        private readonly Dictionary<Type, List<MessageSessionDispatcherInfo>> handlers = new();

        public void Awake()
        {
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(MessageSessionHandlerAttribute));

            foreach (Type type in types)
            {
                if (Activator.CreateInstance(type) is not IMessageSessionHandler iMessageSessionHandler)
                {
                    Log.Error($"message handle {type.Name} 需要继承 IMHandler");
                    continue;
                }

                object[] attrs = type.GetCustomAttributes(typeof(MessageSessionHandlerAttribute), true);
                foreach (object attr in attrs)
                {
                    Type messageType = iMessageSessionHandler.GetMessageType();
                    MessageSessionHandlerAttribute messageSessionHandlerAttribute = attr as MessageSessionHandlerAttribute;
                    MessageSessionDispatcherInfo messageSessionDispatcherInfo = new(messageSessionHandlerAttribute.SceneType, iMessageSessionHandler);
                    this.RegisterHandler(messageType, messageSessionDispatcherInfo);
                }
            }
        }

        private void RegisterHandler(Type messageType, MessageSessionDispatcherInfo handler)
        {
            if (!this.handlers.ContainsKey(messageType))
            {
                this.handlers.Add(messageType, new List<MessageSessionDispatcherInfo>());
            }

            this.handlers[messageType].Add(handler);
        }

        public void Handle(Entity session, object message)
        {
            if (!this.handlers.TryGetValue(message.GetType(), out List<MessageSessionDispatcherInfo> actions))
            {
                Log.Error($"消息没有处理: {message.GetType()} {message}");
                return;
            }

            SceneType sceneType = session.IScene.SceneType;
            foreach (MessageSessionDispatcherInfo ev in actions)
            {
                if (!ev.SceneType.HasSameFlag(sceneType))
                {
                    continue;
                }

                try
                {
                    ev.IMHandler.Handle(session, message);
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
        }
    }
}