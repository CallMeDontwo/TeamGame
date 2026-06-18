/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Com_Paid : GLabel
    {
        public const string URL = "ui://hoqdodepnilu5d";
        public const string UIResName = "Com_Paid";
        public const string UIPackageName = "超级魔方";


        public static Com_Paid CreateInstance()
        {
            return (Com_Paid)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Com_Paid> CreateInstanceAsync()
        {
            TaskCompletionSource<Com_Paid> tcs = new TaskCompletionSource<Com_Paid>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Com_Paid));
            return tcs.Task;
        }

        public static Com_Paid GetFromPool(GObject gObject)
        {
            return (Com_Paid)gObject;
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