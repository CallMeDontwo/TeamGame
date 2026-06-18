/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.TeamGame
{
    [EnableClass]
    public partial class Pro_hp : GProgressBar
    {
        public const string URL = "ui://hyf249vwc7oi0";
        public const string UIResName = "Pro_hp";
        public const string UIPackageName = "TeamGame";


        public static Pro_hp CreateInstance()
        {
            return (Pro_hp)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Pro_hp> CreateInstanceAsync()
        {
            TaskCompletionSource<Pro_hp> tcs = new TaskCompletionSource<Pro_hp>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Pro_hp));
            return tcs.Task;
        }

        public static Pro_hp GetFromPool(GObject gObject)
        {
            return (Pro_hp)gObject;
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