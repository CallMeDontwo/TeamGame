namespace ET.TeamGame
{
    public static class AIBehavior_Attack
    {
        public static async ETTask Execute(Unit unit, Unit target, AIConfig config)
        {
            if (target == null || target.IsDisposed) return;

            // 状态 + 停住
            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Attack);

            unit.GetComponent<MoveComponent>()?.Stop(true);

            // 面向目标
            unit.GetComponent<MoveComponent>()?.FaceDirection(target.Position - unit.Position);

            // 通过技能系统释放普攻
            var attack = unit.GetComponent<AttackComponent>();
            var skillComp = unit.GetComponent<SkillComponent>();
            if (attack == null || skillComp == null || attack.BasicAttackSkillId == 0) return;

            await skillComp.Cast(attack.BasicAttackSkillId, target);

        }
    }
}
