using UnityEngine;

namespace ET
{

    [CreateAssetMenu(menuName = "ET/PhysicsConfig", fileName = "PhysicsConfig", order = 0)]
    public class PhysicsConfig : ScriptableObject
    {
        [Tooltip("下落重力")]
        public float DropGavity = -25;

        [Tooltip("常规重力")]
        public float NormalGavity = -6f;

        [Tooltip("重力阈值")]
        public float SleepVaule = 0.05f;

        [Tooltip("硬币动态摩檫力")]
        public float CoinDyFriction=0.1f;

        [Tooltip("硬币静态摩檫力")]
        public float CoinStaicFriction =0.1f;

        [Tooltip("硬币空气阻力")]
        public float CoinDrag=0.1f;

        [Tooltip("硬币角速度阻力")]
        public float CoinAngleDrag=10f;

        [Tooltip("杆弹力")]
        public float GanBouncine=0.3f;

        [Tooltip("杆碰撞加速")]
        public float GanAddFroce = 10f;
    }
}