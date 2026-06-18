using ET.Client;

namespace ET.TeamGame
{
    [Event(SceneType.Current)]
    internal class ChangePosition_Event : AEvent<Scene, ChangePosition>
    {
        protected override async ETTask Run(Scene scene, ChangePosition a)
        {
            if (a.Unit.TryGetComponent(out UnitGameObjectComponent component))
            {
                // 避免 GameObject 已被销毁但仍发来位置更新
                if (component.GameObject != null)
                {
                    component.GameObject.transform.position = a.Unit.Position;
                }
                await ETTask.CompletedTask;
            }
        }
    }
}