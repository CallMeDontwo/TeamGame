namespace ET.TeamGame
{
    [EntitySystemOf(typeof(AttackComponent))]
    [FriendOf(typeof(AttackComponent))]
    public static partial class AttackComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AttackComponent self)
        {
            self.AttackNextTime = 0;
        }

        [EntitySystem]
        private static void Destroy(this AttackComponent self)
        {
        }

        /// <summary>
        /// 尝试执行一次攻击：检查冷却 → 造成伤害
        /// </summary>
        public static void TryExecute(this AttackComponent self, Unit target)
        {
            if (target == null || target.IsDisposed) return;

            long now = TimeInfo.Instance.ClientFrameTime();
            if (now < self.AttackNextTime) return;

            var unit = self.GetParent<Unit>();
            var config = AIConfigCategory.Instance.Get(unit.GetComponent<AIComponent>().AIConfigId);
            int attackCD = config.CheckInterval;
            self.AttackNextTime = now + attackCD;

            ApplyDamage(unit, target);
        }

        /// <summary>伤害计算：damage = attacker.Attack - target.Defense/2，最少1点</summary>
        private static void ApplyDamage(Unit attacker, Unit target)
        {
            var attackerNum = attacker.GetComponent<NumericComponent>();
            var targetNum = target.GetComponent<NumericComponent>();
            if (attackerNum == null || targetNum == null) return;

            long attack = attackerNum.GetByKey(NumericType.Attack);
            long defense = targetNum.GetByKey(NumericType.Defense);
            long currentHP = targetNum.GetByKey(NumericType.HP);

            long damage = attack - defense / 2;
            if (damage < 1) damage = 1;

            long newHP = currentHP - damage;
            if (newHP < 0) newHP = 0;

            targetNum[NumericType.HP] = newHP;
        }
    }
}
