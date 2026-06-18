/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer7Component : GComponent
    {
        public const string URL = "ui://z89tvj9kn0myb";
        public const string UIResName = "Layer7Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer7Component CreateInstance()
        {
            return (Layer7Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer7Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer7Component> tcs = new TaskCompletionSource<Layer7Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer7Component));
            return tcs.Task;
        }

        public static Layer7Component GetFromPool(GObject gObject)
        {
            return (Layer7Component)gObject;
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