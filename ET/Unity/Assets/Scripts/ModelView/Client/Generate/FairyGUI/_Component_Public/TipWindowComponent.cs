/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class TipWindowComponent : GComponent
    {
        public const string URL = "ui://7am1s0tjr8g34v";
        public const string UIResName = "TipWindowComponent";
        public const string UIPackageName = "_Component_Public";

        public GGraph Graph_Bg { get; private set; }
        public GTextField Text_context { get; private set; }
        public _Component_Public.Btn_Normal Btn_OK { get; private set; }

        public static TipWindowComponent CreateInstance()
        {
            return (TipWindowComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<TipWindowComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<TipWindowComponent> tcs = new TaskCompletionSource<TipWindowComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as TipWindowComponent));
            return tcs.Task;
        }

        public static TipWindowComponent GetFromPool(GObject gObject)
        {
            return (TipWindowComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Graph_Bg = (GGraph)GetChildAt(0);
            Text_context = (GTextField)GetChildAt(2);
            Btn_OK = (_Component_Public.Btn_Normal)GetChildAt(3);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}