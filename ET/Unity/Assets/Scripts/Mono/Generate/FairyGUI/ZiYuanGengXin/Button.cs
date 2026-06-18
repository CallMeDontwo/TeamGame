/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ZiYuanGengXin
{
    public partial class Button : GButton
    {
        public Controller button;
        public GGraph n0;
        public GGraph n1;
        public GGraph n2;
        public const string URL = "ui://k8htpmtjv8693";

        public static Button CreateInstance()
        {
            return (Button)UIPackage.CreateObject("资源更新", "Button");
        }

        public static Task<Button> CreateInstanceAsync()
        {
            TaskCompletionSource<Button> tcs = new TaskCompletionSource<Button>();
            UIPackage.CreateObjectAsync("资源更新", "Button", (go) => tcs.SetResult(go as Button));
            return tcs.Task;
        }

        public static Button GetFromPool(GObject gObject)
        {
            return (Button)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            button = GetControllerAt(0);
            n0 = (GGraph)GetChildAt(0);
            n1 = (GGraph)GetChildAt(1);
            n2 = (GGraph)GetChildAt(2);
        }
    }
}