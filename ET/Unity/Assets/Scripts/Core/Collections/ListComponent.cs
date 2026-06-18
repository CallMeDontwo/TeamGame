using System;
using System.Collections.Generic;

namespace ET
{
    public class ListComponent<T> : List<T>, IDisposable
    {
        public ListComponent()
        {
        }

        public static ListComponent<T> Create()
        {
            return ObjectPool.Instance.Fetch(typeof(ListComponent<T>)) as ListComponent<T>;
        }

        public static ListComponent<T> Create(IEnumerable<T> values)
        {
            ListComponent<T> list = ObjectPool.Instance.Fetch(typeof(ListComponent<T>)) as ListComponent<T>;
            list.AddRange(values);
            return list;
        }

        public void Dispose()
        {
            if (this.Capacity > 64) // 超过64，让gc回收
            {
                return;
            }
            this.Clear();
            ObjectPool.Instance.Recycle(this);
        }
    }
}