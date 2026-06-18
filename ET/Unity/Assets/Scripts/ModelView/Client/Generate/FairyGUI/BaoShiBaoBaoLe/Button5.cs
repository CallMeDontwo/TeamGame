/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class Button5 : GButton
    {
        public const string URL = "ui://oy07mpfwx77657";
        public const string UIResName = "Button5";
        public const string UIPackageName = "宝石爆爆乐";


        public static Button5 CreateInstance()
        {
            return (Button5)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Button5> CreateInstanceAsync()
        {
            TaskCompletionSource<Button5> tcs = new TaskCompletionSource<Button5>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Button5));
            return tcs.Task;
        }

        public static Button5 GetFromPool(GObject gObject)
        {
            return (Button5)gObject;
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