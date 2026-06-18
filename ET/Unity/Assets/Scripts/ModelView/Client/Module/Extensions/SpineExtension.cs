using FairyGUI;
using Spine;
using Spine.Unity;
using UnityEngine;

namespace ET
{
    public static class SpineExtension
    {
        public static ETTask PlayAsync(this SkeletonAnimation spine, string animationName)
        {
            ETTask task = ETTask.Create(true);
            void method(TrackEntry trackEntry) => task.SetResult();
            spine.AnimationState.SetAnimation(0, animationName, false).Complete += method;
            return task;
        }

        public static GTweener TweenFade(this SkeletonAnimation spine, float fade, float duration)
        {
            return GTween.To(spine.skeleton.A, fade, duration).SetTarget(spine.Skeleton).OnUpdate(static tweener => (tweener.target as Skeleton).A = tweener.value.x);
        }

        public static GTweener TweenColor(this SkeletonAnimation spine, Color color, float duration)
        {
            return GTween.To(spine.Skeleton.GetColor(), color, duration).SetTarget(spine.Skeleton).OnUpdate(static tweener => (tweener.target as Skeleton).SetColor(tweener.value.color));
        }
    }
}