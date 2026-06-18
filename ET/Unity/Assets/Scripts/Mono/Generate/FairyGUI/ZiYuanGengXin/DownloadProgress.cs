/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ZiYuanGengXin
{
    public partial class DownloadProgress : GProgressBar
    {
        public GImage n0;
        public GImage bar;
        public GTextField title;
        public const string URL = "ui://k8htpmtjv8691";

        public static DownloadProgress CreateInstance()
        {
            return (DownloadProgress)UIPackage.CreateObject("资源更新", "DownloadProgress");
        }

        public static Task<DownloadProgress> CreateInstanceAsync()
        {
            TaskCompletionSource<DownloadProgress> tcs = new TaskCompletionSource<DownloadProgress>();
            UIPackage.CreateObjectAsync("资源更新", "DownloadProgress", (go) => tcs.SetResult(go as DownloadProgress));
            return tcs.Task;
        }

        public static DownloadProgress GetFromPool(GObject gObject)
        {
            return (DownloadProgress)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            n0 = (GImage)GetChildAt(0);
            bar = (GImage)GetChildAt(1);
            title = (GTextField)GetChildAt(2);
        }
    }
}