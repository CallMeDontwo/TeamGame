using ET.Client;
using UnityEngine;

namespace ET
{
    [Event(SceneType.Current)]
    internal class ChangeSpeed_Event : AEvent<Scene, ChangeSpeed>
    {
        protected override async ETTask Run(Scene scene, ChangeSpeed a)
        {
            await ETTask.CompletedTask;
        }
    }
}