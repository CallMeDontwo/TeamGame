using System;
using System.Collections.Generic;

namespace FairyGUI
{
    public static class GomponentExtension
    {
        public static IEnumerable<GObject> GetEnumerator(this GComponent self)
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                yield return self.GetChildAt(i);
            }
        }

        public static IEnumerable<T> GetEnumerator<T>(this GComponent self) where T : GComponent
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                if (self.GetChildAt(i) is T obj)
                {
                    yield return obj;
                }
            }
        }

        public static void Foreach(this GComponent self, Action<int, GObject> action)
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                action(i, self.GetChildAt(i));
            }
        }

        public static void Foreach<T>(this GComponent self, Action<int, T> action) where T : GComponent
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                if (self.GetChildAt(i) is T obj)
                {
                    action(i, obj);
                }
            }
        }

        public static void Foreach(this GComponent self, Action<GObject> action)
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                action(self.GetChildAt(i));
            }
        }

        public static void Foreach<T>(this GComponent self, Action<T> action) where T : GComponent
        {
            for (int i = 0; i < self.numChildren; i++)
            {
                if (self.GetChildAt(i) is T obj)
                {
                    action(obj);
                }
            }
        }
    }
}