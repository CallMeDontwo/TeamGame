namespace ET.TeamGame
{
    /// <summary>技能事件：生成特效 — 发布事件让视图层加载并实例化VFX</summary>
    public static class SkillEvent_SpawnVFX
    {
        public static void Execute(Unit caster, SkillEventConfig config)
        {
            if (caster == null || caster.IsDisposed) return;

            // StringParam = 特效预制体路径
            // IntParam1 = X轴偏移, IntParam2 = Y轴偏移
            EventSystem.Instance.Publish(caster.Scene(), new SpawnSkillVFX
            {
                Caster = caster,
                PrefabPath = config.StringParam,
                OffsetX = config.IntParam1 / 100f,
                OffsetY = config.IntParam2 / 100f,
                dua = config.IntParam3 , 
            });
        }
    }
}
