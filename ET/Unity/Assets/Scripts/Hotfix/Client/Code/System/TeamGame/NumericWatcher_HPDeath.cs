namespace ET.TeamGame
{
    /// <summary>
    /// 监听 HP 变化，HP ≤ 0 时切 Death 状态
    /// </summary>
    [NumericWatcher(SceneType.Current, NumericType.HP)]
    public class NumericWatcher_HPDeath : INumericWatcher
    {
        public void Run(Unit unit, NumbericChange args)
        {
            if (args.New <= 0)
            {
                var state = unit.GetComponent<StateComponent>();
                if (state != null && state.IsAlive())
                {
                    state.ChangeState(UnitState.Death);
                }
            }
        }
    }
}
