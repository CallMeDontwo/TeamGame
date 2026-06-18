namespace ET.TeamGame
{
    /// <summary>目标查找器 — 根据 TargetFinderConfig 寻找目标</summary>
    public static class TargetFinder
    {
        /// <summary>根据配置查找目标</summary>
        public static Unit Find(Unit caster, TargetFinderConfig config)
        {
            if (caster == null || caster.IsDisposed || config == null) return null;

            return config.FinderType switch
            {
                1 => FindCurrentTarget(caster),       // 当前攻击目标
                // 2 = 最近敌人
                // 3 = 最弱敌人
                // ... 后续扩展
                _ => null,
            };
        }

        /// <summary>查找当前技能释放目标</summary>
        private static Unit FindCurrentTarget(Unit caster)
        {
            var castComp = caster.GetComponent<SkillCastComponent>();
            Unit castCompTarget = castComp.Target;
            if (castComp == null || castCompTarget == null) return null;
            var target = castCompTarget;
            if (target.IsDisposed) return null;
            return target;
        }
    }
}
