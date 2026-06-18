/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.ShiTuoLingFuMo
{
    [PackageBinder]
    public class ShiTuoLingFuMoBinder : Object, IPackageBinder
    {
        public void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(ProgressBar1.URL, typeof(ProgressBar1));
            UIObjectFactory.SetPackageItemExtension(Button1.URL, typeof(Button1));
            UIObjectFactory.SetPackageItemExtension(MainComponent.URL, typeof(MainComponent));
        }
    }
}