namespace ET.TeamGame
{
    /// <summary>技能事件类型（对应 SkillEventConfig.EventType）</summary>
    public enum SkillEventType
    {
        PlayAnimation = 1,  // 播放动画
        FindTarget    = 2,  // 查找释放目标
        SpawnVFX      = 3,  // 生成特效
        ApplyValue    = 4,  // 应用数值（伤害/治疗）
        AddBuff       = 5,  // 添加Buff
        SpawnBullet   = 6,  // 发射子弹
    }
}