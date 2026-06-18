/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemFlyout : GComponent
    {
        public const string URL = "ui://oy07mpfwgf9z89";
        public const string UIResName = "GemFlyout";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Gem { get; private set; }
        public GLoader3D Loader_Gem { get; private set; }
        public GLoader3D Loader_Boom { get; private set; }

        public static GemFlyout CreateInstance()
        {
            return (GemFlyout)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemFlyout> CreateInstanceAsync()
        {
            TaskCompletionSource<GemFlyout> tcs = new TaskCompletionSource<GemFlyout>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemFlyout));
            return tcs.Task;
        }

        public static GemFlyout GetFromPool(GObject gObject)
        {
            return (GemFlyout)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Gem = GetControllerAt(0);
            Loader_Gem = (GLoader3D)GetChildAt(0);
            Loader_Boom = (GLoader3D)GetChildAt(1);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}