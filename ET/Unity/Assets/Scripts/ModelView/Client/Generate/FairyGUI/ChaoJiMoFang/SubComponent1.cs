/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using System.Threading.Tasks;
using FairyGUI;
using FairyGUI.Utils;

namespace ET.ChaoJiMoFang
{
    [EnableClass]
    public partial class SubComponent1 : GComponent
    {
        public const string URL = "ui://hoqdodep995r4z";
        public const string UIResName = "SubComponent1";
        public const string UIPackageName = "超级魔方";

        public GList List_List1 { get; private set; }
        public GList List_List2 { get; private set; }
        public GList List_List3 { get; private set; }
        public GGraph Graph_Line1 { get; private set; }
        public GGraph Graph_Line2 { get; private set; }
        public GGraph Graph_Line3 { get; private set; }
        public GGraph Graph_TL2BR { get; private set; }
        public GGraph Graph_TR2BL { get; private set; }
        public GLoader3D Loader_Start { get; private set; }

        public static SubComponent1 CreateInstance()
        {
            return (SubComponent1)UIPackage.CreateObject(UIPackageName, UIResName);
        }

        public static Task<SubComponent1> CreateInstanceAsync()
        {
            TaskCompletionSource<SubComponent1> tcs = new TaskCompletionSource<SubComponent1>();
            UIPackage.CreateObjectAsync(UIPackageName, UIResName, (go) => tcs.SetResult(go as SubComponent1));
            return tcs.Task;
        }

        public static SubComponent1 GetFromPool(GObject gObject)
        {
            return (SubComponent1)gObject;
        }

        public override void ConstructFromXML(XML xml)
        {
            base.ConstructFromXML(xml);

            name = UIResName;
            List_List1 = (GList)GetChildAt(5);
            List_List2 = (GList)GetChildAt(6);
            List_List3 = (GList)GetChildAt(7);
            Graph_Line1 = (GGraph)GetChildAt(8);
            Graph_Line2 = (GGraph)GetChildAt(9);
            Graph_Line3 = (GGraph)GetChildAt(10);
            Graph_TL2BR = (GGraph)GetChildAt(11);
            Graph_TR2BL = (GGraph)GetChildAt(12);
            Loader_Start = (GLoader3D)GetChildAt(13);

            this.AfterCreate();
        }

        partial void AfterCreate();
    }
}