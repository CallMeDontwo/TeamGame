using Unity.Mathematics;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹运行时数据 — 扁平化 struct，不继承 Entity
    /// 存于 BulletManagerComponent.Bullets（List）中，每帧集中推进
    /// 脱离 Entity/Component 体系，零 GC 分配
    /// </summary>
    public struct BulletRuntimeData
    {
        /// <summary>当前位置（世界坐标）</summary>
        public float2 position;

        /// <summary>上一帧位置（CCD 线段碰撞检测用）</summary>
        public float2 prevPosition;

        /// <summary>速度向量（抛物线时 velocity.y 为垂直分量）</summary>
        public float2 velocity;

        /// <summary>碰撞半径</summary>
        public float radius;

        /// <summary>已飞行距离</summary>
        public float traveledDist;

        /// <summary>最大飞行距离（超出后销毁）</summary>
        public float maxDist;

        /// <summary>重力加速度（仅抛物线飞行使用）</summary>
        public float gravity;

        /// <summary>发射者 Unit 的 EntityId</summary>
        public long casterId;

        /// <summary>锁定追踪目标 Unit 的 EntityId</summary>
        public long targetId;

        /// <summary>伤害值</summary>
        public int damage;

        /// <summary>剩余弹射次数</summary>
        public int ricochetRemain;

        /// <summary>弹射搜索半径</summary>
        public float ricochetRadius;

        /// <summary>对应的子弹 Unit EntityId（视图层/销毁用）</summary>
        public long bulletUnitId;

        /// <summary>飞行类型：1=Straight 直线, 2=Parabolic 抛物线（同 BulletFlightType 枚举）</summary>
        public byte flightType;

        /// <summary>子弹类型：1=Normal 普通, 2=Ricochet 弹射（同 BulletType 枚举）</summary>
        public byte bulletType;

        /// <summary>是否锁定追踪目标</summary>
        public byte isHoming;

        /// <summary>是否活跃（0=已销毁/空闲槽位）</summary>
        public byte isActive;
    }

    /// <summary>
    /// 敌人碰撞快照 — 每帧从 UnitManager 提取一次
    /// 碰撞循环中零次 GetComponent 调用
    /// </summary>
    public struct EnemyCollisionSnapshot
    {
        /// <summary>身体中心（锚点 + 半高偏移）</summary>
        public float2 center;

        /// <summary>碰撞半径</summary>
        public float radius;

        /// <summary>对应的 Unit EntityId</summary>
        public long unitId;

        /// <summary>单位类型：1=Hero, 2=Monster（用于区分友方）</summary>
        public byte unitType;
    }

    /// <summary>
    /// 碰撞命中结果 — 一帧内所有命中的记录
    /// </summary>
    public struct HitResult
    {
        /// <summary>命中子弹在 Bullets 列表中的索引</summary>
        public int bulletIndex;

        /// <summary>被命中目标 Unit 的 EntityId</summary>
        public long targetId;

        /// <summary>造成的伤害值</summary>
        public int damage;
    }
}
