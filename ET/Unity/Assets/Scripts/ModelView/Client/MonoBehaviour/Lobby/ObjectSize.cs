using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class ObjectSize : MonoBehaviour
    {
        public Vector3 Size;
        public Vector3 Pivot;

        public float Width => this.Size.x;
        public float Height => this.Size.y;

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(this.transform.position + this.Mul(this.Size, this.Pivot), this.Size);
        }

        private Vector3 Mul(in Vector3 v1, in Vector3 v2)
        {
            return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
        }
#endif
    }
}