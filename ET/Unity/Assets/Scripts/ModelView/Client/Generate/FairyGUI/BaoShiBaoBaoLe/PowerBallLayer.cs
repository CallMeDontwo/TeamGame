/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.BaoShiBaoBaoLe
{
    [EnableClass]
    public partial class PowerBallLayer : GComponent
    {
        public const string URL = "ui://oy07mpfwrdxpb4";
        public const string UIResName = "PowerBallLayer";
        public const string UIPackageName = "宝石爆爆乐";


        public static PowerBallLayer CreateInstance()
        {
            return (PowerBallLayer)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<PowerBallLayer> CreateInstanceAsync()
        {
            TaskCompletionSource<PowerBallLayer> tcs = new TaskCompletionSource<PowerBallLayer>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as PowerBallLayer));
            return tcs.Task;
        }

        public static PowerBallLayer GetFromPool(GObject gObject)
        {
            return (PowerBallLayer)gObject;
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