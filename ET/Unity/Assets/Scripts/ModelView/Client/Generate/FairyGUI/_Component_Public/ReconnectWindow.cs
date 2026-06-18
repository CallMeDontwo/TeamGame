/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class ReconnectWindow : GComponent
    {
        public const string URL = "ui://7am1s0tjvojlq9r";
        public const string UIResName = "ReconnectWindow";
        public const string UIPackageName = "_Component_Public";

        public Transition T_Play { get; private set; }

        public static ReconnectWindow CreateInstance()
        {
            return (ReconnectWindow)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<ReconnectWindow> CreateInstanceAsync()
        {
            TaskCompletionSource<ReconnectWindow> tcs = new TaskCompletionSource<ReconnectWindow>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as ReconnectWindow));
            return tcs.Task;
        }

        public static ReconnectWindow GetFromPool(GObject gObject)
        {
            return (ReconnectWindow)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            T_Play = GetTransitionAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}