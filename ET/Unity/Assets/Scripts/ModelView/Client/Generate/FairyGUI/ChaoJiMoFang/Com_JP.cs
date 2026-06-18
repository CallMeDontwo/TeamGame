/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_JP : GLabel
    {
        public const string URL = "ui://hoqdodep995r4s";
        public const string UIResName = "Com_JP";
        public const string UIPackageName = "超级魔方";


        public static Com_JP CreateInstance()
        {
            return (Com_JP)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_JP> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_JP> tcs = new TaskCompletionSource<Com_JP>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_JP));
            return tcs.Task;
        }

        public static Com_JP GetFromPool(GObject gObject)
        {
            return (Com_JP)gObject;
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