/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class SettingComponent3 : GComponent
    {
        public const string URL = "ui://oy07mpfwx77652";
        public const string UIResName = "SettingComponent3";
        public const string UIPackageName = "宝石爆爆乐";

        public GList List_Records { get; private set; }
        public BaoShiBaoBaoLe.Button2 Btn_Back { get; private set; }

        public static SettingComponent3 CreateInstance()
        {
            return (SettingComponent3)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SettingComponent3> CreateInstanceAsync()
        {
            TaskCompletionSource<SettingComponent3> tcs = new TaskCompletionSource<SettingComponent3>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SettingComponent3));
            return tcs.Task;
        }

        public static SettingComponent3 GetFromPool(GObject gObject)
        {
            return (SettingComponent3)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            List_Records = (GList)GetChildAt(5);
            Btn_Back = (BaoShiBaoBaoLe.Button2)GetChildAt(6);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}