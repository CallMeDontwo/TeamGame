/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Label_DieIcon : GLabel
    {
        public const string URL = "ui://hoqdodepvdhu5g";
        public const string UIResName = "Label_DieIcon";
        public const string UIPackageName = "超级魔方";


        public static Label_DieIcon CreateInstance()
        {
            return (Label_DieIcon)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Label_DieIcon> CreateInstanceAsync()
        {
            TaskCompletionSource<Label_DieIcon> tcs = new TaskCompletionSource<Label_DieIcon>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Label_DieIcon));
            return tcs.Task;
        }

        public static Label_DieIcon GetFromPool(GObject gObject)
        {
            return (Label_DieIcon)gObject;
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