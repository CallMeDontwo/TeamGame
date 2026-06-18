/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Label_DieDieLeTime : GLabel
    {
        public const string URL = "ui://hoqdodepvdhu5j";
        public const string UIResName = "Label_DieDieLeTime";
        public const string UIPackageName = "超级魔方";

        public GTextField Text_Seconds { get; private set; }

        public static Label_DieDieLeTime CreateInstance()
        {
            return (Label_DieDieLeTime)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Label_DieDieLeTime> CreateInstanceAsync()
        {
            TaskCompletionSource<Label_DieDieLeTime> tcs = new TaskCompletionSource<Label_DieDieLeTime>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Label_DieDieLeTime));
            return tcs.Task;
        }

        public static Label_DieDieLeTime GetFromPool(GObject gObject)
        {
            return (Label_DieDieLeTime)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_Seconds = (GTextField)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}