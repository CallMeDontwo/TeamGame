/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class Btn_Normal : GButton
    {
        public const string URL = "ui://7am1s0tj995rq9t";
        public const string UIResName = "Btn_Normal";
        public const string UIPackageName = "_Component_Public";


        public static Btn_Normal CreateInstance()
        {
            return (Btn_Normal)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Btn_Normal> CreateInstanceAsync()
        {
            TaskCompletionSource<Btn_Normal> tcs = new TaskCompletionSource<Btn_Normal>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Btn_Normal));
            return tcs.Task;
        }

        public static Btn_Normal GetFromPool(GObject gObject)
        {
            return (Btn_Normal)gObject;
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