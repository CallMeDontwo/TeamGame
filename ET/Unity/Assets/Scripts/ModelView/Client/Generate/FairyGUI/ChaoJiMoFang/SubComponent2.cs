/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SubComponent2 : GComponent
    {
        public const string URL = "ui://hoqdodep995r50";
        public const string UIResName = "SubComponent2";
        public const string UIPackageName = "超级魔方";

        public GRichTextField Text_Mark { get; private set; }
        public ChaoJiMoFang.Com_LittleMaryItems Label_MaryItems { get; private set; }

        public static SubComponent2 CreateInstance()
        {
            return (SubComponent2)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SubComponent2> CreateInstanceAsync()
        {
            TaskCompletionSource<SubComponent2> tcs = new TaskCompletionSource<SubComponent2>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SubComponent2));
            return tcs.Task;
        }

        public static SubComponent2 GetFromPool(GObject gObject)
        {
            return (SubComponent2)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_Mark = (GRichTextField)GetChildAt(3);
            Label_MaryItems = (ChaoJiMoFang.Com_LittleMaryItems)GetChildAt(5);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}