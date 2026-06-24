namespace ET.TeamGame
{
    /// <summary>
    /// 技能事件：应用数值（伤害/治疗）
    /// IntParam2 = 1 治疗自己；2 伤害 AoeTargets；其他 伤害当前目标
    /// </summary>
    public static class SkillEvent_ApplyValue
    {
        public static void Execute(Unit caster, SkillCastComponent castComp, SkillEventData config)
        {
            if (caster == null || caster.IsDisposed || castComp == null) return;

            // 1 = 治疗自己
            if (config.IntParam2 == 1)
            {
                HealSelf(caster, config.IntParam1);
                return;
            }

            // 2 = 伤害 AoeTargets（FindTarget 事件填充的目标集合）
            if (config.IntParam2 == 2)
            {
                DamageAoeTargets(castComp, config.IntParam1);
                return;
            }

            // 默认 = 伤害当前锁定的单个目标
            DamageTarget(castComp.Target, config.IntParam1);
        }

        private static void HealSelf(Unit caster, long value)
        {
            var numCaster = caster.GetComponent<NumericComponent>();
            if (numCaster == null) return;
            long currentHP = numCaster.GetByKey(NumericType.HP);
            long maxHP = numCaster.GetByKey(NumericType.MaxHP);
            long newHP = currentHP + value;
            if (newHP > maxHP) newHP = maxHP;
            numCaster[NumericType.HP] = newHP;
        }

        private static void DamageAoeTargets(SkillCastComponent castComp, long damage)
        {
            if (castComp.AoeTargets.Count == 0)
            {
                // 没有 AOE 目标时回退到单个目标
                DamageTarget(castComp.Target, damage);
                return;
            }

            foreach (var target in castComp.AoeTargets)
            {
                DamageTarget(target, damage);
            }
        }

        private static void DamageTarget(Unit target, long damage)
        {
            if (target == null || target.IsDisposed) return;
            var numTarget = target.GetComponent<NumericComponent>();
            if (numTarget == null) return;
            numTarget.TakeDamage(damage);
        }
    }
}
