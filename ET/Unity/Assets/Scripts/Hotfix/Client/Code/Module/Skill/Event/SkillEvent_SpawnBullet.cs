namespace ET.TeamGame
{
    public static class SkillEvent_SpawnBullet
    {
        public static void Execute(Unit caster, SkillEventData config)
        {
            if (caster == null || caster.IsDisposed) return;
            int bulletConfigId = config.IntParam1;
            if (bulletConfigId <= 0) return;
            BulletFactory.CreateBullet(caster, bulletConfigId).Coroutine();
        }
    }
}
