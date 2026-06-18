using ET.BaoShiBaoBaoLe;
using FairyGUI;

namespace ET.GemBoom
{

    internal class GemBoomComponentPanel : ViewPanel<GemBoomComponent>
    {
        public override void Init()
        {
            this.SelfUI.Bar_Progress.min = 0;
            this.SelfUI.Bar_Progress.max = 10;
            this.SelfUI.Bar_Progress.value = 0;
        }


        public async ETTask EnterGame()
        {
          await ETTask.CompletedTask;
        }

        public void RefreshData()
        {
        }

        private void ExitSetting()
        {
           
        }
    }
}