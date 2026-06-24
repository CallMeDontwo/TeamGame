using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.TeamGame
{
    /// <summary>
    /// 目标查找器 — 独立寻敌能力，支持单目标与 AOE 范围查找
    /// 配置来源：TargetFinderConfig（Excel 导表）
    /// </summary>
    public static class TargetFinder
    {
        /// <summary>查找单个目标（AOE 时返回命中列表中的第一个）</summary>
        public static Unit Find(Unit caster, TargetFinderConfig config, int reachHeight = 0)
        {
            if (caster == null || caster.IsDisposed || config == null) return null;

            if (config.FinderType == 1)
            {
                return FindCurrentTarget(caster);
            }

            // AOE 范围查找：填充临时列表并返回首个
            using var list = new DisposeList<Unit>();
            Find(caster, config, list.List, reachHeight);
            return list.List.Count > 0 ? list.List[0] : null;
        }

        /// <summary>查找目标并写入 output（AOE 时填充多个，单目标时填充 0 或 1 个）</summary>
        public static void Find(Unit caster, TargetFinderConfig config, List<Unit> output, int reachHeight = 0)
        {
            if (caster == null || caster.IsDisposed || config == null) return;

            output.Clear();

            if (config.FinderType == 1)
            {
                var target = FindCurrentTarget(caster);
                if (target != null) output.Add(target);
                return;
            }

            // AOE 类型：2=圆形，3=矩形，4=扇形
            if (config.FinderType is < 2 or > 4) return;

            Unit anchor = config.AnchorType == 2 ? FindCurrentTarget(caster) : caster;
            if (anchor == null || anchor.IsDisposed) return;

            float2 anchorPos = new float2(anchor.Position.x, anchor.Position.y);

            // 形状方向与偏移都跟随施法者朝向（而非锚点自身朝向）
            float2 forward = GetForward(caster);
            int facingSign = GetFacingSign(caster);

            // 中心点偏移：OffsetX 沿屏幕水平（右正），人物朝左时取反；OffsetY 垂直上正
            float2 center = anchorPos
                            + new float2(facingSign * config.OffsetX / 100f, config.OffsetY / 100f);

            float param1 = config.Param1 / 100f;
            float param2 = config.Param2 / 100f;
            if (param1 <= 0.001f) return;
            if (config.FinderType == 3 && param2 <= 0.001f) return;

            var scene = caster.Scene();
            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return;

            var selfIdentity = caster.GetComponent<IdentityComponent>();
            UnitType selfType = selfIdentity?.UnitType ?? UnitType.None;
            int casterHeight = selfIdentity?.Height ?? 0;

            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit other) continue;
                if (other == null || other.IsDisposed || other.Id == caster.Id) continue;

                var otherIdentity = other.GetComponent<IdentityComponent>();
                if (otherIdentity == null) continue;
                if (!IsEnemy(selfType, otherIdentity.UnitType)) continue;

                int otherHeight = otherIdentity.Height;
                if (math.abs(casterHeight - otherHeight) > reachHeight) continue;

                float2 pos = new float2(other.Position.x, other.Position.y);
                if (!IsInShape(pos, center, forward, config.FinderType, param1, param2)) continue;

                output.Add(other);
            }

            // 发布 AOE 形状用于调试可视化
            EventSystem.Instance.Publish(scene, new AfterAoeFindTarget
            {
                ShapeType = config.FinderType,
                Center = new float3(center.x, center.y, 0),
                Forward = forward,
                Param1 = param1,
                Param2 = param2,
            });
        }

        /// <summary>查找当前技能释放目标</summary>
        private static Unit FindCurrentTarget(Unit caster)
        {
            var castComp = caster.GetComponent<SkillCastComponent>();
            if (castComp == null) return null;
            Unit target = castComp.Target;
            if (target == null || target.IsDisposed) return null;
            return target;
        }

        /// <summary>判断点是否在指定形状内</summary>
        private static bool IsInShape(float2 point, float2 center, float2 forward, int shapeType, float param1, float param2)
        {
            switch (shapeType)
            {
                case 2: return IsInCircle(point, center, param1);
                case 3: return IsInRectangle(point, center, forward, param1, param2);
                case 4: return IsInSector(point, center, forward, param1, param2);
                default: return false;
            }
        }

        /// <summary>圆形：点到中心距离 <= 半径</summary>
        private static bool IsInCircle(float2 point, float2 center, float radius)
        {
            return math.distance(point, center) <= radius;
        }

        /// <summary>矩形：以 center 为中心，forward 为长度方向（跟随施法者朝向），宽度方向固定为屏幕上方（Y+）</summary>
        private static bool IsInRectangle(float2 point, float2 center, float2 forward, float length, float width)
        {
            float2 diff = point - center;
            float2 right = new float2(0, 1); // 2D横版：宽度始终沿屏幕垂直方向
            float projX = math.dot(diff, forward);
            float projY = math.dot(diff, right);
            return math.abs(projX) <= length * 0.5f && math.abs(projY) <= width * 0.5f;
        }

        /// <summary>扇形：以 center 为中心，forward 为方向，param1=半径，param2=角度（度）</summary>
        private static bool IsInSector(float2 point, float2 center, float2 forward, float radius, float angleDeg)
        {
            float2 diff = point - center;
            float distSq = math.lengthsq(diff);
            if (distSq > radius * radius) return false;
            if (distSq < 0.0001f) return true;

            float2 dir = math.normalizesafe(diff);
            float dot = math.clamp(math.dot(dir, forward), -1f, 1f);
            float halfAngle = math.radians(angleDeg) * 0.5f;
            return math.acos(dot) <= halfAngle;
        }

        /// <summary>获取单位朝向（2D 横版取 forward 的 x/y）</summary>
        private static float2 GetForward(Unit unit)
        {
            float3 f = math.forward(unit.Rotation);
            float2 dir = new float2(f.x, f.y);
            if (math.lengthsq(dir) < 0.0001f)
                dir = new float2(1, 0);
            return math.normalizesafe(dir);
        }

        /// <summary>获取人物朝向符号：1=朝右（Forward.x >= 0），-1=朝左</summary>
        private static int GetFacingSign(Unit unit)
        {
            return unit.Forward.x < 0 ? -1 : 1;
        }

        /// <summary>判断两个单位类型是否敌对</summary>
        private static bool IsEnemy(UnitType selfType, UnitType otherType)
        {
            return (selfType == UnitType.Monster && otherType == UnitType.Hero)
                || (selfType == UnitType.Hero && otherType == UnitType.Monster);
        }

        /// <summary>临时 List 包装，用于单目标查询时自动回收</summary>
        private sealed class DisposeList<T> : System.IDisposable
        {
            public List<T> List { get; } = new();
            public void Dispose() => List.Clear();
        }
    }
}
