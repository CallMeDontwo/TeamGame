using Unity.Mathematics;

namespace ET
{
    public struct ChangePosition
    {
        public Unit Unit;
        public float3 OldPos;
    }

    public struct ChangeRotation
    {
        public Unit Unit;
    }

    /// <summary>英雄 Unit 创建完成（逻辑层发布，视图层监听后创建 GameObject）</summary>
    public struct AfterHeroCreate
    {
        public Unit Unit;
    }

    /// <summary>怪物 Unit 创建完成（逻辑层发布，视图层监听后创建 GameObject）</summary>
    public struct AfterMonsterCreate
    {
        public Unit Unit;
    }

    /// <summary>Unit 状态切换（逻辑层发布，视图层监听后切换动画）</summary>
    public struct UnitStateChanged
    {
        public Unit Unit;
        public ET.TeamGame.UnitState OldState;
        public ET.TeamGame.UnitState NewState;
    }

    public struct ViewPlayAnimation
    {
        public Unit Unit;
        public string anime;
        public bool isLoop;
    }

    /// <summary>技能生成特效（逻辑层发布，视图层监听后加载并实例化VFX）</summary>
    public struct SpawnSkillVFX
    {
        public Unit Caster;
        public string PrefabPath;   // 特效预制体路径
        public float OffsetX;       // X轴偏移
        public float OffsetY;       // Y轴偏移
        public long Duration;     // 持续时间（毫秒）
    }

    /// <summary>子弹创建完成（逻辑层发布，视图层监听后创建子弹的 GameObject）</summary>
    public struct AfterBulletCreate
    {
        public Unit BulletUnit;
    }

    /// <summary>AOE 寻敌完成（逻辑层发布，视图层监听后绘制形状 1 秒）</summary>
    public struct AfterAoeFindTarget
    {
        public int ShapeType;       // 2=圆形，3=矩形，4=扇形
        public float3 Center;
        public float2 Forward;
        public float Param1;        // 半径/长度/扇形半径
        public float Param2;        // 矩形宽度/扇形角度（度）
    }
}