/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ZiYuanGengXin
{
    public partial class HotfixComponent : GComponent
    {
        public Controller C_Status;
        public Controller C_Tip;
        public GGraph n15;
        public GTextField n3;
        public GTextField n4;
        public GTextField Text_DownloadSize;
        public GTextField Text_DownloadSpeed;
        public ZiYuanGengXin.DownloadProgress Bar_Download;
        public GGroup n2;
        public GTextField n10;
        public GGraph n12;
        public ZiYuanGengXin.DownloadTip Com_DownloadTip;
        public const string URL = "ui://k8htpmtjv8690";

        public static HotfixComponent CreateInstance()
        {
            return (HotfixComponent)UIPackage.CreateObject("资源更新", "HotfixComponent");
        }

        public static Task<HotfixComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<HotfixComponent> tcs = new TaskCompletionSource<HotfixComponent>();
            UIPackage.CreateObjectAsync("资源更新", "HotfixComponent", (go) => tcs.SetResult(go as HotfixComponent));
            return tcs.Task;
        }

        public static HotfixComponent GetFromPool(GObject gObject)
        {
            return (HotfixComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            C_Status = GetControllerAt(0);
            C_Tip = GetControllerAt(1);
            n15 = (GGraph)GetChildAt(0);
            n3 = (GTextField)GetChildAt(1);
            n4 = (GTextField)GetChildAt(2);
            Text_DownloadSize = (GTextField)GetChildAt(3);
            Text_DownloadSpeed = (GTextField)GetChildAt(4);
            Bar_Download = (ZiYuanGengXin.DownloadProgress)GetChildAt(5);
            n2 = (GGroup)GetChildAt(6);
            n10 = (GTextField)GetChildAt(7);
            n12 = (GGraph)GetChildAt(8);
            Com_DownloadTip = (ZiYuanGengXin.DownloadTip)GetChildAt(9);
        }
    }
}