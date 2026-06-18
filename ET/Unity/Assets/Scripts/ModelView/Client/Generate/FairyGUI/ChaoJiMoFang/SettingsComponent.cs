/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SettingsComponent : GComponent
    {
        public const string URL = "ui://hoqdodepjxfj6a";
        public const string UIResName = "SettingsComponent";
        public const string UIPackageName = "超级魔方";

        public Controller C_Page { get; private set; }
        public ChaoJiMoFang.SettingComponent1 Com_Page0 { get; private set; }
        public ChaoJiMoFang.SettingComponent2 Com_Page1 { get; private set; }
        public ChaoJiMoFang.SettingComponent3 Com_Page2 { get; private set; }
        public ChaoJiMoFang.SettingComponent4 Com_Page3 { get; private set; }
        public ChaoJiMoFang.SettingComponent5 Com_Page4 { get; private set; }

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
            Com_Page0 = (ChaoJiMoFang.SettingComponent1)GetChildAt(2);
            Com_Page1 = (ChaoJiMoFang.SettingComponent2)GetChildAt(3);
            Com_Page2 = (ChaoJiMoFang.SettingComponent3)GetChildAt(4);
            Com_Page3 = (ChaoJiMoFang.SettingComponent4)GetChildAt(5);
            Com_Page4 = (ChaoJiMoFang.SettingComponent5)GetChildAt(6);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}