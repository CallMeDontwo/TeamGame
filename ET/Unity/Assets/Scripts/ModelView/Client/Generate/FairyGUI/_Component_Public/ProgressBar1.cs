/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET._Component_Public
{
    [EnableClass]
    public partial class ProgressBar1 : GProgressBar
    {
        public const string URL = "ui://7am1s0tjppw61";
        public const string UIResName = "ProgressBar1";
        public const string UIPackageName = "_Component_Public";


        public static ProgressBar1 CreateInstance()
        {
            return (ProgressBar1)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<ProgressBar1> CreateInstanceAsync()
        {
            TaskCompletionSource<ProgressBar1> tcs = new TaskCompletionSource<ProgressBar1>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as ProgressBar1));
            return tcs.Task;
        }

        public static ProgressBar1 GetFromPool(GObject gObject)
        {
            return (ProgressBar1)gObject;
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