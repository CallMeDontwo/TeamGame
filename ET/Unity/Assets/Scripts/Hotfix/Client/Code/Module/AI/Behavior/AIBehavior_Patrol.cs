using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.TeamGame
{
    public static class AIBehavior_Patrol
    {

        public static void Execute(Unit unit, AIConfig config, float speed)
        {
            var state = unit.GetComponent<StateComponent>();
            state?.ChangeState(UnitState.Move);

            var mc = unit.GetComponent<MoveComponent>();
            if (mc == null) return;

            float step = math.lerp(0.3f, 0.8f, speed / 5f);
            float3 newPos = unit.Position;
            newPos.x -= step;
            newPos.y += (RandomGenerator.RandFloat01() - 0.5f) * step * 0.2f;
            if (newPos.x < -6f) newPos.x = -6f;

            var path = new List<float3> { unit.Position, newPos };
            mc.MoveToAsync(path, speed).Coroutine();

            mc.FaceDirection(newPos - unit.Position);
        }
    }
}
