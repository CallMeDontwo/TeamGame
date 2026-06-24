using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.TeamGame
{
    public static class AIBehavior_Chase
    {

        public static void Execute(Unit unit, Unit target, float attackRange, float speed)
        {
            if (target == null || target.IsDisposed) return;

            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Move);

            var mc = unit.GetComponent<MoveComponent>();
            if (mc == null) return;

            float3 selfPos = unit.Position;
            float3 targetPos = target.Position;

            // 横版：追击终点固定为"目标正对面、同一 Y 层"
            // 这样单位在上下方错位时也会先对齐 Y，再进入攻击范围
            float xDir = math.sign(targetPos.x - selfPos.x); // 1=目标在右侧，-1=目标在左侧
            float3 stopPos = new float3(targetPos.x - xDir * attackRange, targetPos.y, targetPos.z);

            var path = new List<float3> { selfPos, stopPos };
            mc.MoveToAsync(path, speed).Coroutine();

            mc.FaceDirection(targetPos - selfPos);
        }
    }
}
