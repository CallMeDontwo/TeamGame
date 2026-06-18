/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_SPIN : GComponent
    {
        public const string URL = "ui://hoqdodep995r4t";
        public const string UIResName = "Com_SPIN";
        public const string UIPackageName = "超级魔方";

        public Controller C_Num { get; private set; }

        public static Com_SPIN CreateInstance()
        {
            return (Com_SPIN)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_SPIN> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_SPIN> tcs = new TaskCompletionSource<Com_SPIN>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_SPIN));
            return tcs.Task;
        }

        public static Com_SPIN GetFromPool(GObject gObject)
        {
            return (Com_SPIN)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Num = GetControllerAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}