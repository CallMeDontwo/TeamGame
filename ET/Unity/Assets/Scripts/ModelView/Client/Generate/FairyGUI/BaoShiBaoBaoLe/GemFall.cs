/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemFall : GComponent
    {
        public const string URL = "ui://oy07mpfwgxs78a";
        public const string UIResName = "GemFall";
        public const string UIPackageName = "宝石爆爆乐";

        public GLoader3D Loader_Spine { get; private set; }

        public static GemFall CreateInstance()
        {
            return (GemFall)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemFall> CreateInstanceAsync()
        {
            TaskCompletionSource<GemFall> tcs = new TaskCompletionSource<GemFall>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemFall));
            return tcs.Task;
        }

        public static GemFall GetFromPool(GObject gObject)
        {
            return (GemFall)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Loader_Spine = (GLoader3D)GetChildAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}