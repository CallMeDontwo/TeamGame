namespace ET.TeamGame
{
    /// <summary>
    /// 技能事件：发射子弹
    /// IntParam1 = BulletConfigId（指向 BulletConfig 表）
    /// </summary>
    public static class SkillEvent_SpawnBullet
    {
        public static void Execute(Unit caster, SkillEventConfig config)
        {
            if (caster == null || caster.IsDisposed) return;

            int bulletConfigId = config.IntParam1;
            if (bulletConfigId <= 0) return;

            // 异步创建子弹（不等待，子弹独立飞行）
            BulletFactory.CreateBullet(caster, bulletConfigId).Coroutine();
        }
    }
}
