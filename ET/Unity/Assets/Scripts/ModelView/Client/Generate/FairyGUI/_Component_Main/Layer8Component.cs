/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer8Component : GComponent
    {
        public const string URL = "ui://z89tvj9kn0myc";
        public const string UIResName = "Layer8Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer8Component CreateInstance()
        {
            return (Layer8Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer8Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer8Component> tcs = new TaskCompletionSource<Layer8Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer8Component));
            return tcs.Task;
        }

        public static Layer8Component GetFromPool(GObject gObject)
        {
            return (Layer8Component)gObject;
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