using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(UnitManager))]
    [FriendOf(typeof(UnitManager))]
    public static partial class UnitManagerSystem
    {
        [EntitySystem]
        private static void Awake(this UnitManager self)
        {
        }

        [EntitySystem]
        private static void Destroy(this UnitManager self)
        {
        }

        // ═══════════════════════════════════════════════════
        //  每帧单位分离：防止单位（尤其是近战围攻时）完全重叠
        // ═══════════════════════════════════════════════════

        [EntitySystem]
        private static void Update(this UnitManager self)
        {
            // 收集所有存活单位
            var units = new List<Unit>(self.Children.Count);
            foreach (var kv in self.Children)
            {
                if (kv.Value is not Unit unit) continue;
                if (unit.IsDisposed) continue;

                var state = unit.GetComponent<StateComponent>();
                if (state == null || state.State == UnitState.Death) continue;

                units.Add(unit);
            }

            int n = units.Count;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    Unit a = units[i];
                    Unit b = units[j];

                    var identityA = a.GetComponent<IdentityComponent>();
                    var identityB = b.GetComponent<IdentityComponent>();
                    if (identityA == null || identityB == null) continue;

                    float rA = GetUnitCollisionRadius(a, identityA.UnitType);
                    float rB = GetUnitCollisionRadius(b, identityB.UnitType);

                    float3 posA = a.Position;
                    float3 posB = b.Position;
                    float3 diff = posA - posB;
                    float dist = math.length(diff);
                    float minDist = rA + rB;

                    // 允许保留少量重叠（轻微重叠可以接受），只有超出才推开
                    const float allowedOverlap = 0.4f;
                    if (dist < minDist - allowedOverlap && dist > 1e-6f)
                    {
                        float overlap = (minDist - allowedOverlap) - dist;
                        float3 offset = (diff / dist) * overlap * 0.5f;

                        // 限制每帧最大偏移，避免被挤飞或抖动过大
                        const float maxOffset = 0.01f;
                        if (math.length(offset) > maxOffset)
                            offset = math.normalize(offset) * maxOffset;

                        a.SetPositon(posA + offset, true);
                        b.SetPositon(posB - offset, true);
                    }
                }
            }

            // 清理失效的锁定（锁定者或目标已死亡/Dispose）
            var deadKeys = new List<long>();
            foreach (var kv in self.LockedTargets)
            {
                long targetId = kv.Key;
                long lockerId = kv.Value;
                self.Children.TryGetValue(targetId, out var targetEntity);
                self.Children.TryGetValue(lockerId, out var lockerEntity);
                if (targetEntity is not Unit targetUnit || targetUnit.IsDisposed
                    || lockerEntity is not Unit lockerUnit || lockerUnit.IsDisposed
                    || targetUnit.GetComponent<StateComponent>()?.State == UnitState.Death
                    || lockerUnit.GetComponent<StateComponent>()?.State == UnitState.Death)
                {
                    deadKeys.Add(targetId);
                }
            }
            foreach (long key in deadKeys)
                self.LockedTargets.Remove(key);
        }

        /// <summary>获取单位碰撞半径（int/100 → float）</summary>
        private static float GetUnitCollisionRadius(Unit unit, UnitType unitType)
        {
            int radius = unitType switch
            {
                UnitType.Hero    => HeroConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius,
                UnitType.Monster => MonsterConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius,
                _                => 40,
            };
            return radius / 100f;
        }

        // ═══════════════════════════════════════════════════
        //  锁定关系管理（感知层优先选未锁定目标）
        // ═══════════════════════════════════════════════════

        /// <summary>注册锁定：targetId 被 lockerId 锁定</summary>
        public static void RegisterLock(this UnitManager self, long targetId, long lockerId)
        {
            self.LockedTargets[targetId] = lockerId;
        }

        /// <summary>解除锁定者之前的锁定关系（lockerId 不再锁定任何目标）</summary>
        public static void UnregisterLock(this UnitManager self, long lockerId)
        {
            long oldKey = 0;
            foreach (var kv in self.LockedTargets)
            {
                if (kv.Value == lockerId)
                {
                    oldKey = kv.Key;
                    break;
                }
            }
            if (oldKey != 0)
                self.LockedTargets.Remove(oldKey);
        }

        /// <summary>目标是否被锁定（被任意单位锁定）</summary>
        public static bool IsLocked(this UnitManager self, long targetId)
        {
            return self.LockedTargets.ContainsKey(targetId);
        }

        /// <summary>目标是否被友方单位锁定（排除自己，同阵营且非己 → 已被友方抢走）</summary>
        public static bool IsLockedByFriend(this UnitManager self, long targetId, UnitType myType, long myUnitId)
        {
            if (!self.LockedTargets.TryGetValue(targetId, out long lockerId))
                return false;
            if (lockerId == myUnitId) return false; // 自己锁的，不算"已被抢走"
            self.Children.TryGetValue(lockerId, out var lockerEntity);
            if (lockerEntity is Unit lockerUnit)
            {
                var idComp = lockerUnit.GetComponent<IdentityComponent>();
                if (idComp != null && idComp.UnitType == myType)
                    return true;
            }
            return false;
        }

        /// <summary>查询谁锁定了这个目标（targetId 被谁锁定），未锁返回 0</summary>
        public static long GetLockerOf(this UnitManager self, long targetId)
        {
            self.LockedTargets.TryGetValue(targetId, out long lockerId);
            return lockerId;
        }

        /// <summary>
        /// 创建测试用英雄和怪物（只在调试阶段使用）
        /// </summary>
        public static async ETTask CreateTestUnits(this UnitManager self, Scene scene)
        {
            Scene s = scene ?? self.Scene();

            // 创建英雄（configId=1001，位置 x=-5）
            await UnitFactory.CreateHero(s, 1001, new float3(-5, 0, 0));

            // 创建怪物（configId=2001，位置 x=5）
            //await UnitFactory.CreateMonster(s, 99001, new float3(0, 0, 0));
            await UnitFactory.CreateMonster(s, 2001, new float3(5, 1.5f, 0));
        }
    }
}
