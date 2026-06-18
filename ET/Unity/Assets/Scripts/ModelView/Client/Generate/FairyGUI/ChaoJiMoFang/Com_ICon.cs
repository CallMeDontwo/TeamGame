/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_ICon : GLabel
    {
        public const string URL = "ui://hoqdodep995r4y";
        public const string UIResName = "Com_ICon";
        public const string UIPackageName = "超级魔方";

        public Transition T_Shiny { get; private set; }
        public Transition T_Shiny1 { get; private set; }

        public static Com_ICon CreateInstance()
        {
            return (Com_ICon)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_ICon> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_ICon> tcs = new TaskCompletionSource<Com_ICon>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_ICon));
            return tcs.Task;
        }

        public static Com_ICon GetFromPool(GObject gObject)
        {
            return (Com_ICon)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            T_Shiny = GetTransitionAt(0);
            T_Shiny1 = GetTransitionAt(1);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}