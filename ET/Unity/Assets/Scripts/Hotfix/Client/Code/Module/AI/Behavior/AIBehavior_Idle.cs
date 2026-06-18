namespace ET.TeamGame
{
    public static class AIBehavior_Idle
    {
        public static void Execute(Unit unit)
        {
            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Idle);

            unit.GetComponent<MoveComponent>()?.Stop(true);
        }
    }
}
