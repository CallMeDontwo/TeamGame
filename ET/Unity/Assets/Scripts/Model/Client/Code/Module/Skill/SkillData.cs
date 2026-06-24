using System;
using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能事件数据（JSON 序列化）
    /// 替代 SkillEventConfig 表，技能编辑器直写 JSON
    /// </summary>
    [Serializable]
    public class SkillEventData
    {
        /// <summary>事件类型（对应 SkillEventType 枚举）</summary>
        public int EventType;

        /// <summary>事件触发时间戳 (ms)</summary>
        public int Timestamp;

        /// <summary>整型参数1（含义依 EventType 而定）</summary>
        public int IntParam1;

        /// <summary>整型参数2</summary>
        public int IntParam2;

        /// <summary>整型参数3</summary>
        public int IntParam3;

        /// <summary>浮点参数1（范围类参数用，如半径/长度）</summary>
        public float FloatParam1;

        /// <summary>浮点参数2（范围类参数用，如矩形宽度/扇形角度）</summary>
        public float FloatParam2;

        /// <summary>浮点参数3（范围类参数用，如中心点偏移X）</summary>
        public float FloatParam3;

        /// <summary>浮点参数4（范围类参数用，如中心点偏移Y）</summary>
        public float FloatParam4;

        /// <summary>字符串参数（动画名/特效路径等）</summary>
        public string StringParam;
    }

    /// <summary>
    /// 技能配置数据（JSON 序列化）
    /// 替代 SkillConfig 表，技能编辑器直写 JSON 到 Resources/Config/Skills/
    /// </summary>
    [Serializable]
    public class SkillData
    {
        /// <summary>技能ID</summary>
        public int SkillId;

        /// <summary>技能名称</summary>
        public string Name;

        /// <summary>技能描述</summary>
        public string Desc;

        /// <summary>冷却时间（秒）</summary>
        public int CD;

        /// <summary>技能总时长（毫秒）</summary>
        public int Duration;

        /// <summary>施法距离（int/100 = 世界单位，0=不限）</summary>
        public int CastRange;

        /// <summary>攻击可达高度（int/100，0=仅同层，500=5层差内均可）</summary>
        public int ReachHeight;

        /// <summary>事件列表（按 Timestamp 排序）</summary>
        public List<SkillEventData> Events = new();
    }
}
