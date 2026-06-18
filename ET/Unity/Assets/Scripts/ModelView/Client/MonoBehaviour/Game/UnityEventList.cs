using UnityEngine;
using UnityEngine.Events;

namespace ET
{
    [EnableClass]
    public sealed class UnityEventList : MonoBehaviour
    {
        public UnityEvent[] Events;

        public void Invoke(int index)
        {
            if (index >= 0 && index < this.Events.Length)
            {
                this.Events[index]?.Invoke();
            }
        }
    }
}