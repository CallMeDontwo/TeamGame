namespace ET.TeamGame
{
    /// <summary>技能事件：应用数值（伤害 / 治疗）</summary>
    /// <remarks>
    /// IntParam1 = 数值
    /// IntParam2 = 0 伤害 / 1 治疗
    /// </remarks>
    public static class SkillEvent_ApplyValue
    {
        public static void Execute(Unit caster, Unit target, SkillEventConfig config)
        {
            if (config.IntParam2 == 1)
            {
                // 治疗
                var numCaster = caster.GetComponent<NumericComponent>();
                if (numCaster == null) return;

                long currentHP = numCaster.GetByKey(NumericType.HP);
                long maxHP = numCaster.GetByKey(NumericType.MaxHP);
                long newHP = currentHP + config.IntParam1;
                if (newHP > maxHP) newHP = maxHP;
                numCaster[NumericType.HP] = newHP;
            }
            else
            {
                // 伤害
                if (target == null || target.IsDisposed) return;
                var numTarget = target.GetComponent<NumericComponent>();
                if (numTarget == null) return;

                long currentHP = numTarget.GetByKey(NumericType.HP);
                long newHP = currentHP - config.IntParam1;
                if (newHP < 0) newHP = 0;
                numTarget[NumericType.HP] = newHP;
            }
        }
    }
}
