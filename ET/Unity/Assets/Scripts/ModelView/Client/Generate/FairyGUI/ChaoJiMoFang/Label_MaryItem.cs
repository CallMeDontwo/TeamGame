/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Label_MaryItem : GLabel
    {
        public const string URL = "ui://hoqdodepd8z853";
        public const string UIResName = "Label_MaryItem";
        public const string UIPackageName = "超级魔方";

        public Controller C_Selected { get; private set; }

        public static Label_MaryItem CreateInstance()
        {
            return (Label_MaryItem)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Label_MaryItem> CreateInstanceAsync()
        {
            TaskCompletionSource<Label_MaryItem> tcs = new TaskCompletionSource<Label_MaryItem>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Label_MaryItem));
            return tcs.Task;
        }

        public static Label_MaryItem GetFromPool(GObject gObject)
        {
            return (Label_MaryItem)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Selected = GetControllerAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}