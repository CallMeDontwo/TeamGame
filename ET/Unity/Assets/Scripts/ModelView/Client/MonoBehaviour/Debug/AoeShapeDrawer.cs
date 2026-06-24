using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// AOE 寻敌形状调试可视化
    /// 每次 FindTarget 触发 AOE 时，生成一个形状并在 1 秒后消失
    /// 颜色：圆形=黄、矩形=白、扇形=青
    /// </summary>
    [EnableClass]
    public class AoeShapeDrawer : MonoBehaviour
    {
        public static AoeShapeDrawer Instance { get; private set; }

        private const int SEGMENTS = 32;
        private const float DEFAULT_DURATION = 1f;

        private struct ShapeEntry
        {
            public int type;           // 2=圆形, 3=矩形, 4=扇形
            public float3 center;
            public float2 forward;
            public float param1;       // 半径/长度
            public float param2;       // 宽度/扇形角度（度）
            public float expireTime;
        }

        private readonly List<ShapeEntry> shapes = new();

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }

        /// <summary>添加一个 AOE 形状，1 秒后自动消失</summary>
        public void AddShape(int type, float3 center, float2 forward, float param1, float param2)
        {
            AddShape(type, center, forward, param1, param2, DEFAULT_DURATION);
        }

        /// <summary>添加一个 AOE 形状，duration 秒后自动消失</summary>
        public void AddShape(int type, float3 center, float2 forward, float param1, float param2, float duration)
        {
            shapes.Add(new ShapeEntry
            {
                type = type,
                center = center,
                forward = forward,
                param1 = param1,
                param2 = param2,
                expireTime = Time.time + duration,
            });
        }

        private void OnDrawGizmos()
        {
            float now = Time.time;

            // 清理过期形状
            shapes.RemoveAll(s => s.expireTime < now);

            Color[] colors = new[]
            {
                Color.clear,        // 0 - unused
                Color.clear,        // 1 - unused (current target)
                Color.yellow,       // 2 - 圆形 黄色
                Color.white,        // 3 - 矩形 白色
                Color.cyan,         // 4 - 扇形 青色
            };

            foreach (var s in shapes)
            {
                if (s.type < 2 || s.type >= colors.Length) continue;
                Gizmos.color = colors[s.type];

                switch (s.type)
                {
                    case 2: DrawWireCircle(s.center, s.param1); break;
                    case 3: DrawWireRectangle(s.center, s.forward, s.param1, s.param2); break;
                    case 4: DrawWireSector(s.center, s.forward, s.param1, s.param2); break;
                }
            }
        }

        // ═══════════════════════════════════════════════════
        //  绘制工具方法
        // ═══════════════════════════════════════════════════

        private static void DrawWireCircle(float3 center, float radius)
        {
            float angleStep = 2f * math.PI / SEGMENTS;
            float3 prev = center + new float3(radius, 0, 0);

            for (int i = 1; i <= SEGMENTS; i++)
            {
                float angle = i * angleStep;
                float3 next = center + new float3(math.cos(angle) * radius, math.sin(angle) * radius, 0);
                Gizmos.DrawLine(prev, next);
                prev = next;
            }
        }

        private static void DrawWireRectangle(float3 center, float2 forward, float length, float width)
        {
            float3 f = new float3(forward.x, forward.y, 0);
            float3 r = new float3(0, 1, 0); // 宽度方向固定屏幕 Y+

            float3 halfF = f * (length * 0.5f);
            float3 halfR = r * (width * 0.5f);

            float3 tl = center - halfF + halfR;
            float3 tr = center + halfF + halfR;
            float3 br = center + halfF - halfR;
            float3 bl = center - halfF - halfR;

            Gizmos.DrawLine(tl, tr);
            Gizmos.DrawLine(tr, br);
            Gizmos.DrawLine(br, bl);
            Gizmos.DrawLine(bl, tl);
        }

        private static void DrawWireSector(float3 center, float2 forward, float radius, float angleDeg)
        {
            float halfAngle = angleDeg * 0.5f * Mathf.Deg2Rad;
            float angleStep = angleDeg * Mathf.Deg2Rad / SEGMENTS;

            float3 f3 = new float3(forward.x, forward.y, 0);

            // 弧线
            float3 firstPoint = center + Rotate2D(f3, -halfAngle) * radius;
            float3 prev = firstPoint;
            for (int i = 1; i <= SEGMENTS; i++)
            {
                float angle = -halfAngle + i * angleStep;
                float3 next = center + Rotate2D(f3, angle) * radius;
                Gizmos.DrawLine(prev, next);
                prev = next;
            }

            // 两条边
            float3 leftEdge = center + Rotate2D(f3, -halfAngle) * radius;
            float3 rightEdge = center + Rotate2D(f3, halfAngle) * radius;
            Gizmos.DrawLine(center, leftEdge);
            Gizmos.DrawLine(center, rightEdge);
        }

        /// <summary>绕 Z 轴旋转 2D 向量</summary>
        private static float3 Rotate2D(float3 v, float angleRad)
        {
            float cos = math.cos(angleRad);
            float sin = math.sin(angleRad);
            return new float3(v.x * cos - v.y * sin, v.x * sin + v.y * cos, 0);
        }
    }
}
