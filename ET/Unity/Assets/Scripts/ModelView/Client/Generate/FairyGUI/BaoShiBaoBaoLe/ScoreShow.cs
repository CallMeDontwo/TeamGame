/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class ScoreShow : GComponent
    {
        public const string URL = "ui://oy07mpfwlzui2h";
        public const string UIResName = "ScoreShow";
        public const string UIPackageName = "宝石爆爆乐";

        public GTextField Text_Socre { get; private set; }

        public static ScoreShow CreateInstance()
        {
            return (ScoreShow)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<ScoreShow> CreateInstanceAsync()
        {
            TaskCompletionSource<ScoreShow> tcs = new TaskCompletionSource<ScoreShow>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as ScoreShow));
            return tcs.Task;
        }

        public static ScoreShow GetFromPool(GObject gObject)
        {
            return (ScoreShow)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_Socre = (GTextField)GetChildAt(1);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}