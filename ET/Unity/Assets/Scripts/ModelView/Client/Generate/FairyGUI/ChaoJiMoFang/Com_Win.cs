/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_Win : GLabel
    {
        public const string URL = "ui://hoqdodepnilu5e";
        public const string UIResName = "Com_Win";
        public const string UIPackageName = "超级魔方";


        public static Com_Win CreateInstance()
        {
            return (Com_Win)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_Win> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_Win> tcs = new TaskCompletionSource<Com_Win>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_Win));
            return tcs.Task;
        }

        public static Com_Win GetFromPool(GObject gObject)
        {
            return (Com_Win)gObject;
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