using UnityEngine;

namespace ET
{
    [EnableClass]
    public class CellGrid : MonoBehaviour
    {
        public Vector3 CellSize = Vector3.zero;
        public Vector3 CellGap = Vector3.zero;
        public Vector3 CellOffset = Vector3.zero;

        public Vector3 CellToLocal(Vector3Int cell)
        {
            return this.Mul(cell, this.CellSize + this.CellGap) + this.CellOffset;
        }

        public Vector3 CellToWorld(Vector3Int cell)
        {
            return this.transform.localToWorldMatrix * this.CellToLocal(cell);
        }

        private Vector3 Mul(Vector3Int v3Int, Vector3 v3)
        {
            return new Vector3(v3Int.x * v3.x, v3Int.y * v3.y, v3Int.z * v3.z);
        }
    }
}