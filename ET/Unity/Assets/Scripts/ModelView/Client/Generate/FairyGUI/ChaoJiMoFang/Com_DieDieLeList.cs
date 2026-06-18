/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_DieDieLeList : GComponent
    {
        public const string URL = "ui://hoqdodepvdhu5m";
        public const string UIResName = "Com_DieDieLeList";
        public const string UIPackageName = "超级魔方";


        public static Com_DieDieLeList CreateInstance()
        {
            return (Com_DieDieLeList)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_DieDieLeList> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_DieDieLeList> tcs = new TaskCompletionSource<Com_DieDieLeList>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_DieDieLeList));
            return tcs.Task;
        }

        public static Com_DieDieLeList GetFromPool(GObject gObject)
        {
            return (Com_DieDieLeList)gObject;
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