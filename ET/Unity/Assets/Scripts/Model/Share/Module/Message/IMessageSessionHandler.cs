using System;

namespace ET
{
    public interface IMessageSessionHandler
    {
        void Handle(Entity session, object message);
        Type GetMessageType();

        Type GetResponseType();
    }
}