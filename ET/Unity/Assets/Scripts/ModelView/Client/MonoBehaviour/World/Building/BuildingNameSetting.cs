using FairyGUI;
using UnityEngine;

namespace ET
{
    [EnableClass]
    [ExecuteAlways]
    public class BuildingNameSetting : MonoBehaviour, EMRenderTarget
    {
        public string Name;
        public int Type;

        private void Start()
        {
            UIPanel panel = this.GetComponentInChildren<UIPanel>();
            if (panel && panel.ui != null)
            {
                panel.ui.GetChild("Text_Name").asTextField.text = this.Name;
                panel.ui.GetController("C_Type").selectedIndex = this.Type;
                panel.container.scale = 2 * 9.6f / Screen.height * Vector2.one;
            }
        }

#if UNITY_EDITOR
        private void OnEnable()
        {
            EMRenderSupport.Add(this);
        }

        private void OnDisable()
        {
            EMRenderSupport.Remove(this);
        }

        private void OnValidate()
        {
            this.Start();
        }
#endif

        public int EM_sortingOrder => this.GetComponentInChildren<UIPanel>().EM_sortingOrder + 1;

        public void EM_BeforeUpdate()
        {
            this.Start();
        }

        public void EM_Reload()
        {
            this.Start();
        }

        public void EM_Update(UpdateContext context)
        {
        }
    }
}