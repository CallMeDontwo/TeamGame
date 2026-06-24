namespace ET.TeamGame
{
    /// <summary>
    /// 战斗工具类 — 统一伤害/治疗入口
    /// </summary>
    public static class CombatHelper
    {
        /// <summary>
        /// 对自身造成伤害。死亡单位免疫。返回实际伤害值。
        /// </summary>
        public static long TakeDamage(this NumericComponent self, long rawDamage)
        {
            if (rawDamage <= 0) return 0;

            var state = self.GetParent<Unit>().GetComponent<StateComponent>();
            if (state == null || !state.IsAlive()) return 0;

            long currentHP = self.GetAsLong(NumericType.HP);
            long newHP = currentHP - rawDamage;
            if (newHP < 0) newHP = 0;
            self.Set(NumericType.HP, newHP);
            return rawDamage;
        }
    }
}
