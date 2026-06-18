using Spine.Unity;
using UnityEngine;

namespace ET
{
    [EnableClass]
    public class MachineController : MonoBehaviour
    {
        private const string ON = "ON";
        private const string OFF = "OFF";

        private int style;
        public int Style
        {
            get => this.style;
            set => this.SetStyle(value);
        }

        private bool enable;
        public bool Enable
        {
            get => this.enable;
            set => this.SetEnable(value);
        }

        public void SetStyle(int style)
        {
            this.style = style;
            this.UpdateStyle();
        }

        public void SetEnable(bool enable)
        {
            this.enable = enable;
            this.UpdateStyle();
        }

        private void UpdateStyle()
        {
            SkeletonAnimation animation = this.GetComponent<SkeletonAnimation>();
            animation.AnimationName = $"Arcade{this.style}_{(this.enable ? ON : OFF)}";
        }
    }
}