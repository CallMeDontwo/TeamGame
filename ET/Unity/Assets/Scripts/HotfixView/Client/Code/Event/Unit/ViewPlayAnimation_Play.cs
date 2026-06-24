using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听 Unit 状态变化 → 通过 AnimatorView 播放对应动画
    /// 状态 → 动画名映射：Idle="idle", Move="walk", Attack="attack", Hit="hit", Death="death"
    /// </summary>
    [Event(SceneType.Current)]
    internal class ViewPlayAnimation_Play : AEvent<Scene, ViewPlayAnimation>
    {
        protected override async ETTask Run(Scene scene, ViewPlayAnimation a)
        {
            var unit = a.Unit;
            if (unit.IsDisposed) return;

            // 获取视图层 AnimatorView 播放动画
            if (unit.TryGetComponent(out UnitGameObjectComponent view) && view.GameObject != null)
            {
                var animView = view.GameObject.GetComponent<AnimatorView>();
                if (animView != null)
                {
                    animView.PlayAnimation(a.anime,a.isLoop);
                }
            }

            await ETTask.CompletedTask;
        }

        
    }
}
