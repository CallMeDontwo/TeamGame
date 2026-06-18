namespace ET.TeamGame
{
    public static class AIBehavior_Attack
    {
        public static void Execute(Unit unit, Unit target, AIConfig config)
        {
            if (target == null || target.IsDisposed) return;

            // 状态 + 停住
            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Attack);

            unit.GetComponent<MoveComponent>()?.Stop(true);

            // 面向目标
            unit.GetComponent<MoveComponent>()?.FaceDirection(target.Position - unit.Position);

            // 尝试攻击
            var attack = unit.GetComponent<AttackComponent>();
            attack?.TryExecute(target);
        }
    }
}
