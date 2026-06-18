/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Label_DieDieLeCoins : GLabel
    {
        public const string URL = "ui://hoqdodepvdhu5i";
        public const string UIResName = "Label_DieDieLeCoins";
        public const string UIPackageName = "超级魔方";


        public static Label_DieDieLeCoins CreateInstance()
        {
            return (Label_DieDieLeCoins)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Label_DieDieLeCoins> CreateInstanceAsync()
        {
            TaskCompletionSource<Label_DieDieLeCoins> tcs = new TaskCompletionSource<Label_DieDieLeCoins>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Label_DieDieLeCoins));
            return tcs.Task;
        }

        public static Label_DieDieLeCoins GetFromPool(GObject gObject)
        {
            return (Label_DieDieLeCoins)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}