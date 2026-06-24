namespace ET.TeamGame
{
    /// <summary>战斗单位类型</summary>
    public enum UnitType
    {
        None = 0,
        Hero = 1,
        Monster = 2,
        NPC = 3,
    }

    /// <summary>单位当前行为状态</summary>
    public enum UnitState
    {
        None = 0,
        Idle = 1,
        Move = 2,
        Attack = 3,
        Skill = 4,
        Hit = 5,
        Death = 6,
    }

    /// <summary>游戏流程阶段</summary>
    public enum GameFlowPhase
    {
        None = 0,
        BattlePrepare = 1,  // 布阵阶段
        Fighting = 2,       // 战斗中
    }
}
