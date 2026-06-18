/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class RecordItem1 : GComponent
    {
        public const string URL = "ui://oy07mpfwx77654";
        public const string UIResName = "RecordItem1";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Type { get; private set; }
        public GTextField Text_Name { get; private set; }
        public GTextField Text_Count { get; private set; }
        public GTextField Text_Socre { get; private set; }

        public static RecordItem1 CreateInstance()
        {
            return (RecordItem1)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<RecordItem1> CreateInstanceAsync()
        {
            TaskCompletionSource<RecordItem1> tcs = new TaskCompletionSource<RecordItem1>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as RecordItem1));
            return tcs.Task;
        }

        public static RecordItem1 GetFromPool(GObject gObject)
        {
            return (RecordItem1)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Text_Name = (GTextField)GetChildAt(1);
            Text_Count = (GTextField)GetChildAt(2);
            Text_Socre = (GTextField)GetChildAt(3);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}