/** This is an automatically generated class by FairyGUI. Please do not modify it. **/

using FairyGUI;

namespace ET.TeamGame
{
    [PackageBinder]
    public class TeamGameBinder : Object, IPackageBinder
    {
        public void BindAll()
        {
            UIObjectFactory.SetPackageItemExtension(Pro_hp.URL, typeof(Pro_hp));
            UIObjectFactory.SetPackageItemExtension(TeamMain.URL, typeof(TeamMain));
        }
    }
}