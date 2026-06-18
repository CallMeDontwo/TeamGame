/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class RecordItem2 : GLabel
    {
        public const string URL = "ui://oy07mpfwx77656";
        public const string UIResName = "RecordItem2";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Type { get; private set; }
        public GTextField Text_Data { get; private set; }

        public static RecordItem2 CreateInstance()
        {
            return (RecordItem2)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<RecordItem2> CreateInstanceAsync()
        {
            TaskCompletionSource<RecordItem2> tcs = new TaskCompletionSource<RecordItem2>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as RecordItem2));
            return tcs.Task;
        }

        public static RecordItem2 GetFromPool(GObject gObject)
        {
            return (RecordItem2)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Text_Data = (GTextField)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}