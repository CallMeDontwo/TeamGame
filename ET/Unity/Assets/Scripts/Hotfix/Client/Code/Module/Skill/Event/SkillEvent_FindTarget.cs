namespace ET.TeamGame
{
    /// <summary>技能事件：查找释放目标（占位，后续实现目标筛选逻辑）</summary>
    public static class SkillEvent_FindTarget
    {
        public static void Execute(Unit caster, SkillEventConfig config)
        {
            // TODO: 根据 IntParam1（目标类型）查找/切换目标
            // 当前暂用 AI 感知里的 PrimaryTarget
        }
    }
}
