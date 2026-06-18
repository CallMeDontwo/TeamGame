/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Button4 : GButton
    {
        public const string URL = "ui://hoqdodepee5c6l";
        public const string UIResName = "Button4";
        public const string UIPackageName = "超级魔方";


        public static Button4 CreateInstance()
        {
            return (Button4)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Button4> CreateInstanceAsync()
        {
            TaskCompletionSource<Button4> tcs = new TaskCompletionSource<Button4>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Button4));
            return tcs.Task;
        }

        public static Button4 GetFromPool(GObject gObject)
        {
            return (Button4)gObject;
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