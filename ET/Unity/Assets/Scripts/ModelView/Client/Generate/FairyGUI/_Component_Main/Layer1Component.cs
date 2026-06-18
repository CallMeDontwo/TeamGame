/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer1Component : GComponent
    {
        public const string URL = "ui://z89tvj9kgpi10";
        public const string UIResName = "Layer1Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer1Component CreateInstance()
        {
            return (Layer1Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer1Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer1Component> tcs = new TaskCompletionSource<Layer1Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer1Component));
            return tcs.Task;
        }

        public static Layer1Component GetFromPool(GObject gObject)
        {
            return (Layer1Component)gObject;
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