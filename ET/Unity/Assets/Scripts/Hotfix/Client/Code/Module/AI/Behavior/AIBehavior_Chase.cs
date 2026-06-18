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
            float3 dir = math.normalizesafe(targetPos - selfPos);
            float3 stopPos = targetPos - dir * attackRange;

            var path = new List<float3> { selfPos, stopPos };
            mc.MoveToAsync(path, speed).Coroutine();

            mc.FaceDirection(stopPos - selfPos);
        }
    }
}
