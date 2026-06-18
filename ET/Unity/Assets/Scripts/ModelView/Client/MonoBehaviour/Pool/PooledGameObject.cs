using UnityEngine;

namespace ET
{
    /// <summary>
    /// 池化标记组件 — 挂载在每个池物体上,记录来源 prefabPath 和回收回调
    /// 由 GameObjectPool 自动添加和移除
    /// </summary>
    [DisallowMultipleComponent]
    [EnableClass]
    public class PooledGameObject : MonoBehaviour
    {
        /// <summary>来自哪个预制体路径(YooAsset address)</summary>
        public string PrefabPath { get; set; }

        /// <summary>是否来自对象池</summary>
        public bool IsFromPool { get; set; }

        /// <summary>挂载的池化回调组件列表(缓存)</summary>
        private IPoolable[] _poolables;

        private void Awake()
        {
            _poolables = GetComponentsInChildren<IPoolable>(true);
        }

        /// <summary>通知所有回调组件：已从池取出</summary>
        public void NotifySpawn()
        {
            if (_poolables == null) _poolables = GetComponentsInChildren<IPoolable>(true);
            foreach (var p in _poolables) p.OnSpawn();
        }

        /// <summary>通知所有回调组件：即将回池</summary>
        public void NotifyRecycle()
        {
            if (_poolables == null) _poolables = GetComponentsInChildren<IPoolable>(true);
            foreach (var p in _poolables) p.OnRecycle();
        }

        /// <summary>停止所有粒子并注册自动回收回调（VFX专用）</summary>
        public void SetupAutoRecycle()
        {
            var particles = GetComponentsInChildren<ParticleSystem>();
            if (particles.Length == 0) return;

            float maxDuration = 0f;
            foreach (var ps in particles)
            {
                var main = ps.main;
                main.stopAction = ParticleSystemStopAction.Callback;
                float dur = main.duration + main.startLifetime.constantMax;
                if (dur > maxDuration) maxDuration = dur;
            }

            if (maxDuration <= 0) maxDuration = 2f;
            Invoke(nameof(DoRecycle), maxDuration);
        }

        private void DoRecycle()
        {
            if (IsFromPool)
            {
                GameObjectPool.Recycle(gameObject);
            }
        }

        private void OnDestroy()
        {
            CancelInvoke();
        }
    }
}
