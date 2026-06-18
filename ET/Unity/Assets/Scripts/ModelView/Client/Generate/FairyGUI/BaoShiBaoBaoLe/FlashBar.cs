/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class FlashBar : GComponent
    {
        public const string URL = "ui://oy07mpfwnf6a87";
        public const string UIResName = "FlashBar";
        public const string UIPackageName = "宝石爆爆乐";

        public GLoader3D Loader_Spine { get; private set; }

        public static FlashBar CreateInstance()
        {
            return (FlashBar)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<FlashBar> CreateInstanceAsync()
        {
            TaskCompletionSource<FlashBar> tcs = new TaskCompletionSource<FlashBar>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as FlashBar));
            return tcs.Task;
        }

        public static FlashBar GetFromPool(GObject gObject)
        {
            return (FlashBar)gObject;
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