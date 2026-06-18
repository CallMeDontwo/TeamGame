using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听 Unit 状态变化 → 通过 AnimatorView 播放对应动画
    /// 状态 → 动画名映射：Idle="idle", Move="walk", Attack="attack", Hit="hit", Death="death"
    /// </summary>
    [Event(SceneType.Current)]
    internal class UnitStateChanged_PlayAnimation : AEvent<Scene, UnitStateChanged>
    {
        protected override async ETTask Run(Scene scene, UnitStateChanged a)
        {
            var unit = a.Unit;
            if (unit.IsDisposed) return;

            await ETTask.CompletedTask;
        }

        
    }
}
