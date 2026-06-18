using Unity.Mathematics;

namespace ET.TeamGame
{
    public static class MoveComponentExtensions
    {
        /// <summary>使单位面向指定方向（只影响 X 轴朝向，横版用）</summary>
        public static void FaceDirection(this MoveComponent self, float3 direction)
        {
            if (math.abs(direction.x) < 0.001f) return;

            var unit = self.GetParent<Unit>();
            float3 faceDir = direction.x < 0 ? new float3(-1, 0, 0) : new float3(1, 0, 0);
            unit.Rotation = quaternion.LookRotation(faceDir, math.up());
        }
    }
}
