namespace ET.TeamGame
{
    /// <summary>子弹飞行类型</summary>
    public enum BulletFlightType
    {
        Straight  = 1,  // 直线飞行
        Parabolic = 2,  // 抛物线飞行
    }

    /// <summary>子弹行为类型</summary>
    public enum BulletType
    {
        Normal   = 1,  // 普通子弹：击中销毁
        Ricochet = 2,  // 弹射子弹：击中后跳向下一个敌人
    }

    /// <summary>
    /// 子弹组件 — 挂载在子弹 Unit 上，仅存标识+配置引用
    /// 运行时数据已迁移到 BulletRuntimeData（BulletManagerComponent.Bullets 中）
    /// </summary>
    [ComponentOf(typeof(Unit))]
    [EnableMethod]
    public sealed partial class BulletComponent : Entity, IAwake, IDestroy
    {
        /// <summary>子弹配置ID（查 BulletDataStore）</summary>
        public int BulletConfigId { get; set; }

        /// <summary>发射者 Unit 引用</summary>
        public EntityRef<Unit> Caster { get; set; }

        /// <summary>预制体路径（视图层创建 GameObject 用）</summary>
        public string PrefabPath { get; set; }
    }
}
