using ET._Component_Public;
using FairyGUI;

namespace ET
{
    internal sealed class LoadingWindow : ViewWindow<LoadingWindow, LoadingComponent>
    {
        public override void Init()
        {
            base.Init();
            this.Window.MakeFullScreen();
            this.Window.AddRelation(GRoot.inst, RelationType.Size);
        }
    }
}