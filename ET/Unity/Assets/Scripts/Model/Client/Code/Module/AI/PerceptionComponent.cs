using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 感知组件 — 挂载在 Unit 上，负责扫描周围可见敌人
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class PerceptionComponent : Entity, IAwake, IDestroy
    {
        /// <summary>视野范围（从 AIConfig 读取，使用时 /100）</summary>
        public int SightRange { get; set; }

        /// <summary>可见 Unit 的 InstanceId 列表（通过 AOI 扫描）</summary>
        public List<long> VisibleTargets { get; set; } = new();

        /// <summary>当前主目标 Unit 的 InstanceId</summary>
        public long PrimaryTargetId { get; set; }

        /// <summary>上次扫描时间戳</summary>
        public long LastScanTime { get; set; }

        /// <summary>扫描间隔（ms）</summary>
        public int ScanInterval { get; set; } = 100;
    }
}
