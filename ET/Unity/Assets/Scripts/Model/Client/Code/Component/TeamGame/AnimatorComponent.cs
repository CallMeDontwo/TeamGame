namespace ET.TeamGame
{
    /// <summary>
    /// Spine 骨骼动画组件 — 挂载在 Unit 上，管理 2D 动画播放和朝向
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class AnimatorComponent : Entity, IAwake, IDestroy
    {
        /// <summary>Spine 骨骼资源路径（如 "Assets/Res/.../xxx_SkeletonData.asset"）</summary>
        public string SkeletonAsset { get; set; }

        /// <summary>当前播放的 Spine 动画名称</summary>
        public string CurrentAnimation { get; set; }

        /// <summary>朝向：1=右, -1=左</summary>
        public int Facing { get; set; } = 1;
    }
}
