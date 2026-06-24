namespace ET.TeamGame
{
    /// <summary>
    /// 死亡清理：监听 Unit 状态切换为 Death → 等动画播完 → Dispose
    /// </summary>
    [Event(SceneType.Current)]
    public class UnitDeathHandler : AEvent<Scene, UnitStateChanged>
    {
        protected override async ETTask Run(Scene scene, UnitStateChanged a)
        {
            if (a.NewState != UnitState.Death) return;
            var unit = a.Unit;
            if (unit == null || unit.IsDisposed) return;

            // 等死亡动画播完（暂时写死 1000ms，后续可读 AnimatorComponent 实际时长）
            await scene.GetComponent<TimerComponent>().WaitAsync(1000);

            // Dispose: ET Entity 链递归清理 → UnitGameObjectComponent.Destroy → GameObjectPool.Recycle → 从 UnitManager.Children 移除
            unit.Dispose();
        }
    }
}
