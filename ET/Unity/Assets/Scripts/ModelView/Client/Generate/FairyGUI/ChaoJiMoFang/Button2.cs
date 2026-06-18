/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class Button2 : GButton
    {
        public const string URL = "ui://hoqdodepee5c6d";
        public const string UIResName = "Button2";
        public const string UIPackageName = "超级魔方";


        public static Button2 CreateInstance()
        {
            return (Button2)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<Button2> CreateInstanceAsync()
        {
            TaskCompletionSource<Button2> tcs = new TaskCompletionSource<Button2>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as Button2));
            return tcs.Task;
        }

        public static Button2 GetFromPool(GObject gObject)
        {
            return (Button2)gObject;
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