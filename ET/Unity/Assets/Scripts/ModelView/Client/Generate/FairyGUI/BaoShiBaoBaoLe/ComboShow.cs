/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class ComboShow : GComponent
    {
        public const string URL = "ui://oy07mpfwlzui2i";
        public const string UIResName = "ComboShow";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Type { get; private set; }
        public GRichTextField Text_Combo0 { get; private set; }
        public GRichTextField Text_Combo1 { get; private set; }
        public GRichTextField Text_Combo2 { get; private set; }
        public GRichTextField Text_Combo3 { get; private set; }
        public GRichTextField Text_Combo4 { get; private set; }

        public static ComboShow CreateInstance()
        {
            return (ComboShow)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<ComboShow> CreateInstanceAsync()
        {
            TaskCompletionSource<ComboShow> tcs = new TaskCompletionSource<ComboShow>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as ComboShow));
            return tcs.Task;
        }

        public static ComboShow GetFromPool(GObject gObject)
        {
            return (ComboShow)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Text_Combo0 = (GRichTextField)GetChildAt(1);
            Text_Combo1 = (GRichTextField)GetChildAt(2);
            Text_Combo2 = (GRichTextField)GetChildAt(3);
            Text_Combo3 = (GRichTextField)GetChildAt(4);
            Text_Combo4 = (GRichTextField)GetChildAt(5);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}