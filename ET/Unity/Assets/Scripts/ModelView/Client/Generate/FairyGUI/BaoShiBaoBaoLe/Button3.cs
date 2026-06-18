/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class Button3 : GButton
    {
        public const string URL = "ui://oy07mpfwx7765c";
        public const string UIResName = "Button3";
        public const string UIPackageName = "宝石爆爆乐";


        public static Button3 CreateInstance()
        {
            return (Button3)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Button3> CreateInstanceAsync()
        {
            TaskCompletionSource<Button3> tcs = new TaskCompletionSource<Button3>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Button3));
            return tcs.Task;
        }

        public static Button3 GetFromPool(GObject gObject)
        {
            return (Button3)gObject;
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