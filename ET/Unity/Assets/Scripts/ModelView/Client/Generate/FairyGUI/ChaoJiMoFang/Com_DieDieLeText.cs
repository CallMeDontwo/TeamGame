/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_DieDieLeText : GComponent
    {
        public const string URL = "ui://hoqdodepvdhu5k";
        public const string UIResName = "Com_DieDieLeText";
        public const string UIPackageName = "超级魔方";

        public GTextField Text_NewText { get; private set; }
        public GTextField Text_OldText { get; private set; }
        public Transition T_Show { get; private set; }

        public static Com_DieDieLeText CreateInstance()
        {
            return (Com_DieDieLeText)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_DieDieLeText> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_DieDieLeText> tcs = new TaskCompletionSource<Com_DieDieLeText>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_DieDieLeText));
            return tcs.Task;
        }

        public static Com_DieDieLeText GetFromPool(GObject gObject)
        {
            return (Com_DieDieLeText)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Text_NewText = (GTextField)GetChildAt(0);
            Text_OldText = (GTextField)GetChildAt(1);
            T_Show = GetTransitionAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}