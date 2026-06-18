/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer6Component : GComponent
    {
        public const string URL = "ui://z89tvj9kn0mya";
        public const string UIResName = "Layer6Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer6Component CreateInstance()
        {
            return (Layer6Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer6Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer6Component> tcs = new TaskCompletionSource<Layer6Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer6Component));
            return tcs.Task;
        }

        public static Layer6Component GetFromPool(GObject gObject)
        {
            return (Layer6Component)gObject;
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