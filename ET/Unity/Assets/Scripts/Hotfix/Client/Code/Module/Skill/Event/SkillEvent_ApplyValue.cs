namespace ET.TeamGame
{
    public static class SkillEvent_ApplyValue
    {
        public static void Execute(Unit caster, Unit target, SkillEventData config)
        {
            if (config.IntParam2 == 1)
            {
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
