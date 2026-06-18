/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET._Component_Public
{
    [PackageBinder]
    public class _Component_PublicBinder : Object, IPackageBinder
    {
        public void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(Btn_Normal.URL, typeof(Btn_Normal));
            UIObjectFactory.SetPackageItemExtension(Btn_Exit.URL, typeof(Btn_Exit));
            UIObjectFactory.SetPackageItemExtension(LoadingComponent.URL, typeof(LoadingComponent));
            UIObjectFactory.SetPackageItemExtension(ProgressBar1.URL, typeof(ProgressBar1));
            UIObjectFactory.SetPackageItemExtension(TipWindowComponent.URL, typeof(TipWindowComponent));
            UIObjectFactory.SetPackageItemExtension(ReconnectWindow.URL, typeof(ReconnectWindow));
        }
    }
}