/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SettingComponent5 : GComponent
    {
        public const string URL = "ui://hoqdodepee5c6j";
        public const string UIResName = "SettingComponent5";
        public const string UIPackageName = "超级魔方";

        public Controller C_CaiPiaoModule { get; private set; }
        public Controller C_CoinNum { get; private set; }
        public Controller C_BGM { get; private set; }
        public Controller C_CaiJingMusic { get; private set; }
        public Controller C_CaiPiaoRate { get; private set; }
        public Controller C_RTP { get; private set; }
        public ChaoJiMoFang.Button2 Btn_Back { get; private set; }
        public ChaoJiMoFang.Button5 Btn_Resotre { get; private set; }

        public static SettingComponent5 CreateInstance()
        {
            return (SettingComponent5)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingComponent5> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingComponent5> tcs = new TaskCompletionSource<SettingComponent5>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingComponent5));
            return tcs.Task;
        }

        public static SettingComponent5 GetFromPool(GObject gObject)
        {
            return (SettingComponent5)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_CaiPiaoModule = GetControllerAt(0);
            C_CoinNum = GetControllerAt(1);
            C_BGM = GetControllerAt(2);
            C_CaiJingMusic = GetControllerAt(3);
            C_CaiPiaoRate = GetControllerAt(4);
            C_RTP = GetControllerAt(5);
            Btn_Back = (ChaoJiMoFang.Button2)GetChildAt(16);
            Btn_Resotre = (ChaoJiMoFang.Button5)GetChildAt(17);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}