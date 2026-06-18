namespace ET.TeamGame
{
    /// <summary>
    /// 监听数值变化 → 更新 3D 血条
    /// </summary>
    [Event(SceneType.Current)]
    internal class NumbericChange_UpdateHPBar : AEvent<Scene, NumbericChange>
    {
        protected override async ETTask Run(Scene scene, NumbericChange a)
        {
            if (a.NumericType != NumericType.HP) return;

            var unit = a.Unit;
            if (unit.IsDisposed) return;

            if (!unit.TryGetComponent(out UnitGameObjectComponent view) || view.GameObject == null)
                return;

            var hpBar = view.GameObject.GetComponent<HPBarComponent>();
            if (hpBar == null) return;

            int currentHp = (int)a.New;
            hpBar.SetHP(currentHp);

            await ETTask.CompletedTask;
        }
    }
}
