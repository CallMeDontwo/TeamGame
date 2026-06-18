/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class CoinPusherComponent : GComponent
    {
        public const string URL = "ui://hoqdodepg3y35o";
        public const string UIResName = "CoinPusherComponent";
        public const string UIPackageName = "超级魔方";

        public ChaoJiMoFang.SettingsComponent Com_Settings { get; private set; }

        public static CoinPusherComponent CreateInstance()
        {
            return (CoinPusherComponent)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<CoinPusherComponent> CreateInstanceAsync()
        {
            TaskCompletionSource<CoinPusherComponent> tcs = new TaskCompletionSource<CoinPusherComponent>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as CoinPusherComponent));
            return tcs.Task;
        }

        public static CoinPusherComponent GetFromPool(GObject gObject)
        {
            return (CoinPusherComponent)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            Com_Settings = (ChaoJiMoFang.SettingsComponent)GetChildAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}