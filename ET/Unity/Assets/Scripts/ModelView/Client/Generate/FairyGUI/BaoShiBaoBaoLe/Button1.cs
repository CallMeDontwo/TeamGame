/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class Button1 : GButton
    {
        public const string URL = "ui://oy07mpfwx7764w";
        public const string UIResName = "Button1";
        public const string UIPackageName = "宝石爆爆乐";


        public static Button1 CreateInstance()
        {
            return (Button1)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Button1> CreateInstanceAsync()
        {
            TaskCompletionSource<Button1> tcs = new TaskCompletionSource<Button1>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Button1));
            return tcs.Task;
        }

        public static Button1 GetFromPool(GObject gObject)
        {
            return (Button1)gObject;
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