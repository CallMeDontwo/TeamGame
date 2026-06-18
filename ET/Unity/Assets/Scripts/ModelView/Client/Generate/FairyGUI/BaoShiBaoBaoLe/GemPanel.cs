/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemPanel : GComponent
    {
        public const string URL = "ui://oy07mpfwfwdoh";
        public const string UIResName = "GemPanel";
        public const string UIPackageName = "宝石爆爆乐";

        public BaoShiBaoBaoLe.ScoreShow ScoreShow { get; private set; }
        public BaoShiBaoBaoLe.ComboShow Com_Combo { get; private set; }
        public Transition T_Combo { get; private set; }
        public Transition T_PlayBoom { get; private set; }
        public Transition T_GemFall { get; private set; }

        public static GemPanel CreateInstance()
        {
            return (GemPanel)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemPanel> CreateInstanceAsync()
        {
            TaskCompletionSource<GemPanel> tcs = new TaskCompletionSource<GemPanel>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemPanel));
            return tcs.Task;
        }

        public static GemPanel GetFromPool(GObject gObject)
        {
            return (GemPanel)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            ScoreShow = (BaoShiBaoBaoLe.ScoreShow)GetChildAt(0);
            Com_Combo = (BaoShiBaoBaoLe.ComboShow)GetChildAt(1);
            T_Combo = GetTransitionAt(0);
            T_PlayBoom = GetTransitionAt(1);
            T_GemFall = GetTransitionAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}