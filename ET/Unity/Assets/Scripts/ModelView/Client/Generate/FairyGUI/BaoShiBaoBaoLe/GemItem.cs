/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class GemItem : GLabel
    {
        public const string URL = "ui://oy07mpfwsnyy2";
        public const string UIResName = "GemItem";
        public const string UIPackageName = "宝石爆爆乐";

        public Controller C_Type { get; private set; }
        public GLoader3D Loader_Normal { get; private set; }
        public GLoader3D Loader_Color { get; private set; }
        public GLoader3D Loader_SuperBook { get; private set; }
        public GLoader3D Loader_Boom { get; private set; }
        public Transition T_GemFall { get; private set; }

        public static GemItem CreateInstance()
        {
            return (GemItem)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<GemItem> CreateInstanceAsync()
        {
            TaskCompletionSource<GemItem> tcs = new TaskCompletionSource<GemItem>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as GemItem));
            return tcs.Task;
        }

        public static GemItem GetFromPool(GObject gObject)
        {
            return (GemItem)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            C_Type = GetControllerAt(0);
            Loader_Normal = (GLoader3D)GetChildAt(0);
            Loader_Color = (GLoader3D)GetChildAt(1);
            Loader_SuperBook = (GLoader3D)GetChildAt(2);
            Loader_Boom = (GLoader3D)GetChildAt(3);
            T_GemFall = GetTransitionAt(0);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}