/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class WinAwardPool : GComponent
    {
        public const string URL = "ui://oy07mpfwka85b6";
        public const string UIResName = "WinAwardPool";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_ShowText { get; private set; }
        public GLoader3D Loader_Spine { get; private set; }
        public GTextField Text_SelfAward { get; private set; }
        public GTextField Text_AwardPool { get; private set; }
        public GTextField Text_Score { get; private set; }

        public static WinAwardPool CreateInstance()
        {
            return (WinAwardPool)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<WinAwardPool> CreateInstanceAsync()
        {
            TaskCompletionSource<WinAwardPool> tcs = new TaskCompletionSource<WinAwardPool>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as WinAwardPool));
            return tcs.Task;
        }

        public static WinAwardPool GetFromPool(GObject gObject)
        {
            return (WinAwardPool)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_ShowText = GetControllerAt(0);
            Loader_Spine = (GLoader3D)GetChildAt(1);
            Text_SelfAward = (GTextField)GetChildAt(4);
            Text_AwardPool = (GTextField)GetChildAt(6);
            Text_Score = (GTextField)GetChildAt(8);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}