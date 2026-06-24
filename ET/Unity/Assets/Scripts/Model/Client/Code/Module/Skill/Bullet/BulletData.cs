using System;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹配置数据（JSON 序列化）
    /// 替代 BulletConfig 表，子弹编辑器直写 JSON 到 Assets/Bundles/Bullet/
    /// </summary>
    [Serializable]
    [EnableClass]
    public class BulletData
    {
        /// <summary>子弹配置ID</summary>
        public int Id;

        /// <summary>子弹名称</summary>
        public string BulletName;

        /// <summary>描述</summary>
        public string Desc;

        /// <summary>速度（int/10000 = 世界单位/秒）</summary>
        public int Speed;

        /// <summary>伤害值</summary>
        public int Damage;

        /// <summary>飞行类型: 1=直线(Straight), 2=抛物线(Parabolic)</summary>
        public int FlightType;

        /// <summary>飞行参数（抛物线: FlightValue[0]=发射角度(度)）</summary>
        public int[] FlightValue;

        /// <summary>子弹类型: 1=普通(Normal), 2=弹射(Ricochet)</summary>
        public int BulletType;

        /// <summary>弹射参数（[0]=弹射次数, [1]=搜索半径(int/10000)）</summary>
        public int[] BulletTypeValue;

        /// <summary>锁定追踪: 0=否, 1=是</summary>
        public int IsHoming;

        /// <summary>最大飞行距离（int/10000 = 世界单位）</summary>
        public int MaxDistance;

        /// <summary>寻敌配置ID（0=使用施法者当前目标）</summary>
        public int TargetFinderId;

        /// <summary>碰撞半径（int/100 = 世界单位）</summary>
        public int CollisionRadius;

        /// <summary>预制体路径（如 "Bullet_Track"）</summary>
        public string PrefabPath;

        /// <summary>发射点X偏移（int/100 = 世界单位）</summary>
        public int SpawnOffsetX;

        /// <summary>发射点Y偏移（int/100 = 世界单位，0=脚底，正=向上）</summary>
        public int SpawnOffsetY;
    }
}
