/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ZiYuanGengXin
{
    public partial class DownloadTip : GComponent
    {
        public GGraph n4;
        public GTextField Text_Tip;
        public ZiYuanGengXin.Button Btn_Cancel;
        public ZiYuanGengXin.Button Btn_OK;
        public const string URL = "ui://k8htpmtjv8692";

        public static DownloadTip CreateInstance()
        {
            return (DownloadTip)UIPackage.CreateObject("资源更新", "DownloadTip");
        }

        public static Task<DownloadTip> CreateInstanceAsync()
        {
            TaskCompletionSource<DownloadTip> tcs = new TaskCompletionSource<DownloadTip>();
            UIPackage.CreateObjectAsync("资源更新", "DownloadTip", (go) => tcs.SetResult(go as DownloadTip));
            return tcs.Task;
        }

        public static DownloadTip GetFromPool(GObject gObject)
        {
            return (DownloadTip)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n4 = (GGraph)GetChildAt(0);
            Text_Tip = (GTextField)GetChildAt(1);
            Btn_Cancel = (ZiYuanGengXin.Button)GetChildAt(2);
            Btn_OK = (ZiYuanGengXin.Button)GetChildAt(3);
        }
    }
}