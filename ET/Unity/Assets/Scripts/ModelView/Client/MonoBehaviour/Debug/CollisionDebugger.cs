using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ET.TeamGame
{
    /// <summary>
    /// 碰撞调试可视化 — 按 F2 开关，绘制所有 Unit 的碰撞圆
    /// 挂载在场景根节点上，由场景创建器添加
    /// 
    /// 颜色:
    ///   绿色 = 友方（英雄/己方）
    ///   红色 = 敌方（怪物）
    ///   黄色 = 子弹
    /// </summary>
    [EnableClass]
    public class CollisionDebugger : MonoBehaviour
    {
        public Scene scene;
        private bool visible = true;

        // 圆形分段数
        private const int SEGMENTS = 32;

        private struct CollisionInfo
        {
            public float3 center;
            public float radius;
            public Color color;
        }

        private readonly List<CollisionInfo> cache = new();

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.f2Key.wasPressedThisFrame)
                visible = !visible;
        }

        private void OnDrawGizmos()
        {
            if (!visible) return;
            if (scene == null || scene.IsDisposed) return;

            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return;

            cache.Clear();

            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit unit) continue;
                if (unit.IsDisposed) continue;

                // 获取 Unit 类型和碰撞半径
                float radius = 0f;
                Color color = Color.white;
                float2 center2 = new float2(unit.Position.x, unit.Position.y);

                var identity = unit.GetComponent<IdentityComponent>();
                var bulletComp = unit.GetComponent<BulletComponent>();

                if (bulletComp != null)
                {
                    radius = BulletConfigCategory.Instance.Get(bulletComp.BulletConfigId).CollisionRadius / 100f;
                    color = Color.yellow;
                }
                else if (identity != null)
                {
                    switch (identity.UnitType)
                    {
                        case UnitType.Hero:
                            radius = HeroConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius / 100f;
                            color = Color.green;
                            break;
                        case UnitType.Monster:
                            radius = MonsterConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius / 100f;
                            color = Color.red;
                            break;
                        default:
                            radius = 0.4f;  // 与碰撞系统默认值一致
                            color = Color.gray;
                            break;
                    }
                    // 敌人中心 = 锚点 + 半高（碰撞半径）
                    center2.y += radius;
                }
                else
                {
                    continue;
                }

                if (radius <= 0f) continue;

                cache.Add(new CollisionInfo
                {
                    center = new float3(center2.x, center2.y, 0),
                    radius = radius,
                    color = color,
                });
            }

            // 绘制所有碰撞圆
            foreach (var info in cache)
            {
                DrawWireCircle(info.center, info.radius, info.color);
            }
        }

        private static void DrawWireCircle(float3 center, float radius, Color color)
        {
            Gizmos.color = color;
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
    }
}
