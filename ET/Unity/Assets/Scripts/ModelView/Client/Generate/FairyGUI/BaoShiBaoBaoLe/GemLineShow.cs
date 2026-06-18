/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemLineShow : GLabel
    {
        public const string URL = "ui://oy07mpfwowhb9l";
        public const string UIResName = "GemLineShow";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Quality { get; private set; }
        public Controller C_Multiple { get; private set; }
        public Controller C_Gem { get; private set; }

        public static GemLineShow CreateInstance()
        {
            return (GemLineShow)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemLineShow> CreateInstanceAsync()
        {
            TaskCompletionSource<GemLineShow> tcs = new TaskCompletionSource<GemLineShow>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemLineShow));
            return tcs.Task;
        }

        public static GemLineShow GetFromPool(GObject gObject)
        {
            return (GemLineShow)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Quality = GetControllerAt(0);
            C_Multiple = GetControllerAt(1);
            C_Gem = GetControllerAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}