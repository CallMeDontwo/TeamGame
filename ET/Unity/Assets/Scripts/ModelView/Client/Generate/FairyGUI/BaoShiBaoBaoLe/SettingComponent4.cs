/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingComponent4 : GComponent
    {
        public const string URL = "ui://oy07mpfwx77655";
        public const string UIResName = "SettingComponent4";
        public const string UIPackageName = "宝石爆爆乐";

        public BaoShiBaoBaoLe.RecordItem2 Com_Record0 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record1 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record2 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record3 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record4 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record5 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record6 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record7 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record8 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record9 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record10 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record11 { get; private set; }
        public BaoShiBaoBaoLe.RecordItem2 Com_Record12 { get; private set; }
        public BaoShiBaoBaoLe.Button2 Btn_Back { get; private set; }
        public BaoShiBaoBaoLe.Button5 Btn_Resotre { get; private set; }

        public static SettingComponent4 CreateInstance()
        {
            return (SettingComponent4)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingComponent4> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingComponent4> tcs = new TaskCompletionSource<SettingComponent4>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingComponent4));
            return tcs.Task;
        }

        public static SettingComponent4 GetFromPool(GObject gObject)
        {
            return (SettingComponent4)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Com_Record0 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(4);
            Com_Record1 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(5);
            Com_Record2 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(6);
            Com_Record3 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(7);
            Com_Record4 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(8);
            Com_Record5 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(9);
            Com_Record6 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(10);
            Com_Record7 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(11);
            Com_Record8 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(12);
            Com_Record9 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(13);
            Com_Record10 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(14);
            Com_Record11 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(15);
            Com_Record12 = (BaoShiBaoBaoLe.RecordItem2)GetChildAt(16);
            Btn_Back = (BaoShiBaoBaoLe.Button2)GetChildAt(18);
            Btn_Resotre = (BaoShiBaoBaoLe.Button5)GetChildAt(19);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}