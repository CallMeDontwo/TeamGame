/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_JPAward : GComponent
    {
        public const string URL = "ui://hoqdodepdxvn5a";
        public const string UIResName = "Com_JPAward";
        public const string UIPackageName = "超级魔方";

        public Controller C_Type { get; private set; }
        public GTextField Text_Total { get; private set; }
        public GLoader3D Loader_Coin { get; private set; }
        public GTextField Text_Current { get; private set; }
        public GTextField Text_Double { get; private set; }

        public static Com_JPAward CreateInstance()
        {
            return (Com_JPAward)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_JPAward> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_JPAward> tcs = new TaskCompletionSource<Com_JPAward>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_JPAward));
            return tcs.Task;
        }

        public static Com_JPAward GetFromPool(GObject gObject)
        {
            return (Com_JPAward)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Text_Total = (GTextField)GetChildAt(8);
            Loader_Coin = (GLoader3D)GetChildAt(9);
            Text_Current = (GTextField)GetChildAt(10);
            Text_Double = (GTextField)GetChildAt(11);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}