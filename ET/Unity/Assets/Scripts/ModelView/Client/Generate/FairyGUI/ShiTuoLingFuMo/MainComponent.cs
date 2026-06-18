/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ShiTuoLingFuMo
{
    [EnableClass]
    public partial class MainComponent : GComponent
    {
        public const string URL = "ui://rjjbyphxhbgv0";
        public const string UIResName = "MainComponent";
        public const string UIPackageName = "狮驼岭伏魔";

        public ShiTuoLingFuMo.ProgressBar1 Progress_Huihe { get; private set; }
        public GList List_GemList { get; private set; }
        public GTextField Text_SuperBigAward { get; private set; }
        public GTextField Text_BigAward { get; private set; }
        public GTextField Text_LittleAward { get; private set; }
        public GTextField Text_Credit { get; private set; }
        public GTextField Text_Bet { get; private set; }
        public GTextField Text_AllScore { get; private set; }
        public ShiTuoLingFuMo.Button1 Btn_Auto { get; private set; }

        public static MainComponent CreateInstance()
        {
            return (MainComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<MainComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<MainComponent> tcs = new TaskCompletionSource<MainComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as MainComponent));
            return tcs.Task;
        }

        public static MainComponent GetFromPool(GObject gObject)
        {
            return (MainComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Progress_Huihe = (ShiTuoLingFuMo.ProgressBar1)GetChildAt(1);
            List_GemList = (GList)GetChildAt(4);
            Text_SuperBigAward = (GTextField)GetChildAt(7);
            Text_BigAward = (GTextField)GetChildAt(10);
            Text_LittleAward = (GTextField)GetChildAt(13);
            Text_Credit = (GTextField)GetChildAt(17);
            Text_Bet = (GTextField)GetChildAt(20);
            Text_AllScore = (GTextField)GetChildAt(23);
            Btn_Auto = (ShiTuoLingFuMo.Button1)GetChildAt(26);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}