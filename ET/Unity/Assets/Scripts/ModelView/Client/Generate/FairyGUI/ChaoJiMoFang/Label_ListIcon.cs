/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Label_ListIcon : GLabel
    {
        public const string URL = "ui://hoqdodepd8z852";
        public const string UIResName = "Label_ListIcon";
        public const string UIPackageName = "超级魔方";

        public Transition T_Shiny { get; private set; }

        public static Label_ListIcon CreateInstance()
        {
            return (Label_ListIcon)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Label_ListIcon> CreateInstanceAsync()
        {
            TaskCompletionSource<Label_ListIcon> tcs = new TaskCompletionSource<Label_ListIcon>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Label_ListIcon));
            return tcs.Task;
        }

        public static Label_ListIcon GetFromPool(GObject gObject)
        {
            return (Label_ListIcon)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            T_Shiny = GetTransitionAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}