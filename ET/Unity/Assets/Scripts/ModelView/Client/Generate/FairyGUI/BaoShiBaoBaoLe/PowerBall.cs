/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class PowerBall : GComponent
    {
        public const string URL = "ui://oy07mpfwrdxpb5";
        public const string UIResName = "PowerBall";
        public const string UIPackageName = "宝石爆爆乐";

        public GLoader3D Loader_Back { get; private set; }
        public GLoader3D Loader_Front { get; private set; }

        public static PowerBall CreateInstance()
        {
            return (PowerBall)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<PowerBall> CreateInstanceAsync()
        {
            TaskCompletionSource<PowerBall> tcs = new TaskCompletionSource<PowerBall>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as PowerBall));
            return tcs.Task;
        }

        public static PowerBall GetFromPool(GObject gObject)
        {
            return (PowerBall)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Loader_Back = (GLoader3D)GetChildAt(0);
            Loader_Front = (GLoader3D)GetChildAt(1);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}