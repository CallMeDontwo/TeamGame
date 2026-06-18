using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace ET
{
    internal enum IntEnum : int
    {
        A,
        B,
    }

    internal class RefStateMachine : IAsyncStateMachine
    {
        public void MoveNext() { }
        public void SetStateMachine(IAsyncStateMachine stateMachine) { }
    }

    internal class ReferenceTypes : MonoBehaviour
    {
        public void RefNullable()
        {
            sbyte? a = 0;
            byte? b = 0;
            short? c = 0;
            ushort? d = 0;
            int? e = 0;
            uint? f = 0;
            long? g = 0;
            ulong? h = 0;
            float? i = 0;
            double? j = 0;
            decimal? k = 0;
            bool? l = true;
            char? m = char.MinValue;
            object oa = a, ob = b, oc = c, od = d, oe = e, of = f;
            object og = g, oh = h, oi = i, oj = j, ok = k, ol = l, om = m;
        }

        public void RefUnityEngine()
        {
            FindObjectOfType<GameObject>();
            FindObjectOfType<GameObject>(true);
            FindObjectsOfType<GameObject>();
            FindObjectsOfType<GameObject>(true);
            Instantiate<GameObject>(null);
            Instantiate<GameObject>(null, null);
            Instantiate<GameObject>(null, null, true);
            Instantiate<GameObject>(null, Vector3.zero, Quaternion.identity);
            Instantiate<GameObject>(null, Vector3.zero, Quaternion.identity, null);
            this.GetComponent<ReferenceTypes>();
            this.GetComponents<ReferenceTypes>();
            this.GetComponents(new List<object>());
            this.GetComponentInChildren<ReferenceTypes>();
            this.GetComponentInChildren<ReferenceTypes>(true);
            this.GetComponentsInChildren<ReferenceTypes>();
            this.GetComponentsInChildren<ReferenceTypes>(true);
            this.GetComponentsInChildren(new List<object>());
            this.GetComponentsInChildren(true, new List<object>());
            this.GetComponentInParent<ReferenceTypes>();
            this.GetComponentsInParent<ReferenceTypes>();
            this.GetComponentsInParent<ReferenceTypes>(true);
            this.GetComponentsInParent(true, new List<object>());
            this.TryGetComponent(out ReferenceTypes _);
            this.gameObject.AddComponent<ReferenceTypes>();
            this.gameObject.GetComponent<ReferenceTypes>();
            this.gameObject.GetComponents<ReferenceTypes>();
            this.gameObject.GetComponents(new List<object>());
            this.gameObject.GetComponentInChildren<ReferenceTypes>();
            this.gameObject.GetComponentInChildren<ReferenceTypes>(true);
            this.gameObject.GetComponentsInChildren<ReferenceTypes>();
            this.gameObject.GetComponentsInChildren<ReferenceTypes>(true);
            this.gameObject.GetComponentsInChildren(new List<object>());
            this.gameObject.GetComponentsInChildren(true, new List<object>());
            this.gameObject.GetComponentInParent<ReferenceTypes>();
            this.gameObject.GetComponentInParent<ReferenceTypes>(true);
            this.gameObject.GetComponentsInParent<ReferenceTypes>();
            this.gameObject.GetComponentsInParent<ReferenceTypes>(true);
            this.gameObject.GetComponentsInParent(true, new List<object>());
            this.gameObject.TryGetComponent(out ReferenceTypes _);
        }

        public void RefContainer()
        {
            new HashSetComponent<short>();
            new HashSetComponent<ushort>();
            new HashSetComponent<int>();
            new HashSetComponent<long>();
            new HashSetComponent<object>();
            new ListComponent<int>();
            new ListComponent<long>();
            new ListComponent<float>();
            new ListComponent<double>();
            new ListComponent<object>();
            new ListComponent<Vector2>();
            new ListComponent<Vector3>();
            new ListComponent<Vector4>();
            new ListComponent<Quaternion>();
            new Queue<int>();
            new Queue<long>();
            new Queue<object>();
            new Stack<int>();
            new Stack<long>();
            new Stack<object>();
            new DictionaryComponent<int, int>();
            new DictionaryComponent<int, long>();
            new DictionaryComponent<int, object>();
            new DictionaryComponent<long, int>();
            new DictionaryComponent<long, long>();
            new DictionaryComponent<long, object>();
            new DictionaryComponent<object, long>();
            new DictionaryComponent<object, object>();
            new DoubleMap<int, int>();
            new DoubleMap<int, long>();
            new DoubleMap<int, object>();
            new DoubleMap<long, int>();
            new DoubleMap<long, long>();
            new DoubleMap<long, object>();
            new DoubleMap<object, long>();
            new DoubleMap<object, object>();
            new MultiMap<int, int>();
            new MultiMap<int, long>();
            new MultiMap<int, object>();
            new MultiMap<long, int>();
            new MultiMap<long, long>();
            new MultiMap<long, object>();
            new MultiMap<object, long>();
            new MultiMap<object, object>();
            new SortedDictionary<int, int>();
            new SortedDictionary<int, long>();
            new SortedDictionary<int, object>();
            new SortedDictionary<long, int>();
            new SortedDictionary<long, long>();
            new SortedDictionary<long, object>();
            new SortedDictionary<object, long>();
            new SortedDictionary<object, object>();
            new MultiDictionary<int, int, int>();
            new MultiDictionary<long, long, long>();
            new MultiDictionary<int, int, object>();
            new MultiDictionary<long, long, object>();
            new MultiDictionary<object, object, object>();
            new ValueTuple<int, int>(1, 1);
            new ValueTuple<long, long>(1, 1);
            new ValueTuple<object, object>(1, 1);
            new ValueTuple<int, int, int>(1, 1, 1);
            new ValueTuple<long, long, long>(1, 1, 1);
            new ValueTuple<object, object, object>(1, 1, 1);
            new ValueTuple<int, int, int, int>(1, 1, 1, 1);
            new ValueTuple<long, long, long, long>(1, 1, 1, 1);
            new ValueTuple<object, object, object, object>(1, 1, 1, 1);
        }

        public void RefMedthod()
        {
            object Obj1 = new object();
            object Obj2 = new object();
            Activator.CreateInstance<object>();
            EnumHelper.FromString<object>(string.Empty);
            ObjectHelper.Swap(ref Obj1, ref Obj2);
            StringHelper.ListToString(new List<object>());
            System.Linq.Enumerable.Cast<object>(new List<object>());
            System.Linq.Enumerable.Contains(new List<int>(), 0);
            System.Linq.Enumerable.Contains(new List<long>(), 0);
            System.Linq.Enumerable.Contains(new List<object>(), null);
            System.Linq.Enumerable.Select(new List<int>(), item => true);
            System.Linq.Enumerable.Select(new List<long>(), item => true);
            System.Linq.Enumerable.Select(new List<object>(), item => true);
            System.Linq.Enumerable.SequenceEqual(new List<int>(), new List<int>());
            System.Linq.Enumerable.SequenceEqual(new List<long>(), new List<long>());
            System.Linq.Enumerable.SequenceEqual(new List<object>(), new List<object>());
            System.Linq.Enumerable.ToArray(Array.Empty<int>());
            System.Linq.Enumerable.ToArray(Array.Empty<long>());
            System.Linq.Enumerable.ToArray(Array.Empty<object>());
            System.Linq.Enumerable.ToList(Array.Empty<int>());
            System.Linq.Enumerable.ToList(Array.Empty<long>());
            System.Linq.Enumerable.ToList(Array.Empty<object>());
            System.Threading.Tasks.Task.FromCanceled<object>(CancellationToken.None);
            System.Threading.Tasks.Task.FromResult(new object());
            System.Threading.Tasks.Task.Run(() => new object());
            System.Threading.Tasks.Task.WhenAll(new List<System.Threading.Tasks.Task<object>>());
            System.Threading.Tasks.Task.WhenAny(new List<System.Threading.Tasks.Task<object>>());
            YooAsset.YooAssets.LoadAllAssetsSync<UnityEngine.Object>(string.Empty);
            YooAsset.YooAssets.LoadAllAssetsAsync<UnityEngine.Object>(string.Empty);
            YooAsset.YooAssets.LoadAssetSync<UnityEngine.Object>(string.Empty).GetAssetObject<UnityEngine.Object>();
            YooAsset.YooAssets.LoadAssetAsync<UnityEngine.Object>(string.Empty).GetAssetObject<UnityEngine.Object>();
            YooAsset.YooAssets.LoadSubAssetsSync<UnityEngine.Object>(string.Empty).GetSubAssetObject<UnityEngine.Object>(string.Empty);
            YooAsset.YooAssets.LoadSubAssetsSync<UnityEngine.Object>(string.Empty).GetSubAssetObjects<UnityEngine.Object>();
            YooAsset.YooAssets.LoadSubAssetsAsync<UnityEngine.Object>(string.Empty).GetSubAssetObject<UnityEngine.Object>(string.Empty);
            YooAsset.YooAssets.LoadSubAssetsAsync<UnityEngine.Object>(string.Empty).GetSubAssetObjects<UnityEngine.Object>();
        }

        public void RefAsyncMethod()
        {
            TaskAwaiter aw = default;
            RefStateMachine stateMachine = new RefStateMachine();

            {
                var builder = new AsyncVoidMethodBuilder();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult();
            }
            {
                var builder = new AsyncTaskMethodBuilder();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult();
            }
            {
                var builder = new AsyncTaskMethodBuilder<byte>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<bool>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<int>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<long>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<float>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<double>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<object>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new AsyncTaskMethodBuilder<IntEnum>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult();
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<byte>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<bool>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<int>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<long>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<float>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<double>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<object>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
            {
                var builder = new ETAsyncTaskMethodBuilder<IntEnum>();
                builder.Start(ref stateMachine);
                builder.AwaitOnCompleted(ref aw, ref stateMachine);
                builder.AwaitUnsafeOnCompleted(ref aw, ref stateMachine);
                builder.SetException(null);
                builder.SetResult(default);
            }
        }
    }
}