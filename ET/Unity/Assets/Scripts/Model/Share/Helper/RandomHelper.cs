using System;
using System.Collections.Generic;
using Random = System.Random;

namespace ET
{
    public static class RandomHelper
    {
        [StaticField]
        public static readonly Random Shared = new Random(Guid.NewGuid().GetHashCode());

        public static bool RandBool()
        {
            return Shared.Next(2) == 0;
        }

        public static int RandInt32()
        {
            return Shared.Next();
        }

        /// <summary>
        /// 获取lower与Upper之间的随机数,包含下限，不包含上限
        /// </summary>
        public static int RandInt32(int lower, int upper)
        {
            return Shared.Next(lower, upper);
        }

        public static uint RandUInt32()
        {
            return (uint)Shared.Next();
        }

        public static long RandInt64()
        {
#if !NOT_UNITY
            byte[] byte8 = new byte[8];
            Shared.NextBytes(byte8);
            return BitConverter.ToInt64(byte8, 0);
#else
            return Shared.NextInt64();
#endif
        }

        public static long RandInt64(long minValue, long maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("minValue is great than maxValue", "minValue");
            }

            long num = maxValue - minValue;
            return minValue + (long)(Shared.NextDouble() * num);
        }

        public static ulong RandUInt64()
        {
#if !NOT_UNITY
            byte[] byte8 = new byte[8];
            Shared.NextBytes(byte8);
            return BitConverter.ToUInt64(byte8, 0);
#else
            return (ulong)Shared.NextInt64();
#endif
        }

        public static float RandFloat01()
        {
            return RandInt32(0, 1000000) / 1000000f;
        }

        /// <summary>
        /// 百分比概率
        /// </summary>
        public static bool RandomPa(int num)
        {
            return RandInt32(0, 100) < num;
        }

        public static T RandomArray<T>(this IList<T> array)
        {
            return array[RandInt32(0, array.Count)];
        }

        public static T RandomArray_Len2<T>(this IList<T> array)
        {
            return array[RandInt32(0, 2)];
        }

        /// <summary>
        /// 打乱数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arr">要打乱的数组</param>
        public static void BreakRank<T>(this IList<T> arr)
        {
            if (arr != null && arr.Count >= 2)
            {
                for (int i = 0; i < arr.Count; i++)
                {
                    int index = Shared.Next(0, arr.Count);
                    (arr[i], arr[index]) = (arr[index], arr[i]);
                }
            }
        }

        public static int[] GetRandoms(int size, int min, int max)
        {
            int j = 0;
            int[] arr = new int[size];
            //表示键和值对的集合。
            HashSet<int> hashtable = new HashSet<int>();
            while (hashtable.Count < size)
            {
                //返回一个min到max之间的随机数
                int nValue = Shared.Next(min, max);
                // 是否包含特定值
                if (!hashtable.Contains(nValue))
                {
                    //把键和值添加到hashtable
                    arr[j] = nValue;
                    hashtable.Add(nValue);
                    j++;
                }
            }

            return arr;
        }

        /// <summary>
        /// 随机从数组中取若干个不重复的元素，
        /// 为了降低算法复杂度，所以是伪随机，对随机要求不是非常高的逻辑可以用
        /// </summary>
        public static bool GetRandListByCount<T>(IList<T> sourceList, IList<T> destList, int randCount)
        {
            if (randCount <= 0 || sourceList == null || destList == null)
            {
                return false;
            }
            destList.Clear();
            if (randCount >= sourceList.Count)
            {
                foreach (var val in sourceList)
                {
                    destList.Add(val);
                }
            }
            else
            {
                int beginIndex = Shared.Next(0, sourceList.Count - 1);
                for (int i = beginIndex; i < beginIndex + randCount; i++)
                {
                    destList.Add(sourceList[i % sourceList.Count]);
                }
            }
            return true;
        }

        /// <summary>
        /// 通过权重随机
        /// </summary>
        public static int RandomByWeight(IList<int> weights)
        {
            return RandomByWeight(weights, weights.Count);
        }

        public static int RandomByWeight(IList<int> weights, int Count)
        {
            return RandomByWeight(weights, Count, 0);
        }

        public static int RandomByWeightM(IList<int> weights, int weightRandomMinVal)
        {
            return RandomByWeight(weights, weights.Count, weightRandomMinVal);
        }

        public static T RandomByWeight<T>(IList<T> list, Func<T, int> wight)
        {
            using ListComponent<int> listComponent = ListComponent<int>.Create();
            list.Foreach(item => listComponent.Add(wight(item)));
            int index = RandomByWeight(listComponent);
            return list[index];
        }

        public static int RandomByWeight(IList<int> weights, int Count, int weightRandomMinVal)
        {
            if (weights.Count == 0)
            {
                return -1;
            }

            if (weights.Count == 1)
            {
                return 0;
            }

            int sum = 0;
            int sum_temp = 0;
            for (int i = 0; i < Count; i++)
            {
                if (i >= weights.Count)
                {
                    break;
                }
                sum += weights[i];
            }

            int number_rand = Shared.Next(1, Math.Max(sum, weightRandomMinVal) + 1);
            for (int i = 0; i < weights.Count; i++)
            {
                sum_temp += weights[i];
                if (number_rand <= sum_temp)
                {
                    return i;
                }
            }

            return -1;
        }

        public static int RandomByWeight(IList<long> weights)
        {
            if (weights.Count == 0)
            {
                return -1;
            }

            if (weights.Count == 1)
            {
                return 0;
            }

            long sum = 0;
            long sum_temp = 0;
            for (int i = 0; i < weights.Count; i++)
            {
                sum += weights[i];
            }

            long number_rand = RandInt64(1, sum + 1);
            for (int i = 0; i < weights.Count; i++)
            {
                sum_temp += weights[i];
                if (number_rand <= sum_temp)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}