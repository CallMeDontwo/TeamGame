using System;

namespace ET
{
    public struct ValueWatcher<T> where T : IEquatable<T>
    {
        public Action<T, T> Action;

        private T value;

        public T Value
        {
            get => this.value;
            set
            {
                if (this.value.Equals(value))
                    return;
                T oldValue = this.value;
                this.value = value;
                this.Action?.Invoke(oldValue, value);
            }
        }

        public override readonly string ToString() => this.value.ToString();

        public static implicit operator T(ValueWatcher<T> watcher) => watcher.Value;
    }
}