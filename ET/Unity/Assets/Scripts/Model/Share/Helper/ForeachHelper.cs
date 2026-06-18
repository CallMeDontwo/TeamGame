using System;
using System.Collections.Generic;

namespace ET
{
    public static class ForeachHelper
    {
        public static void For(int start, int end, Action<int> action)
        {
            for (int i = start; i < end; i++)
            {
                action(i);
            }
        }

        public static IEnumerable<int> Each(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                yield return i;
            }
        }

        public static IList<T> Foreach<T>(this IList<T> list, Action<int, T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(i, list[i]);
            }
            return list;
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> values, Action<T> action)
        {
            foreach (var item in values)
            {
                action(item);
            }
            return values;
        }

        public static IDictionary<T, K> Foreach<T, K>(this IDictionary<T, K> dictionary, Action<T, K> action)
        {
            foreach (var kv in dictionary)
            {
                action(kv.Key, kv.Value);
            }
            return dictionary;
        }

        public static IDictionary<T, K> ForeachFunc<T, K>(this IDictionary<T, K> dictionary, Func<T, K, bool> func)
        {
            foreach (var kv in dictionary)
            {
                if (!func(kv.Key, kv.Value))
                {
                    break;
                }
            }
            return dictionary;
        }
    }
}