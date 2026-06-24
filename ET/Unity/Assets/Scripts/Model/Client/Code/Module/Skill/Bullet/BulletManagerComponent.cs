using System.Collections.Generic;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹管理器 — Scene 级组件，替代 N 个独立协程
    /// 所有子弹的飞行/碰撞/命中等逻辑在此集中处理（BulletManagerSystem.Update）
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public sealed partial class BulletManagerComponent : Entity, IAwake, IDestroy, IUpdate
    {
        /// <summary>所有子弹运行时数据（托管 List，struct 不装箱）</summary>
        public List<BulletRuntimeData> Bullets;

        /// <summary>空闲槽位索引队列（复用已销毁子弹的槽位）</summary>
        public Queue<int> FreeSlots;

        /// <summary>空间哈希网格：cellKey → 该 cell 内子弹索引列表</summary>
        public Dictionary<int, List<int>> BulletGrid;

        /// <summary>空间哈希网格：cellKey → 该 cell 内敌人索引列表</summary>
        public Dictionary<int, List<int>> EnemyGrid;

        /// <summary>本帧敌人碰撞快照（从 UnitManager 提取一次）</summary>
        public List<EnemyCollisionSnapshot> EnemiesSnapshot;

        /// <summary>本帧命中结果列表</summary>
        public List<HitResult> HitsThisFrame;

        /// <summary>网格 cell 大小（world units）</summary>
        public float GridCellSize;

        /// <summary>固定步长时间累积器</summary>
        public float TimeAccumulator;

        /// <summary>上一帧时间戳（ms）</summary>
        public long LastUpdateTime;

       
    }
}
