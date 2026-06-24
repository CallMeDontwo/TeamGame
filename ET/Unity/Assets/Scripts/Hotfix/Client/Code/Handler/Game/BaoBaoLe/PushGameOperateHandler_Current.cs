

namespace ET
{
    //[MessageSessionHandler(SceneType.Current)]
    //internal class PushGameOperateHandler_Current : MessageSessionHandler<PushGameOperate>
    //{
    //    protected override async ETTask Run(Entity session, PushGameOperate message)
    //    {
    //        GemBoomManagerComponent manager = session.Scene().GetComponent<GemBoomManagerComponent>();
    //        switch (message.num)
    //        {
    //            case 0:
    //                manager.AddCoin(1);
    //                break;
    //            case 1:
    //                manager.Run().Coroutine();
    //                break;
    //            case 2:
    //                manager.IncBet();
    //                break;
    //            case 3:
    //                manager.AutoSwitch();
    //                break;
    //            case 4:
    //                manager.Ticket();
    //                break;
    //        }
    //        await ETTask.CompletedTask;
    //    }
    //}
}