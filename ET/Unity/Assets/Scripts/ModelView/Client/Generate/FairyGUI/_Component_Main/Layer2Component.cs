/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer2Component : GComponent
    {
        public const string URL = "ui://z89tvj9ktvqu2";
        public const string UIResName = "Layer2Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer2Component CreateInstance()
        {
            return (Layer2Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer2Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer2Component> tcs = new TaskCompletionSource<Layer2Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer2Component));
            return tcs.Task;
        }

        public static Layer2Component GetFromPool(GObject gObject)
        {
            return (Layer2Component)gObject;
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