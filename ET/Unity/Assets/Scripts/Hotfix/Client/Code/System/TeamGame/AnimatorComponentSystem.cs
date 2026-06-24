namespace ET.TeamGame
{
    [EntitySystemOf(typeof(AnimatorComponent))]
    [FriendOf(typeof(AnimatorComponent))]
    public static partial class AnimatorComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AnimatorComponent self)
        {
            self.SkeletonAsset    = null;
            self.CurrentAnimation = null;
            self.Facing           = 1;
        }

        [EntitySystem]
        private static void Destroy(this AnimatorComponent self)
        {
            self.SkeletonAsset    = null;
            self.CurrentAnimation = null;
        }

        /// <summary>从 HeroConfig / MonsterConfig 加载动画数据（TODO）</summary>
        public static void LoadFromConfig(this AnimatorComponent self, int configId)
        {
            //self.SkeletonAsset    = ...;
            //self.CurrentAnimation = ...;
        }

        /// <summary>播放动画</summary>
        public static void Play(this AnimatorComponent self, string anime,bool isLoop)
        {
            if (string.IsNullOrEmpty(anime)) return;
            if (anime == self.CurrentAnimation&&isLoop ==true) return;
            self.CurrentAnimation = anime;
            // View 层监听 CurrentAnimation 变化来驱动 Spine 播放
            EventSystem.Instance.Publish(self.Scene(), new ViewPlayAnimation() { Unit = self.GetParent<Unit>(), anime = self.CurrentAnimation,isLoop = isLoop });
        }

        public static  void PlayWithState(this AnimatorComponent self, UnitState state)
        {
            var data = AnimNameFromState(state);
            self.Play(data.Item1,data.Item2);
        }

        /// <summary>设置朝向（1=右, -1=左）</summary>
        public static void SetFacing(this AnimatorComponent self, int facing)
        {
            facing = facing >= 0 ? 1 : -1;
            if (self.Facing == facing) return;
            self.Facing = facing;
            // View 层监听 Facing 变化驱动 Spine Skeleton.scaleX
        }

        private static (string, bool) AnimNameFromState(UnitState state)
        {
            return state switch
            {
                UnitState.Idle => ("idle",true),
                UnitState.Move => ("move",true),
                UnitState.Hit => ("hit", false),
                UnitState.Death => ("death", false),
                _ => (null,false),
            };
        }
    }
}
