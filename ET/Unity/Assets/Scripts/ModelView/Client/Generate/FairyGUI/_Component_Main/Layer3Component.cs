/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer3Component : GComponent
    {
        public const string URL = "ui://z89tvj9ksnyy7";
        public const string UIResName = "Layer3Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer3Component CreateInstance()
        {
            return (Layer3Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer3Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer3Component> tcs = new TaskCompletionSource<Layer3Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer3Component));
            return tcs.Task;
        }

        public static Layer3Component GetFromPool(GObject gObject)
        {
            return (Layer3Component)gObject;
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