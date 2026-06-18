using System.Collections.Generic;
using UnityEngine;

namespace ET.Client
{
    public enum MotionType
    {
        None,
        Idle,
        Run,
    }

    [ComponentOf]
    public class UnitAnimatorComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public Animator Animator;
        public readonly HashSet<string> Parameter = new();
        public readonly Dictionary<string, AnimationClip> AnimationClips = new();

        public MotionType MotionType;
        public float MontionSpeed;
        public bool isStop;
        public float stopSpeed;
    }
}