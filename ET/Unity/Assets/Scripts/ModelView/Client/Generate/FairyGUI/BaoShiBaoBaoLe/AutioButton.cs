/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class AutioButton : GButton
    {
        public const string URL = "ui://oy07mpfwggki20";
        public const string UIResName = "AutioButton";
        public const string UIPackageName = "宝石爆爆乐";


        public static AutioButton CreateInstance()
        {
            return (AutioButton)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<AutioButton> CreateInstanceAsync()
        {
            TaskCompletionSource<AutioButton> tcs = new TaskCompletionSource<AutioButton>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as AutioButton));
            return tcs.Task;
        }

        public static AutioButton GetFromPool(GObject gObject)
        {
            return (AutioButton)gObject;
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