/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_Credit : GLabel
    {
        public const string URL = "ui://hoqdodep995r4x";
        public const string UIResName = "Com_Credit";
        public const string UIPackageName = "超级魔方";


        public static Com_Credit CreateInstance()
        {
            return (Com_Credit)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_Credit> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_Credit> tcs = new TaskCompletionSource<Com_Credit>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_Credit));
            return tcs.Task;
        }

        public static Com_Credit GetFromPool(GObject gObject)
        {
            return (Com_Credit)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}