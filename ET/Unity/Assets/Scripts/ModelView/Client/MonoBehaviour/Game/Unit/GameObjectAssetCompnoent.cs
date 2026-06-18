using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using YooAsset;

namespace ET
{
    [EnableClass]
    public class GameObjectAssetCompnoent : MonoBehaviour
    {
        public readonly List<AssetHandle> Assets = ObjectPool.Instance.Fetch<List<AssetHandle>>();

        private void OnDestroy()
        {
            this.Assets.Where(item => item.IsValid).Foreach(asset => asset.Dispose());
            this.Assets.Clear();
            ObjectPool.Instance?.Recycle(this.Assets);
        }
    }
}