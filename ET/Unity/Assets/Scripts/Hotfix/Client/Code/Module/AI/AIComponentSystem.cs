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
            float attackRange = config.AttackRange / 100f;

            while (!self.IsDisposed)
            {
                if (self.Paused)
                {
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

                // 2. 决策
                var (handlerType, target) = DecideNextHandler(self, config);

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
                        AIBehavior_Attack.Execute(unit, target, config);
                        break;

                    case HANDLER_CHASE:
                        self.CurrentBehaviorName = "Chase";
                        if (target != null && !target.IsDisposed)
                            AIBehavior_Chase.Execute(unit, target, attackRange, speed);
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

            if (perception != null && perception.PrimaryTargetId != 0)
            {
                Unit target = FindUnitById(unit.Scene(), perception.PrimaryTargetId);
                if (target != null && !target.IsDisposed)
                {
                    float dist = math.distance(unit.Position, target.Position);
                    float attackRange = config.AttackRange / 100f;

                    // 优先级: 技能 > 普攻 > 追击
                    if (HasHandler(config.HandlerIds, HANDLER_SKILL) && HasReadySkill(unit))
                        return (HANDLER_SKILL, target);

                    if (dist <= attackRange && HasHandler(config.HandlerIds, HANDLER_ATTACK))
                        return (HANDLER_ATTACK, target);

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
