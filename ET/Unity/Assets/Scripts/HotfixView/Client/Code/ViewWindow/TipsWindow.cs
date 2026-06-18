using ET._Component_Public;
using FairyGUI;

namespace ET
{
    internal sealed class TipsWindow : ViewWindow<TipsWindow, TipWindowComponent>
    {
        public override void Init()
        {
            base.Init();
            this.Window.MakeFullScreen();
            this.Window.Center(true);
            this.Window.AddRelation(GRoot.inst, RelationType.Size);
        }

        public override void Show(object args)
        {
            this.SelfUI.Text_context.text = args.ToString();
            this.SelfUI.Btn_OK.onClick.Set(this.Hide);
            this.Show();
        }

        public ETTask ShowTip(string text)
        {
            ETTask task = ETTask.Create(true);
            this.SelfUI.Text_context.text = text;
            this.SelfUI.Btn_OK.onClick.Set(() => task.SetResult());
            this.SelfUI.Btn_OK.onClick.Add(this.Hide);
            this.Show();
            return task;
        }
    }
}