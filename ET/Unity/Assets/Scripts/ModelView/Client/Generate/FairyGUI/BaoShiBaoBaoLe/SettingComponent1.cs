/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingComponent1 : GComponent
    {
        public const string URL = "ui://oy07mpfwx7764v";
        public const string UIResName = "SettingComponent1";
        public const string UIPackageName = "宝石爆爆乐";

        public BaoShiBaoBaoLe.Button1 Btn_Record { get; private set; }
        public BaoShiBaoBaoLe.Button1 Btn_Settings { get; private set; }
        public BaoShiBaoBaoLe.Button1 Btn_Exit { get; private set; }

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
            Btn_Record = (BaoShiBaoBaoLe.Button1)GetChildAt(0);
            Btn_Settings = (BaoShiBaoBaoLe.Button1)GetChildAt(1);
            Btn_Exit = (BaoShiBaoBaoLe.Button1)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}