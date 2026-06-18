/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_OUT : GLabel
    {
        public const string URL = "ui://hoqdodep995r4w";
        public const string UIResName = "Com_OUT";
        public const string UIPackageName = "超级魔方";

        public Controller C_Status { get; private set; }

        public static Com_OUT CreateInstance()
        {
            return (Com_OUT)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_OUT> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_OUT> tcs = new TaskCompletionSource<Com_OUT>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_OUT));
            return tcs.Task;
        }

        public static Com_OUT GetFromPool(GObject gObject)
        {
            return (Com_OUT)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Status = GetControllerAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}