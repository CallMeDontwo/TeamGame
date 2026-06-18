using ET.Client;

namespace ET.TeamGame
{
    [Event(SceneType.Current)]
    internal class ChangeRotation_Event : AEvent<Scene, ChangeRotation>
    {
        protected override async ETTask Run(Scene scene, ChangeRotation a)
        {
            if (a.Unit.TryGetComponent(out UnitGameObjectComponent component))
            {
                if (component.GameObject != null)
                {
                    component.SetAnimationFlipX(a.Unit.Forward.x < 0);
                }
                await ETTask.CompletedTask;
            }
        }
    }
}