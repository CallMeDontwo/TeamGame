/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingComponent2 : GComponent
    {
        public const string URL = "ui://oy07mpfwx7764z";
        public const string UIResName = "SettingComponent2";
        public const string UIPackageName = "宝石爆爆乐";

        public BaoShiBaoBaoLe.Button1 Btn_Record1 { get; private set; }
        public BaoShiBaoBaoLe.Button1 Btn_Record2 { get; private set; }
        public BaoShiBaoBaoLe.Button2 Btn_Back { get; private set; }

        public static SettingComponent2 CreateInstance()
        {
            return (SettingComponent2)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingComponent2> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingComponent2> tcs = new TaskCompletionSource<SettingComponent2>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingComponent2));
            return tcs.Task;
        }

        public static SettingComponent2 GetFromPool(GObject gObject)
        {
            return (SettingComponent2)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Btn_Record1 = (BaoShiBaoBaoLe.Button1)GetChildAt(1);
            Btn_Record2 = (BaoShiBaoBaoLe.Button1)GetChildAt(2);
            Btn_Back = (BaoShiBaoBaoLe.Button2)GetChildAt(3);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}