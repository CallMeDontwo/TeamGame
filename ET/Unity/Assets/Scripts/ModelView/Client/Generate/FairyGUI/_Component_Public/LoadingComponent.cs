/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class LoadingComponent : GComponent
    {
        public const string URL = "ui://7am1s0tjppw60";
        public const string UIResName = "LoadingComponent";
        public const string UIPackageName = "_Component_Public";

        public _Component_Public.ProgressBar1 Bar_Loading { get; private set; }

        public static LoadingComponent CreateInstance()
        {
            return (LoadingComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<LoadingComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<LoadingComponent> tcs = new TaskCompletionSource<LoadingComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as LoadingComponent));
            return tcs.Task;
        }

        public static LoadingComponent GetFromPool(GObject gObject)
        {
            return (LoadingComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Bar_Loading = (_Component_Public.ProgressBar1)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}