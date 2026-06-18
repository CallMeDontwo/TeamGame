using FairyGUI;
using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class UIPanelContentScaler : MonoBehaviour
    {
        private int height;
        private float cameraSize;
        private UIPanel panel;

        private void Awake()
        {
            this.height = Screen.height;
            this.cameraSize = Camera.main.orthographicSize;
            this.panel = this.GetComponent<UIPanel>();
        }

        private void OnEnable()
        {
            this.ApplyChange();
        }

        private void Update()
        {
            if (this.height != Screen.height || this.cameraSize != Camera.main.orthographicSize)
            {
                this.ApplyChange();
            }
        }

        public void ApplyChange()
        {
            this.height = Screen.height;
            this.cameraSize = Camera.main.orthographicSize;
            this.panel.container.scale = 2 * this.cameraSize / this.height * UIContentScaler.scaleFactor * Vector2.one;
        }
    }
}