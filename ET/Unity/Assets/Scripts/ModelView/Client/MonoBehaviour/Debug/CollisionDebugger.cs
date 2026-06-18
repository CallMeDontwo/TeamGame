using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ET.TeamGame
{
    /// <summary>
    /// 碰撞调试可视化
    ///   按 F2 — 碰撞圆开关
    ///   按 F3 — 空间哈希网格开关
    /// 
    /// 颜色:
    ///   绿色 = 友方（英雄）
    ///   红色 = 敌方（怪物）
    ///   黄色 = 子弹
    /// </summary>
    [EnableClass]
    public class CollisionDebugger : MonoBehaviour
    {
        public Scene scene;
        private bool circleVisible = true;
        private bool gridVisible = true;

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
            if (Keyboard.current == null) return;
            if (Keyboard.current.f2Key.wasPressedThisFrame)
                circleVisible = !circleVisible;
            if (Keyboard.current.f3Key.wasPressedThisFrame)
                gridVisible = !gridVisible;
        }

        private void OnDrawGizmos()
        {
            if (scene == null || scene.IsDisposed) return;

            if (circleVisible)
                DrawCollisionCircles();

            if (gridVisible)
                DrawSpatialGrid();
        }

        private void DrawCollisionCircles()
        {
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
                    radius = BulletDataStore.Get(bulletComp.BulletConfigId).CollisionRadius / 100f;
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
                            radius = 0.4f;
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

        // ═══════════════════════════════════════════════════
        //  空间哈希网格可视化
        // ═══════════════════════════════════════════════════

        private void DrawSpatialGrid()
        {
            var bulletMgr = scene.GetComponent<BulletManagerComponent>();
            if (bulletMgr == null || bulletMgr.BulletGrid == null) return;

            float cellSize = bulletMgr.GridCellSize;
            var bulletGrid = bulletMgr.BulletGrid;
            var enemyGrid = bulletMgr.EnemyGrid;

            // 收集所有有内容的 cell（合并 bulletGrid + enemyGrid）
            var allKeys = new HashSet<int>();
            foreach (var kv in bulletGrid)
                if (kv.Value.Count > 0) allKeys.Add(kv.Key);
            foreach (var kv in enemyGrid)
                if (kv.Value.Count > 0) allKeys.Add(kv.Key);

            if (allKeys.Count == 0) return;

            foreach (int key in allKeys)
            {
                short cx = (short)(key & 0xFFFF);
                short cy = (short)(key >> 16);

                float xMin = cx * cellSize;
                float yMin = cy * cellSize;
                float xMax = xMin + cellSize;
                float yMax = yMin + cellSize;

                bool hasEnemy = enemyGrid.TryGetValue(key, out var el) && el.Count > 0;
                bool hasBullet = bulletGrid.TryGetValue(key, out var bl) && bl.Count > 0;

                Color cellColor;
                if (hasEnemy && hasBullet)
                    cellColor = new Color(1f, 0.5f, 0f, 0.35f);    // 橙色 = 敌+弹
                else if (hasEnemy)
                    cellColor = new Color(1f, 0f, 0f, 0.2f);        // 红色 = 敌人
                else if (hasBullet)
                    cellColor = new Color(1f, 1f, 0f, 0.25f);       // 黄色 = 子弹
                else
                    cellColor = new Color(0.5f, 0.5f, 0.5f, 0.1f); // 灰色 = 其他

                // 绘制填充矩形
                DrawFilledRect(xMin, yMin, xMax, yMax, cellColor);

                // 绘制边框
                Color border = hasBullet ? new Color(1f, 1f, 0f, 0.6f) : new Color(0.5f, 0.5f, 0.5f, 0.4f);
                DrawWireRect(xMin, yMin, xMax, yMax, border);

                // 标注数量
                if (hasEnemy || hasBullet)
                {
                    int eCount = hasEnemy ? el.Count : 0;
                    int bCount = hasBullet ? bl.Count : 0;
                    string label = $"{eCount}/{bCount}";
#if UNITY_EDITOR
                    UnityEditor.Handles.Label(
                        new Vector3(xMin + cellSize * 0.5f, yMin + cellSize * 0.5f, 0),
                        label,
                        new GUIStyle() { normal = { textColor = Color.white }, fontSize = 10, alignment = TextAnchor.MiddleCenter });
#endif
                }
            }
        }

        private static void DrawFilledRect(float xMin, float yMin, float xMax, float yMax, Color color)
        {
            Gizmos.color = color;
            // 用细线填充近似实心（需要的话可以用 GL，但 Gizmos 更简单）
            float step = 0.1f;
            for (float y = yMin; y <= yMax; y += step)
            {
                Gizmos.DrawLine(new Vector3(xMin, y, 0), new Vector3(xMax, y, 0));
            }
        }

        private static void DrawWireRect(float xMin, float yMin, float xMax, float yMax, Color color)
        {
            Gizmos.color = color;
            Gizmos.DrawLine(new Vector3(xMin, yMin, 0), new Vector3(xMax, yMin, 0));
            Gizmos.DrawLine(new Vector3(xMax, yMin, 0), new Vector3(xMax, yMax, 0));
            Gizmos.DrawLine(new Vector3(xMax, yMax, 0), new Vector3(xMin, yMax, 0));
            Gizmos.DrawLine(new Vector3(xMin, yMax, 0), new Vector3(xMin, yMin, 0));
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
