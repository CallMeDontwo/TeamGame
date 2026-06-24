namespace ET.TeamGame
{
    [EntitySystemOf(typeof(StateComponent))]
    [FriendOf(typeof(StateComponent))]
    public static partial class StateComponentSystem
    {
        [EntitySystem]
        private static void Awake(this StateComponent self)
        {
            self.State = UnitState.None;
        }

        [EntitySystem]
        private static void Destroy(this StateComponent self)
        {
        }

        /// <summary>切换状态，若状态不变则跳过</summary>
        public static void ChangeState(this StateComponent self, UnitState newState)
        {
            //if (self.State == newState) return;
            UnitState oldState = self.State;
            self.State = newState;
            var unit = self.GetParent<Unit>();
            var animationComponent = unit.GetComponent<AnimatorComponent>();
            animationComponent.PlayWithState(newState);

            // 死亡时强制停止移动，避免移动协程继续拖动 unit
            if (newState == UnitState.Death)
            {
                unit.GetComponent<MoveComponent>()?.Stop(true);
            }

            EventSystem.Instance.Publish(self.Scene(), new UnitStateChanged() { Unit = unit, OldState = oldState, NewState = newState });
        }

        /// <summary>是否处于可行动状态（非 Death / None）</summary>
        public static bool IsAlive(this StateComponent self)
        {
            return self.State != UnitState.Death && self.State != UnitState.None;
        }
    }
}
