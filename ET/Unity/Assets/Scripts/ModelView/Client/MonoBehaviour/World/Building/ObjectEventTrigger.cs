using UnityEngine;

namespace ET
{
    [EnableClass]
    [RequireComponent(typeof(ObjectEvent))]
    [AddComponentMenu("ObjectEvent/ObjectEventTrigger")]
    internal class ObjectEventTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player") && this.TryGetComponent(out ObjectEvent objectEvent))
            {
                objectEvent.Invoke();
            }
        }
    }
}