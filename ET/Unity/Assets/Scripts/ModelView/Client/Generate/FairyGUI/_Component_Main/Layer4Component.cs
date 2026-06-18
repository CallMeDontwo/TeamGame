/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer4Component : GComponent
    {
        public const string URL = "ui://z89tvj9ksnyy8";
        public const string UIResName = "Layer4Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer4Component CreateInstance()
        {
            return (Layer4Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer4Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer4Component> tcs = new TaskCompletionSource<Layer4Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer4Component));
            return tcs.Task;
        }

        public static Layer4Component GetFromPool(GObject gObject)
        {
            return (Layer4Component)gObject;
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