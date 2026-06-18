using System;

namespace ET
{
    public static class ActionExtension
    {
        public static void SafeInvoke(this Action action)
        {
            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void SafeInvoke<T>(this Action<T> action, in T obj)
        {
            try
            {
                action?.Invoke(obj);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void SafeInvoke<T0, T1>(this Action<T0, T1> action, in T0 arg1, in T1 arg2)
        {
            try
            {
                action?.Invoke(arg1, arg2);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void SafeInvoke<T0, T1, T2>(this Action<T0, T1, T2> action, in T0 arg1, in T1 arg2, in T2 arg3)
        {
            try
            {
                action?.Invoke(arg1, arg2, arg3);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}