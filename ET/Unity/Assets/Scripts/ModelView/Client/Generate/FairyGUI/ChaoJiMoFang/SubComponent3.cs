/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SubComponent3 : GComponent
    {
        public const string URL = "ui://hoqdodep995r51";
        public const string UIResName = "SubComponent3";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.Com_DieDieLeListGroup Com_ListGroup { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeTextGroup Com_TextGroup { get; private set; }

        public static SubComponent3 CreateInstance()
        {
            return (SubComponent3)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SubComponent3> CreateInstanceAsync()
        {
            TaskCompletionSource<SubComponent3> tcs = new TaskCompletionSource<SubComponent3>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SubComponent3));
            return tcs.Task;
        }

        public static SubComponent3 GetFromPool(GObject gObject)
        {
            return (SubComponent3)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Com_ListGroup = (ChaoJiMoFang.Com_DieDieLeListGroup)GetChildAt(1);
            Com_TextGroup = (ChaoJiMoFang.Com_DieDieLeTextGroup)GetChildAt(2);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}