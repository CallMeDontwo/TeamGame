/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_LittleMaryItems : GComponent
    {
        public const string URL = "ui://hoqdodepsdwr5f";
        public const string UIResName = "Com_LittleMaryItems";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.Label_MaryItem Label_MaryItem0 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem1 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem2 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem3 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem4 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem5 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem6 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem7 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem8 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem9 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem10 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem11 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem12 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem13 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem14 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem15 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem16 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem17 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem18 { get; private set; }
        public ChaoJiMoFang.Label_MaryItem Label_MaryItem19 { get; private set; }

        public static Com_LittleMaryItems CreateInstance()
        {
            return (Com_LittleMaryItems)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_LittleMaryItems> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_LittleMaryItems> tcs = new TaskCompletionSource<Com_LittleMaryItems>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_LittleMaryItems));
            return tcs.Task;
        }

        public static Com_LittleMaryItems GetFromPool(GObject gObject)
        {
            return (Com_LittleMaryItems)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Label_MaryItem0 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(0);
            Label_MaryItem1 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(1);
            Label_MaryItem2 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(2);
            Label_MaryItem3 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(3);
            Label_MaryItem4 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(4);
            Label_MaryItem5 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(5);
            Label_MaryItem6 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(6);
            Label_MaryItem7 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(7);
            Label_MaryItem8 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(8);
            Label_MaryItem9 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(9);
            Label_MaryItem10 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(10);
            Label_MaryItem11 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(11);
            Label_MaryItem12 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(12);
            Label_MaryItem13 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(13);
            Label_MaryItem14 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(14);
            Label_MaryItem15 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(15);
            Label_MaryItem16 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(16);
            Label_MaryItem17 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(17);
            Label_MaryItem18 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(18);
            Label_MaryItem19 = (ChaoJiMoFang.Label_MaryItem)GetChildAt(19);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}