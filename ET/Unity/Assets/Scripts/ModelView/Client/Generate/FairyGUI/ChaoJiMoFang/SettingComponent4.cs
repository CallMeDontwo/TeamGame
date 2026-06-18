/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SettingComponent4 : GComponent
    {
        public const string URL = "ui://hoqdodepee5c6h";
        public const string UIResName = "SettingComponent4";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.RecordItem2 Com_Record0 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record1 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record2 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record3 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record4 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record5 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record6 { get; private set; }
        public ChaoJiMoFang.RecordItem2 Com_Record7 { get; private set; }
        public ChaoJiMoFang.Button2 Btn_Back { get; private set; }
        public ChaoJiMoFang.Button5 Btn_Resotre { get; private set; }

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
            Com_Record0 = (ChaoJiMoFang.RecordItem2)GetChildAt(4);
            Com_Record1 = (ChaoJiMoFang.RecordItem2)GetChildAt(5);
            Com_Record2 = (ChaoJiMoFang.RecordItem2)GetChildAt(6);
            Com_Record3 = (ChaoJiMoFang.RecordItem2)GetChildAt(7);
            Com_Record4 = (ChaoJiMoFang.RecordItem2)GetChildAt(8);
            Com_Record5 = (ChaoJiMoFang.RecordItem2)GetChildAt(9);
            Com_Record6 = (ChaoJiMoFang.RecordItem2)GetChildAt(10);
            Com_Record7 = (ChaoJiMoFang.RecordItem2)GetChildAt(11);
            Btn_Back = (ChaoJiMoFang.Button2)GetChildAt(13);
            Btn_Resotre = (ChaoJiMoFang.Button5)GetChildAt(14);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}