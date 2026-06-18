using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YooAsset.Editor;

namespace ET
{
    internal class PackFairyUI : IPackRule
    {
        public PackRuleResult GetPackRuleResult(PackRuleData data)
        {
            string path = Path.GetDirectoryName(data.AssetPath);
            string bundleName = Path.Combine(path, data.AssetPath.EndsWith(".bytes") ? "desc" : "res");
            PackRuleResult result = new PackRuleResult(bundleName, DefaultPackRule.AssetBundleFileExtension);
            return result;
        }
    }
}