/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Main
{
    [EnableClass]
    public partial class Layer0Component : GComponent
    {
        public const string URL = "ui://z89tvj9ksnyy9";
        public const string UIResName = "Layer0Component";
        public const string UIPackageName = "_Component_Main";


        public static Layer0Component CreateInstance()
        {
            return (Layer0Component)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Layer0Component> CreateInstanceAsync()
        {
            TaskCompletionSource<Layer0Component> tcs = new TaskCompletionSource<Layer0Component>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Layer0Component));
            return tcs.Task;
        }

        public static Layer0Component GetFromPool(GObject gObject)
        {
            return (Layer0Component)gObject;
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