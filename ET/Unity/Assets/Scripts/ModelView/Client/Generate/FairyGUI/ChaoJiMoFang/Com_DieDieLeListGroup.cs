/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_DieDieLeListGroup : GComponent
    {
        public const string URL = "ui://hoqdodepvdhu5h";
        public const string UIResName = "Com_DieDieLeListGroup";
        public const string UIPackageName = "超级魔方";


        public static Com_DieDieLeListGroup CreateInstance()
        {
            return (Com_DieDieLeListGroup)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_DieDieLeListGroup> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_DieDieLeListGroup> tcs = new TaskCompletionSource<Com_DieDieLeListGroup>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_DieDieLeListGroup));
            return tcs.Task;
        }

        public static Com_DieDieLeListGroup GetFromPool(GObject gObject)
        {
            return (Com_DieDieLeListGroup)gObject;
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