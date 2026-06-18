/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class Btn_Exit : GButton
    {
        public const string URL = "ui://7am1s0tjl6ot4u";
        public const string UIResName = "Btn_Exit";
        public const string UIPackageName = "_Component_Public";

        public GTextField Text_Exit { get; private set; }

        public static Btn_Exit CreateInstance()
        {
            return (Btn_Exit)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Btn_Exit> CreateInstanceAsync()
        {
            TaskCompletionSource<Btn_Exit> tcs = new TaskCompletionSource<Btn_Exit>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Btn_Exit));
            return tcs.Task;
        }

        public static Btn_Exit GetFromPool(GObject gObject)
        {
            return (Btn_Exit)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_Exit = (GTextField)GetChildAt(1);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}