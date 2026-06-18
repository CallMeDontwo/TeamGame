/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_AwardShow : GComponent
    {
        public const string URL = "ui://hoqdodepw2zw54";
        public const string UIResName = "Com_AwardShow";
        public const string UIPackageName = "超级魔方";

        public Controller C_Type { get; private set; }
        public GTextField Text_Total { get; private set; }
        public GLoader3D Loader_Coin { get; private set; }
        public GTextField Text_Current { get; private set; }
        public GTextField Text_Num1 { get; private set; }
        public GTextField Text_X { get; private set; }
        public GTextField Text_Num2 { get; private set; }
        public GTextField Text_Equal { get; private set; }
        public GTextField Text_Res { get; private set; }
        public GTextField Text_Double { get; private set; }

        public static Com_AwardShow CreateInstance()
        {
            return (Com_AwardShow)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_AwardShow> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_AwardShow> tcs = new TaskCompletionSource<Com_AwardShow>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_AwardShow));
            return tcs.Task;
        }

        public static Com_AwardShow GetFromPool(GObject gObject)
        {
            return (Com_AwardShow)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Text_Total = (GTextField)GetChildAt(2);
            Loader_Coin = (GLoader3D)GetChildAt(3);
            Text_Current = (GTextField)GetChildAt(4);
            Text_Num1 = (GTextField)GetChildAt(6);
            Text_X = (GTextField)GetChildAt(7);
            Text_Num2 = (GTextField)GetChildAt(8);
            Text_Equal = (GTextField)GetChildAt(9);
            Text_Res = (GTextField)GetChildAt(10);
            Text_Double = (GTextField)GetChildAt(12);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}