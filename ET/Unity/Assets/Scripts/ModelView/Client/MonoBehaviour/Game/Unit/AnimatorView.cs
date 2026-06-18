using Spine;
using Spine.Unity;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 动画视图 — 驱动 Unit GameObject 的实际动画播放（Spine / Animator）
    /// 在 UnitViewFactory 中挂载到实例化后的 GameObject 上
    /// </summary>
    [EnableClass]
    public class AnimatorView : MonoBehaviour
    {
        public string CurrentAnimation { get; private set; }

        public SkeletonAnimation skeleton;
        public void PlayAnimation(string animName)
        {
            if (string.IsNullOrEmpty(animName)) return;
            if (animName == CurrentAnimation) return;

            CurrentAnimation = animName;

            // TODO: 接入 Spine 或 Unity Animator 播放
             skeleton.AnimationState.SetAnimation(0, animName, true);
        }
    }
}
