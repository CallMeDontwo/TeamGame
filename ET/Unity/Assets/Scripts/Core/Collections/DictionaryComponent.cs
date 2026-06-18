using System;
using System.Collections.Generic;

namespace ET
{
    public class DictionaryComponent<TKey, TValue> : Dictionary<TKey, TValue>, IDisposable
    {
        public DictionaryComponent()
        {
        }

        public static DictionaryComponent<TKey, TValue> Create()
        {
            return ObjectPool.Instance.Fetch(typeof(DictionaryComponent<TKey, TValue>)) as DictionaryComponent<TKey, TValue>;
        }

        public void Dispose()
        {
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}