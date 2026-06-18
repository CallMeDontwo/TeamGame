/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemFlyoutGroup : GComponent
    {
        public const string URL = "ui://oy07mpfwgf9z88";
        public const string UIResName = "GemFlyoutGroup";
        public const string UIPackageName = "宝石爆爆乐";

        public BaoShiBaoBaoLe.GemFlyout Loader_Gem0 { get; private set; }
        public BaoShiBaoBaoLe.GemFlyout Loader_Gem1 { get; private set; }
        public BaoShiBaoBaoLe.GemFlyout Loader_Gem2 { get; private set; }
        public BaoShiBaoBaoLe.GemFlyout Loader_Gem3 { get; private set; }
        public BaoShiBaoBaoLe.GemFlyout Loader_Gem4 { get; private set; }
        public BaoShiBaoBaoLe.GemFlyout Loader_Gem5 { get; private set; }

        public static GemFlyoutGroup CreateInstance()
        {
            return (GemFlyoutGroup)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemFlyoutGroup> CreateInstanceAsync()
        {
            TaskCompletionSource<GemFlyoutGroup> tcs = new TaskCompletionSource<GemFlyoutGroup>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemFlyoutGroup));
            return tcs.Task;
        }

        public static GemFlyoutGroup GetFromPool(GObject gObject)
        {
            return (GemFlyoutGroup)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Loader_Gem0 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(0);
            Loader_Gem1 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(1);
            Loader_Gem2 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(2);
            Loader_Gem3 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(3);
            Loader_Gem4 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(4);
            Loader_Gem5 = (BaoShiBaoBaoLe.GemFlyout)GetChildAt(5);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}