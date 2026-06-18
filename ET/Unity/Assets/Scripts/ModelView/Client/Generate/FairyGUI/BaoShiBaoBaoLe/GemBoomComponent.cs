/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemBoomComponent : GComponent
    {
        public const string URL = "ui://oy07mpfwlysj0";
        public const string UIResName = "GemBoomComponent";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_MultiplePos { get; private set; }
        public GLoader3D Loader_GameBg { get; private set; }
        public GGraph Graph_Separate0 { get; private set; }
        public GLoader3D Loader_RoleSpine { get; private set; }
        public GLoader3D Loader_GridBg { get; private set; }
        public BaoShiBaoBaoLe.GemPanel Com_GemPanel { get; private set; }
        public GLoader3D Loader_SuperSodaFg { get; private set; }
        public GGraph Graph_Separate1 { get; private set; }
        public BaoShiBaoBaoLe.ProgressBar1 Bar_Progress { get; private set; }
        public GLoader3D Loader_Multiple { get; private set; }
        public GImage Img_NextGem { get; private set; }
        public BaoShiBaoBaoLe.GemFlyoutGroup Com_FlyoutGroup { get; private set; }
        public GRichTextField Text_Credit { get; private set; }
        public GRichTextField Text_Bet { get; private set; }
        public GRichTextField Text_Totals { get; private set; }
        public GLoader3D Loader_LightBall0 { get; private set; }
        public GLoader3D Loader_LightBall1 { get; private set; }
        public GLoader3D Loader_LightBall2 { get; private set; }
        public GLoader3D Loader_Mode { get; private set; }
        public GLoader3D Loader_PlzAddCoin { get; private set; }
        public GGraph Graph_Separate2 { get; private set; }
        public BaoShiBaoBaoLe.GemLineShow Com_GemLineShow { get; private set; }
        public BaoShiBaoBaoLe.OnlyGenerateGem Com_OnlyGenerate { get; private set; }
        public GLoader3D Loader_LotusFlower { get; private set; }
        public GLoader3D Loader_Energy { get; private set; }
        public GRichTextField Text_AwardPool { get; private set; }
        public GTextField Text_SelfAward { get; private set; }
        public GLoader3D Loader_PondWater { get; private set; }
        public GTextField Text_AwardProgress { get; private set; }
        public GGroup Group_AwardPool { get; private set; }
        public BaoShiBaoBaoLe.PowerBallLayer Com_PowerBalls { get; private set; }
        public GGraph Graph_Black { get; private set; }
        public GLoader3D Loader_Win { get; private set; }
        public GGraph Graph_Separate3 { get; private set; }
        public GRichTextField Text_Score { get; private set; }
        public GImage Img_Power { get; private set; }
        public BaoShiBaoBaoLe.WinAwardPool Com_WinAwardPool { get; private set; }
        public BaoShiBaoBaoLe.AutioButton Btn_Auto { get; private set; }
        public BaoShiBaoBaoLe.SettingsComponent Com_Setting { get; private set; }
        public Transition T_ComboChallenge { get; private set; }
        public Transition T_PlayBonuns { get; private set; }
        public Transition T_PlayFillGem { get; private set; }
        public Transition T_GetCredit { get; private set; }

        public static GemBoomComponent CreateInstance()
        {
            return (GemBoomComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemBoomComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<GemBoomComponent> tcs = new TaskCompletionSource<GemBoomComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemBoomComponent));
            return tcs.Task;
        }

        public static GemBoomComponent GetFromPool(GObject gObject)
        {
            return (GemBoomComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_MultiplePos = GetControllerAt(0);
            Loader_GameBg = (GLoader3D)GetChildAt(0);
            Graph_Separate0 = (GGraph)GetChildAt(1);
            Loader_RoleSpine = (GLoader3D)GetChildAt(4);
            Loader_GridBg = (GLoader3D)GetChildAt(5);
            Com_GemPanel = (BaoShiBaoBaoLe.GemPanel)GetChildAt(6);
            Loader_SuperSodaFg = (GLoader3D)GetChildAt(7);
            Graph_Separate1 = (GGraph)GetChildAt(8);
            Bar_Progress = (BaoShiBaoBaoLe.ProgressBar1)GetChildAt(10);
            Loader_Multiple = (GLoader3D)GetChildAt(11);
            Img_NextGem = (GImage)GetChildAt(12);
            Com_FlyoutGroup = (BaoShiBaoBaoLe.GemFlyoutGroup)GetChildAt(13);
            Text_Credit = (GRichTextField)GetChildAt(18);
            Text_Bet = (GRichTextField)GetChildAt(22);
            Text_Totals = (GRichTextField)GetChildAt(26);
            Loader_LightBall0 = (GLoader3D)GetChildAt(27);
            Loader_LightBall1 = (GLoader3D)GetChildAt(28);
            Loader_LightBall2 = (GLoader3D)GetChildAt(29);
            Loader_Mode = (GLoader3D)GetChildAt(30);
            Loader_PlzAddCoin = (GLoader3D)GetChildAt(31);
            Graph_Separate2 = (GGraph)GetChildAt(32);
            Com_GemLineShow = (BaoShiBaoBaoLe.GemLineShow)GetChildAt(33);
            Com_OnlyGenerate = (BaoShiBaoBaoLe.OnlyGenerateGem)GetChildAt(34);
            Loader_LotusFlower = (GLoader3D)GetChildAt(36);
            Loader_Energy = (GLoader3D)GetChildAt(37);
            Text_AwardPool = (GRichTextField)GetChildAt(38);
            Text_SelfAward = (GTextField)GetChildAt(41);
            Loader_PondWater = (GLoader3D)GetChildAt(42);
            Text_AwardProgress = (GTextField)GetChildAt(43);
            Group_AwardPool = (GGroup)GetChildAt(44);
            Com_PowerBalls = (BaoShiBaoBaoLe.PowerBallLayer)GetChildAt(45);
            Graph_Black = (GGraph)GetChildAt(46);
            Loader_Win = (GLoader3D)GetChildAt(47);
            Graph_Separate3 = (GGraph)GetChildAt(48);
            Text_Score = (GRichTextField)GetChildAt(49);
            Img_Power = (GImage)GetChildAt(50);
            Com_WinAwardPool = (BaoShiBaoBaoLe.WinAwardPool)GetChildAt(51);
            Btn_Auto = (BaoShiBaoBaoLe.AutioButton)GetChildAt(52);
            Com_Setting = (BaoShiBaoBaoLe.SettingsComponent)GetChildAt(53);
            T_ComboChallenge = GetTransitionAt(0);
            T_PlayBonuns = GetTransitionAt(1);
            T_PlayFillGem = GetTransitionAt(2);
            T_GetCredit = GetTransitionAt(3);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}