/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingComponent5 : GComponent
    {
        public const string URL = "ui://oy07mpfwx77658";
        public const string UIResName = "SettingComponent5";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_TicketMode { get; private set; }
        public Controller C_CreditRate { get; private set; }
        public Controller C_TicketRate { get; private set; }
        public Controller C_RTP { get; private set; }
        public Controller C_BGM { get; private set; }
        public Controller C_CaiJingMusic { get; private set; }
        public GList List_CreditRate { get; private set; }
        public BaoShiBaoBaoLe.Button2 Btn_Back { get; private set; }
        public BaoShiBaoBaoLe.Button5 Btn_Resotre { get; private set; }

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
            C_TicketMode = GetControllerAt(0);
            C_CreditRate = GetControllerAt(1);
            C_TicketRate = GetControllerAt(2);
            C_RTP = GetControllerAt(3);
            C_BGM = GetControllerAt(4);
            C_CaiJingMusic = GetControllerAt(5);
            List_CreditRate = (GList)GetChildAt(7);
            Btn_Back = (BaoShiBaoBaoLe.Button2)GetChildAt(16);
            Btn_Resotre = (BaoShiBaoBaoLe.Button5)GetChildAt(17);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}