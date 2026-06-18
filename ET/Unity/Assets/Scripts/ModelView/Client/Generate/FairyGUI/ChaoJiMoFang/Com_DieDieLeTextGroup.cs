/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_DieDieLeTextGroup : GComponent
    {
        public const string URL = "ui://hoqdodepvdhu5l";
        public const string UIResName = "Com_DieDieLeTextGroup";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple0 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple1 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple2 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple3 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple4 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple5 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple6 { get; private set; }
        public ChaoJiMoFang.Com_DieDieLeText Text_SlotMultiple7 { get; private set; }

        public static Com_DieDieLeTextGroup CreateInstance()
        {
            return (Com_DieDieLeTextGroup)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_DieDieLeTextGroup> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_DieDieLeTextGroup> tcs = new TaskCompletionSource<Com_DieDieLeTextGroup>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_DieDieLeTextGroup));
            return tcs.Task;
        }

        public static Com_DieDieLeTextGroup GetFromPool(GObject gObject)
        {
            return (Com_DieDieLeTextGroup)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_SlotMultiple0 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(0);
            Text_SlotMultiple1 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(1);
            Text_SlotMultiple2 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(2);
            Text_SlotMultiple3 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(3);
            Text_SlotMultiple4 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(4);
            Text_SlotMultiple5 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(5);
            Text_SlotMultiple6 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(6);
            Text_SlotMultiple7 = (ChaoJiMoFang.Com_DieDieLeText)GetChildAt(7);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}