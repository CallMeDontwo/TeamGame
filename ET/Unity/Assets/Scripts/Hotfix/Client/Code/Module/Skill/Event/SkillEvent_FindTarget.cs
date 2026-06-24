namespace ET.TeamGame
{
    /// <summary>
    /// 技能事件：查找受击目标集合（AOE目标）
    /// 通过 IntParam1 引用 TargetFinderConfigId，结果写入 SkillCastComponent.AoeTargets
    /// </summary>
    public static class SkillEvent_FindTarget
    {
        public static void Execute(Unit caster, SkillCastComponent castComp, SkillEventData config)
        {
            if (caster == null || caster.IsDisposed || castComp == null) return;

            castComp.AoeTargets.Clear();

            int finderConfigId = config.IntParam1;
            if (finderConfigId <= 0) return;

            if (!TargetFinderConfigCategory.Instance.Contain(finderConfigId))
            {
                Log.Error($"[SkillEvent_FindTarget] TargetFinderConfig not found: {finderConfigId}");
                return;
            }

            var finderCfg = TargetFinderConfigCategory.Instance.Get(finderConfigId);
            int reachHeight = castComp.CachedData?.ReachHeight ?? 0;
            TargetFinder.Find(caster, finderCfg, castComp.AoeTargets, reachHeight);
        }
    }
}
