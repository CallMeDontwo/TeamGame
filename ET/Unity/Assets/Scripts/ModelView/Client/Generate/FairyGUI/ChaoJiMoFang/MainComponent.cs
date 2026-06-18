/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class MainComponent : GComponent
    {
        public const string URL = "ui://hoqdodep995r4r";
        public const string UIResName = "MainComponent";
        public const string UIPackageName = "超级魔方";

        public Controller C_Type { get; private set; }
        public Controller C_JP { get; private set; }
        public ChaoJiMoFang.Com_JP Com_JP1 { get; private set; }
        public ChaoJiMoFang.Com_JP Com_JP2 { get; private set; }
        public ChaoJiMoFang.Com_JP Com_JP3 { get; private set; }
        public ChaoJiMoFang.Com_JP Com_All { get; private set; }
        public ChaoJiMoFang.SubComponent1 Com_Sub1 { get; private set; }
        public ChaoJiMoFang.SubComponent2 Com_Sub2 { get; private set; }
        public ChaoJiMoFang.SubComponent3 Com_Sub3 { get; private set; }
        public ChaoJiMoFang.Com_Credit Com_Credit { get; private set; }
        public ChaoJiMoFang.Com_Paid Com_Paid { get; private set; }
        public ChaoJiMoFang.Com_Win Com_Win { get; private set; }
        public ChaoJiMoFang.Com_OUT Com_OUT { get; private set; }
        public ChaoJiMoFang.Com_SPIN Com_SPIN { get; private set; }
        public ChaoJiMoFang.Label_DieDieLeTime Lable_DieDieLeTime { get; private set; }
        public ChaoJiMoFang.Label_DieDieLeCoins Lable_DieDieLeCoins { get; private set; }
        public GList List_Icon { get; private set; }
        public ChaoJiMoFang.Com_AwardShow Com_Settle { get; private set; }
        public ChaoJiMoFang.Com_JPAward Com_JPAward { get; private set; }
        public GLoader3D Loader_JP { get; private set; }

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
            C_Type = GetControllerAt(0);
            C_JP = GetControllerAt(1);
            Com_JP1 = (ChaoJiMoFang.Com_JP)GetChildAt(1);
            Com_JP2 = (ChaoJiMoFang.Com_JP)GetChildAt(2);
            Com_JP3 = (ChaoJiMoFang.Com_JP)GetChildAt(3);
            Com_All = (ChaoJiMoFang.Com_JP)GetChildAt(4);
            Com_Sub1 = (ChaoJiMoFang.SubComponent1)GetChildAt(5);
            Com_Sub2 = (ChaoJiMoFang.SubComponent2)GetChildAt(6);
            Com_Sub3 = (ChaoJiMoFang.SubComponent3)GetChildAt(7);
            Com_Credit = (ChaoJiMoFang.Com_Credit)GetChildAt(8);
            Com_Paid = (ChaoJiMoFang.Com_Paid)GetChildAt(9);
            Com_Win = (ChaoJiMoFang.Com_Win)GetChildAt(10);
            Com_OUT = (ChaoJiMoFang.Com_OUT)GetChildAt(11);
            Com_SPIN = (ChaoJiMoFang.Com_SPIN)GetChildAt(12);
            Lable_DieDieLeTime = (ChaoJiMoFang.Label_DieDieLeTime)GetChildAt(14);
            Lable_DieDieLeCoins = (ChaoJiMoFang.Label_DieDieLeCoins)GetChildAt(15);
            List_Icon = (GList)GetChildAt(17);
            Com_Settle = (ChaoJiMoFang.Com_AwardShow)GetChildAt(18);
            Com_JPAward = (ChaoJiMoFang.Com_JPAward)GetChildAt(19);
            Loader_JP = (GLoader3D)GetChildAt(20);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}