using FairyGUI;

namespace ET.ChaoJiMoFang
{
    public partial class Com_DieDieLeText
    {
        public async ETTask ChangeText(string text)
        {
            this.Text_OldText.text = this.Text_NewText.text;
            this.Text_NewText.text = text;
            await this.T_Show.PlayAsync();
        }
    }
}