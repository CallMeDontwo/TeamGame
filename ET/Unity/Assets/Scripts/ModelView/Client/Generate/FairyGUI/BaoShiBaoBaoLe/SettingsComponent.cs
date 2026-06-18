/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingsComponent : GComponent
    {
        public const string URL = "ui://oy07mpfwx7764t";
        public const string UIResName = "SettingsComponent";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Page { get; private set; }
        public BaoShiBaoBaoLe.SettingComponent1 Com_Page0 { get; private set; }
        public BaoShiBaoBaoLe.SettingComponent2 Com_Page1 { get; private set; }
        public BaoShiBaoBaoLe.SettingComponent3 Com_Page2 { get; private set; }
        public BaoShiBaoBaoLe.SettingComponent4 Com_Page3 { get; private set; }
        public BaoShiBaoBaoLe.SettingComponent5 Com_Page4 { get; private set; }

        public static SettingsComponent CreateInstance()
        {
            return (SettingsComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingsComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingsComponent> tcs = new TaskCompletionSource<SettingsComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingsComponent));
            return tcs.Task;
        }

        public static SettingsComponent GetFromPool(GObject gObject)
        {
            return (SettingsComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Page = GetControllerAt(0);
            Com_Page0 = (BaoShiBaoBaoLe.SettingComponent1)GetChildAt(2);
            Com_Page1 = (BaoShiBaoBaoLe.SettingComponent2)GetChildAt(3);
            Com_Page2 = (BaoShiBaoBaoLe.SettingComponent3)GetChildAt(4);
            Com_Page3 = (BaoShiBaoBaoLe.SettingComponent4)GetChildAt(5);
            Com_Page4 = (BaoShiBaoBaoLe.SettingComponent5)GetChildAt(6);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}