using System;
using UnityEngine;

namespace ET
{
    [EnableClass]
    [AddComponentMenu("ObjectEvent/ObjectEvent")]
    public sealed class ObjectEvent : SceneMonoBehaviour
    {
        public string EventName;
        public string[] EventArgs;
        private bool _enable = true;

        public void Invoke()
        {
            this.InvokeInternal().Coroutine();
        }

        private async ETTask InvokeInternal()
        {
            try
            {
                if (!this._enable)
                    return;
                this._enable = false;
                switch (this.EventName)
                {
                    case "OpenUI":
                        ViewController.Instance.GetByGType(this.EventArgs[0]).Show(this);
                        break;
                    case "EnterWorld":
                        await EventSystem.Instance.PublishAsync(this.CurtScene, new EnterWorld() { WorldId = this.EventArgs[0] });
                        break;
                    case "EnterGame":
                        await EventSystem.Instance.PublishAsync(this.CurtScene, new EnterGame() { GameName = this.EventArgs[0], RoomId = int.Parse(this.EventArgs[1]), MachineId = int.Parse(this.EventArgs[2]) });
                        break;
                    case "Transport":
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            finally
            {
                this._enable = true;
            }
        }
    }
}