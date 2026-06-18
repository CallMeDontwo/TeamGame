/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer5Component : GComponent
    {
        public const string URL = "ui://z89tvj9kppw61";
        public const string UIResName = "Layer5Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer5Component CreateInstance()
        {
            return (Layer5Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer5Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer5Component> tcs = new TaskCompletionSource<Layer5Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer5Component));
            return tcs.Task;
        }

        public static Layer5Component GetFromPool(GObject gObject)
        {
            return (Layer5Component)gObject;
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