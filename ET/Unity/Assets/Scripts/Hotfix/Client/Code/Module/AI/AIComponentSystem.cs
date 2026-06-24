using System;
using Unity.Mathematics;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(AIComponent))]
    [FriendOf(typeof(AIComponent))]
    public static partial class AIComponentSystem
    {
        private const int HANDLER_IDLE = 1;
        private const int HANDLER_PATROL = 2;
        private const int HANDLER_CHASE = 3;
        private const int HANDLER_ATTACK = 4;
        private const int HANDLER_SKILL = 5;

        /// <summary>横版攻击的 Y 轴正对面容差（单位：世界坐标）</summary>
        private const float VerticalAttackTolerance = 0.15f;

        private static TimerComponent GetTimer(this Entity entity)
        {
            return entity.Root().GetComponent<TimerComponent>();
        }

        [EntitySystem]
        private static void Awake(this AIComponent self, int aiConfigId)
        {
            self.AIConfigId = aiConfigId;
            self.Paused = true;  // 暂未激活，等视图就绪后再启动
            self.StartAICycle().Coroutine();
        }

        [EntitySystem]
        private static void Destroy(this AIComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = null;
        }

        // ═══════════════════════════════════════════════════
        //  AI 主循环（纯编排，行为委托给 Behavior/ 目录）
        // ═══════════════════════════════════════════════════

        private static async ETTask StartAICycle(this AIComponent self)
        {
            TimerComponent timer = self.GetTimer();
            var config = AIConfigCategory.Instance.Get(self.AIConfigId);
            var unit = self.GetParent<Unit>();

            if (config.HandlerIds.Length == 0)
            {
                Log.Error($"AIComponent AIConfigId={self.AIConfigId} HandlerIds 为空");
                return;
            }

            float speed = unit.GetComponent<NumericComponent>()?.GetAsInt(NumericType.MoveSpeed) ?? 3f;

            while (!self.IsDisposed)
            {
                if (self.Paused)
                {
                    await timer.WaitFrameAsync();
                    continue;
                }

                // 死亡单位不执行 AI（放过 None，让新单位可正常初始化）
                var state = unit.GetComponent<StateComponent>();
                if (state == null || state.State == UnitState.Death)
                {
                    // 死亡期间只等 Dispose 清走
                    var ai = unit.GetComponent<AIComponent>();
                    if (ai != null) ai.CurrentBehaviorName = "Dead";
                    await timer.WaitFrameAsync();
                    continue;
                }

                // 0. 施法中跳过决策（技能时间轴由 SkillComponent 自驱动）
                //if (unit.GetComponent<SkillComponent>()?.IsCasting() == true)
                //{
                //    await timer.WaitFrameAsync();
                //    continue;
                //}

                // 1. 扫描感知
                unit.GetComponent<PerceptionComponent>()?.ScanSurrounding();

                // 2. 决策前先清理旧锁定（避免 DecideNextHandler 内部清零而导致外层的 oldLockedId 失效、造成锁泄漏）
                var unitManager = unit.Scene().GetComponent<UnitManager>();
                long oldLockedId = self.LockedTargetId;
                if (unitManager != null && oldLockedId != 0)
                    unitManager.LockedTargets.Remove(oldLockedId);

                // 3. 决策
                var (handlerType, target) = DecideNextHandler(self, config);

                // 决策结果写回锁定目标，同步注册新锁定
                self.LockedTargetId = target?.Id ?? 0;
                if (unitManager != null && target != null)
                    unitManager.LockedTargets[target.Id] = unit.Id;

                // 3. 委托执行
                switch (handlerType)
                {
                    case HANDLER_SKILL:
                        self.CurrentBehaviorName = "Skill";
                        if (target != null && !target.IsDisposed)
                           await AIBehavior_Skill.TryExecute(unit, target);
                        break;

                    case HANDLER_ATTACK:
                        self.CurrentBehaviorName = "Attack";
                        if (target != null && !target.IsDisposed)
                           await AIBehavior_Attack.Execute(unit, target, config);
                        break;

                    case HANDLER_CHASE:
                        self.CurrentBehaviorName = "Chase";
                        if (target != null && !target.IsDisposed)
                            AIBehavior_Chase.Execute(unit, target, GetActualAttackRange(unit), speed);
                        break;

                    case HANDLER_PATROL:
                        self.CurrentBehaviorName = "Patrol";
                        AIBehavior_Patrol.Execute(unit, config, speed);
                        break;

                    case HANDLER_IDLE:
                        self.CurrentBehaviorName = "Idle";
                        AIBehavior_Idle.Execute(unit);
                        break;
                }

                await timer.WaitFrameAsync();
            }
        }

        // ═══════════════════════════════════════════════════
        //  决策逻辑
        // ═══════════════════════════════════════════════════

        private static (int handlerType, Unit target) DecideNextHandler(AIComponent self, AIConfig config)
        {
            var unit = self.GetParent<Unit>();
            var perception = unit.GetComponent<PerceptionComponent>();
            var identity = unit.GetComponent<IdentityComponent>();
            var selfAtk = unit.GetComponent<AttackComponent>();
            float actualAttackRange = GetActualAttackRange(unit);

            // 目标锁定：当前目标存活则保持锁定，不因距离/其他敌人切换
            if (self.LockedTargetId != 0)
            {
                var lockedTarget = FindUnitById(unit.Scene(), self.LockedTargetId);
                if (lockedTarget != null && !lockedTarget.IsDisposed)
                {
                    var lockedState = lockedTarget.GetComponent<StateComponent>();
                    if (lockedState != null && lockedState.IsAlive())
                    {
                        float lockedDist = math.distance(unit.Position, lockedTarget.Position);
                        var unitManager = unit.Scene().GetComponent<UnitManager>();

                        // 英雄专属：如果感知层选了一个未锁定的新目标，优先切换（不检查攻击条件）
                        bool heroSwitch = false;
                        if (identity.UnitType == UnitType.Hero
                            && perception != null && perception.PrimaryTargetId != 0 && perception.PrimaryTargetId != self.LockedTargetId)
                        {
                            var betterTarget = FindUnitById(unit.Scene(), perception.PrimaryTargetId);
                            if (betterTarget != null && !betterTarget.IsDisposed)
                            {
                                var betterState = betterTarget.GetComponent<StateComponent>();
                                if (betterState != null && betterState.IsAlive()
                                    && (unitManager == null || !unitManager.IsLockedByFriend(betterTarget.Id, identity.UnitType, unit.Id)))
                                {
                                    heroSwitch = true;
                                }
                            }
                        }

                        if (!heroSwitch)
                        {
                            if (HasHandler(config.HandlerIds, HANDLER_SKILL)
                                && IsInVerticalAttackRange(unit.Position, lockedTarget.Position)
                                && HasReadySkillForTarget(unit, lockedTarget))
                                return (HANDLER_SKILL, lockedTarget);

                            if (IsInVerticalAttackRange(unit.Position, lockedTarget.Position)
                                && lockedDist <= actualAttackRange + 0.01f
                                && HasHandler(config.HandlerIds, HANDLER_ATTACK))
                            {
                                var basicSkill = SkillDataStore.Get(selfAtk?.BasicAttackSkillId ?? 0);
                                if (basicSkill != null)
                                {
                                    int hDiff = math.abs(identity.Height - lockedTarget.GetComponent<IdentityComponent>()?.Height ?? 0);
                                    if (hDiff <= basicSkill.ReachHeight)
                                        return (HANDLER_ATTACK, lockedTarget);
                                }
                                else
                                {
                                    return (HANDLER_ATTACK, lockedTarget);
                                }
                            }

                            if (HasHandler(config.HandlerIds, HANDLER_CHASE))
                                return (HANDLER_CHASE, lockedTarget);
                        }
                        // heroSwitch==true → 不返回锁定目标，fall through 到正常感知流程，由外部注册新锁定
                    }
                }
            }

            if (perception != null && perception.PrimaryTargetId != 0)
            {
                Unit target = FindUnitById(unit.Scene(), perception.PrimaryTargetId);
                if (target != null && !target.IsDisposed)
                {
                    if (selfAtk != null)
                    {
                        int heightDiff = math.abs(identity.Height - target.GetComponent<IdentityComponent>()?.Height ?? 0);
                        if (heightDiff > selfAtk.ReachHeight)
                        {
                            perception.PrimaryTargetId = 0;
                            target = null;
                        }
                    }
                }

                if (target != null && !target.IsDisposed)
                {
                    float dist = math.distance(unit.Position, target.Position);

                    // 优先级: 技能 > 普攻 > 追击
                    if (HasHandler(config.HandlerIds, HANDLER_SKILL) && IsInVerticalAttackRange(unit.Position, target.Position) && HasReadySkillForTarget(unit, target))
                        return (HANDLER_SKILL, target);

                    if (IsInVerticalAttackRange(unit.Position, target.Position)&&dist <= actualAttackRange + 0.01f && HasHandler(config.HandlerIds, HANDLER_ATTACK))
                    {
                        // Y 轴正对面校验：不在同一水平线则 fall through 到追击
                        //if (!IsInVerticalAttackRange(unit.Position, target.Position))
                        //    continue;

                        // 普攻高度校验：感知层用 maxReachHeight 放行，此处用普攻自身 ReachHeight 再验
                        var basicSkill = SkillDataStore.Get(unit.GetComponent<AttackComponent>()?.BasicAttackSkillId ?? 0);
                        if (basicSkill != null)
                        {
                            int hDiff = math.abs(identity.Height - target.GetComponent<IdentityComponent>()?.Height ?? 0);
                            if (hDiff <= basicSkill.ReachHeight)
                                return (HANDLER_ATTACK, target);
                        }
                        else
                        {
                            return (HANDLER_ATTACK, target);
                        }
                        // 普攻打不到 → fall through 到 CHASE/PATROL
                    }

                    if (HasHandler(config.HandlerIds, HANDLER_CHASE))
                        return (HANDLER_CHASE, target);
                }
                else
                {
                    perception.PrimaryTargetId = 0;
                }
            }

            bool canPatrol = identity != null && identity.UnitType == UnitType.Monster;
            if (canPatrol && HasHandler(config.HandlerIds, HANDLER_PATROL))
                return (HANDLER_PATROL, null);

            if (HasHandler(config.HandlerIds, HANDLER_IDLE))
                return (HANDLER_IDLE, null);

            return (-1, null);
        }

        private static Unit FindUnitById(Scene scene, long unitId)
        {
            var unitManager = scene.GetComponent<UnitManager>();
            if (unitManager == null) return null;
            if (unitManager.Children.TryGetValue(unitId, out var entity) && entity is Unit unit)
                return unit;
            return null;
        }

        private static bool HasHandler(int[] handlerIds, int type)
        {
            return Array.IndexOf(handlerIds, type) >= 0;
        }

        private static bool HasReadySkill(Unit unit)
        {
            var skillComp = unit.GetComponent<SkillComponent>();
            return skillComp != null && skillComp.GetFirstReadySkill() != 0;
        }

        /// <summary>检查是否有能命中指定目标的就绪技能（含高度、距离过滤）</summary>
        private static bool HasReadySkillForTarget(Unit unit, Unit target)
        {
            var skillComp = unit.GetComponent<SkillComponent>();
            if (skillComp == null || skillComp.SkillIds == null) return false;

            int selfH = unit.GetComponent<IdentityComponent>()?.Height ?? 0;
            int targetH = target?.GetComponent<IdentityComponent>()?.Height ?? 0;

            foreach (int skillId in skillComp.SkillIds)
            {
                if (!skillComp.IsReady(skillId)) continue;
                var data = SkillDataStore.Get(skillId);
                if (data == null) continue;
                int hDiff = math.abs(selfH - targetH);
                if (hDiff > data.ReachHeight) continue;

                // 施法距离校验（与 SkillComponent.Cast 保持一致）
                if (data.CastRange > 0 && target != null && !target.IsDisposed)
                {
                    float castRange = data.CastRange / 100f;
                    float dist = math.distance(unit.Position, target.Position);
                    if (dist > castRange + 0.01f) continue;
                }

                return true;
            }
            return false;
        }

        /// <summary>获取实际攻击范围：读普攻技能的 CastRange，未配置默认 1.5 单位</summary>
        private static float GetActualAttackRange(Unit unit)
        {
            var attackComp = unit.GetComponent<AttackComponent>();
            if (attackComp != null && attackComp.BasicAttackSkillId > 0)
            {
                var skillData = SkillDataStore.Get(attackComp.BasicAttackSkillId);
                if (skillData != null && skillData.CastRange > 0)
                    return skillData.CastRange / 100f;
            }
            return 1.5f; // 默认普攻距离
        }

        /// <summary>高度可达性校验（保底重检，与感知层一致）</summary>
        private static bool IsHeightReachable(Unit self, Unit target, IdentityComponent selfId, AttackComponent selfAtk)
        {
            if (selfAtk == null) return true;
            var targetId = target.GetComponent<IdentityComponent>();
            if (targetId == null) return false;
            int hDiff = math.abs(selfId.Height - targetId.Height);
            return hDiff <= selfAtk.ReachHeight;
        }

        /// <summary>判断单位与目标在横版 Y 轴上是否处于可攻击范围（正对面容差）</summary>
        private static bool IsInVerticalAttackRange(float3 selfPos, float3 targetPos)
        {
            return math.abs(selfPos.y - targetPos.y) <= VerticalAttackTolerance;
        }

        // ═══════════════════════════════════════════════════
        //  外部接口
        // ═══════════════════════════════════════════════════

        public static void StopAI(this AIComponent self)
        {
            self.CancellationToken?.Cancel();
        }

        public static void PauseAI(this AIComponent self)
        {
            self.Paused = true;
            self.CancellationToken?.Cancel();
        }

        public static void ResumeAI(this AIComponent self)
        {
            self.Paused = false;
        }
    }
}
