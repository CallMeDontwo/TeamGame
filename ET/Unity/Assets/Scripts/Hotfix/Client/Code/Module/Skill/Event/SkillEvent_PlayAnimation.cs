namespace ET.TeamGame
{
    /// <summary>技能事件：播放动画</summary>
    public static class SkillEvent_PlayAnimation
    {
        public static void Execute(Unit caster, SkillEventData config)
        {
            var anim = caster.GetComponent<AnimatorComponent>();
            if (anim != null && !string.IsNullOrEmpty(config.StringParam))
            {
                anim.Play(config.StringParam,false);
            }
        }
    }
}
