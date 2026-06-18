using FairyGUI;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 3D 血条 — 挂载在 Unit GameObject 上，作为子 3D 物体用 FairyGUI UIPanel 渲染 Pro_hp
    /// </summary>
    [EnableClass]
    public class HPBarComponent : MonoBehaviour
    {
        private Pro_hp proHp;
        private UIPanel panel;
        private int maxHP;

        public bool IsCreated => proHp != null;

        public void Create(int maxHp, int currentHp)
        {
            if (proHp != null) return;

            // 创建 3D 子物体
            GameObject bar3D = new GameObject("HPBar3D");
            bar3D.transform.SetParent(transform, false);
            bar3D.transform.localPosition = new Vector3(-0.75f, 1.2f, 0);

            // 挂载 FairyGUI UIPanel
            panel = bar3D.AddComponent<UIPanel>();
            panel.packageName = Pro_hp.UIPackageName;
            panel.componentName = Pro_hp.UIResName;
            panel.sortingOrder = 1;
            panel.fitScreen = FitScreen.None;

            proHp = panel.ui as Pro_hp;
            if (proHp != null)
            {
                proHp.max = maxHp;
                proHp.value = currentHp;
                maxHP = maxHp;
                proHp.scale = new Vector2(0.5f, 0.5f);
            }

        }

        public void SetHP(int currentHp)
        {
            if (proHp == null) return;
            proHp.value = currentHp;
        }

        public void SetMaxHP(int newMaxHp)
        {
            if (proHp == null) return;
            maxHP = newMaxHp;
            proHp.max = maxHP;
        }

        private void OnDestroy()
        {
            if (panel != null)
            {
                Destroy(panel);
                panel = null;
            }
            proHp?.Dispose();
            proHp = null;
        }
    }
}
