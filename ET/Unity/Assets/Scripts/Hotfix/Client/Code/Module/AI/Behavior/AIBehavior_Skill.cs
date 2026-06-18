namespace ET.TeamGame
{
    public static class AIBehavior_Skill
    {
        public static async ETTask<bool> TryExecute(Unit unit, Unit target)
        {
            var skillComp = unit.GetComponent<SkillComponent>();
            if (skillComp == null || !skillComp.HasSkill()) return false;

            int readySkillId = skillComp.GetFirstReadySkill();
            if (readySkillId == 0) return false;

            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Skill);

           await skillComp.Cast(readySkillId, target);
            return true;
        }
    }
}
