namespace ET.TeamGame
{
    public static class SkillEvent_SpawnVFX
    {
        public static void Execute(Unit caster, SkillEventData config)
        {
            if (caster == null || caster.IsDisposed) return;
            EventSystem.Instance.Publish(caster.Scene(), new SpawnSkillVFX
            {
                Caster = caster,
                PrefabPath = config.StringParam,
                OffsetX = config.IntParam1 / 100f,
                OffsetY = config.IntParam2 / 100f,
                Duration = config.IntParam3,
            });
        }
    }
}
