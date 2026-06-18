/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class OnlyGenerateGem : GComponent
    {
        public const string URL = "ui://oy07mpfwtuks9m";
        public const string UIResName = "OnlyGenerateGem";
        public const string UIPackageName = "宝石爆爆乐";


        public static OnlyGenerateGem CreateInstance()
        {
            return (OnlyGenerateGem)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<OnlyGenerateGem> CreateInstanceAsync()
        {
            TaskCompletionSource<OnlyGenerateGem> tcs = new TaskCompletionSource<OnlyGenerateGem>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as OnlyGenerateGem));
            return tcs.Task;
        }

        public static OnlyGenerateGem GetFromPool(GObject gObject)
        {
            return (OnlyGenerateGem)gObject;
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