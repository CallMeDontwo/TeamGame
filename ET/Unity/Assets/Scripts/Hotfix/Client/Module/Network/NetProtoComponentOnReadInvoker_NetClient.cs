namespace ET
{
    [Invoke((long)SceneType.Current)]
    internal class NetProtoComponentOnReadInvoker_NetClient : AInvokeHandler<NetProtoComponentOnRead>
    {
        public override void Handle(NetProtoComponentOnRead args)
        {
            PSession session = args.Session;
            if (args.RpcId == 0)
            {
                MessageSessionDispatcher.Instance.Handle(args.Session, args.Message);
                return;
            }
            //if (args.Message is RespAckError error)
            //{
            //    session.OnResponse(args.RpcId, error.errInfoId, string.Empty);
            //    error.Dispose();
            //    return;
            //}
            session.OnResponse(args.RpcId, args.Message as MessageObject);
        }
    }
}