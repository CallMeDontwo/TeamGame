/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer9Component : GComponent
    {
        public const string URL = "ui://z89tvj9kn0myd";
        public const string UIResName = "Layer9Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer9Component CreateInstance()
        {
            return (Layer9Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer9Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer9Component> tcs = new TaskCompletionSource<Layer9Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer9Component));
            return tcs.Task;
        }

        public static Layer9Component GetFromPool(GObject gObject)
        {
            return (Layer9Component)gObject;
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