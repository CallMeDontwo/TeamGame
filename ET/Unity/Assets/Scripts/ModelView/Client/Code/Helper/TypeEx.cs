using Unity.Mathematics;
using UnityEngine;

namespace ET
{
    public static class TypeEx
    {

        public static Vector3 ToVector(this float3 float3)
        {
            return new Vector3(float3.x, float3.y, float3.z);
        }
    }
}
