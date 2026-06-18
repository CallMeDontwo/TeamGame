/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.TeamGame
{
    [EnableClass]
    public partial class TeamMain : GComponent
    {
        public const string URL = "ui://hyf249vwc7oi3";
        public const string UIResName = "TeamMain";
        public const string UIPackageName = "TeamGame";


        public static TeamMain CreateInstance()
        {
            return (TeamMain)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<TeamMain> CreateInstanceAsync()
        {
            TaskCompletionSource<TeamMain> tcs = new TaskCompletionSource<TeamMain>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as TeamMain));
            return tcs.Task;
        }

        public static TeamMain GetFromPool(GObject gObject)
        {
            return (TeamMain)gObject;
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