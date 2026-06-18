/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SettingComponent1 : GComponent
    {
        public const string URL = "ui://hoqdodepjxfj6c";
        public const string UIResName = "SettingComponent1";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.Button1 Btn_Record { get; private set; }
        public ChaoJiMoFang.Button1 Btn_Settings { get; private set; }
        public ChaoJiMoFang.Button1 Btn_Exit { get; private set; }

        public static SettingComponent1 CreateInstance()
        {
            return (SettingComponent1)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingComponent1> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingComponent1> tcs = new TaskCompletionSource<SettingComponent1>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingComponent1));
            return tcs.Task;
        }

        public static SettingComponent1 GetFromPool(GObject gObject)
        {
            return (SettingComponent1)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Btn_Record = (ChaoJiMoFang.Button1)GetChildAt(0);
            Btn_Settings = (ChaoJiMoFang.Button1)GetChildAt(1);
            Btn_Exit = (ChaoJiMoFang.Button1)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}