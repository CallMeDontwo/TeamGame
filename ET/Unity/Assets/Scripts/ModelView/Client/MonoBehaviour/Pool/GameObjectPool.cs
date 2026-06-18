using System.Collections.Generic;
using UnityEngine;
using YooAsset;

namespace ET
{
    /// <summary>
    /// 显示层对象池 — 管理 Unity GameObject 的复用
    /// 按 prefabPath 分组,每组分持一个 YooAsset AssetHandle
    /// </summary>
    [EnableClass]
    public static class GameObjectPool
    {
        [EnableClass]
        private class PrefabPool
        {
            public AssetHandle Handle;
            public readonly Stack<GameObject> Inactive = new();
            public GameObject PrefabAsset;
        }

        [StaticField]
        private static  Dictionary<string, PrefabPool> _pools = new Dictionary<string, PrefabPool>();

        // ═══════════════════════════════════════
        //  公共接口
        // ═══════════════════════════════════════

        /// <summary>从池中获取或创建实例</summary>
        public static async ETTask<GameObject> FetchAsync(string prefabPath)
        {
            if (string.IsNullOrEmpty(prefabPath)) return null;

            if (!_pools.TryGetValue(prefabPath, out var pool))
            {
                pool = new PrefabPool();
                _pools[prefabPath] = pool;
            }

            // 池中有空闲 → 直接取
            if (pool.Inactive.Count > 0)
            {
                var go = pool.Inactive.Pop();
                if (go != null)
                {
                    go.SetActive(true);
                    var marker = go.GetComponent<PooledGameObject>();
                    marker?.NotifySpawn();
                    return go;
                }
            }
            Log.Debug(prefabPath);
            // 池为空 → 加载预制体并实例化
            if (pool.PrefabAsset == null)
            {
                if (pool.Handle == null || !pool.Handle.IsValid)
                {
                    pool.Handle = YooAssets.LoadAssetAsync<GameObject>(prefabPath);
                }

                // 等待加载完成（并发调用时可能第一个已经在加载中）
                if (!pool.Handle.IsDone)
                {
                    await pool.Handle.Task;
                }

                if (pool.Handle.Status != EOperationStatus.Succeed)
                {
                    Log.Error($"GameObjectPool: 加载预制体失败 {prefabPath}, status={pool.Handle.Status}");
                    pool.Handle.Release();
                    pool.Handle = null;
                    _pools.Remove(prefabPath);
                    return null;
                }

                pool.PrefabAsset = pool.Handle.AssetObject as GameObject;
                if (pool.PrefabAsset == null)
                {
                    Log.Error($"GameObjectPool: 预制体资源为空 {prefabPath}");
                    pool.Handle.Release();
                    pool.Handle = null;
                    _pools.Remove(prefabPath);
                    return null;
                }
            }

            var instance = GameObject.Instantiate(pool.PrefabAsset);
            instance.name = pool.PrefabAsset.name;

            var pooled = instance.EnsureComponent<PooledGameObject>();
            pooled.PrefabPath = prefabPath;
            pooled.IsFromPool = true;
            pooled.NotifySpawn();

            return instance;
        }

        /// <summary>回收对象到池</summary>
        public static void Recycle(GameObject go)
        {
            if (go == null) return;

            var pooled = go.GetComponent<PooledGameObject>();
            if (pooled == null || !pooled.IsFromPool)
            {
                GameObject.Destroy(go);
                return;
            }

            // 通知回收回调
            pooled.NotifyRecycle();
            pooled.CancelInvoke();

            // 停止粒子
            var particles = go.GetComponentsInChildren<ParticleSystem>();
            foreach (var ps in particles) { if (ps.isPlaying) ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); }

            // 停用并清理父节点
            go.SetActive(false);
            go.transform.SetParent(null);

            // 入池
            if (_pools.TryGetValue(pooled.PrefabPath, out var pool))
            {
                pool.Inactive.Push(go);
            }
            else
            {
                GameObject.Destroy(go);
            }
        }

        /// <summary>预热 N 个实例</summary>
        public static async ETTask PrewarmAsync(string prefabPath, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var go = await FetchAsync(prefabPath);
                if (go != null)
                {
                    go.SetActive(false);
                    Recycle(go);
                }
            }
        }

        /// <summary>清理指定预制体的整个池</summary>
        public static void Clear(string prefabPath)
        {
            if (!_pools.TryGetValue(prefabPath, out var pool)) return;

            while (pool.Inactive.Count > 0)
            {
                var go = pool.Inactive.Pop();
                if (go != null)
                {
                    var pooled = go.GetComponent<PooledGameObject>();
                    if (pooled != null) pooled.IsFromPool = false;
                    GameObject.Destroy(go);
                }
            }

            pool.Handle?.Release();
            _pools.Remove(prefabPath);
        }

        /// <summary>清理所有池</summary>
        public static void ClearAll()
        {
            foreach (var key in new List<string>(_pools.Keys))
            {
                Clear(key);
            }
            _pools.Clear();
        }
    }
}
