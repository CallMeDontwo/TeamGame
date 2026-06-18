using System.Collections.Generic;
using System.Threading.Tasks;

namespace ET.TeamGame
{
    [EntitySystemOf(typeof(SkillComponent))]
    [FriendOf(typeof(SkillComponent))]
    [FriendOf(typeof(SkillCastComponent))]
    public static partial class SkillComponentSystem
    {
        [EntitySystem]
        private static void Awake(this SkillComponent self)
        {
            self.SkillIds = System.Array.Empty<int>();
            self.CooldownEndTime = new Dictionary<int, long>();
        }

        [EntitySystem]
        private static void Destroy(this SkillComponent self)
        {
            self.SkillIds = null;
            self.CooldownEndTime?.Clear();
            self.CooldownEndTime = null;
        }

        private static TimerComponent GetTimer(this SkillComponent self)
        {
            return self.GetParent<Unit>().Root().GetComponent<TimerComponent>();
        }

        // ═══════════════════════════════════════════════════
        //  技能施放
        // ═══════════════════════════════════════════════════

        public static bool IsCasting(this SkillComponent self)
        {
            var castComp = self.GetParent<Unit>().GetComponent<SkillCastComponent>();
            return castComp != null && castComp.SkillConfigId != 0;
        }

        public static async ETTask<bool> Cast(this SkillComponent self, int skillId, Unit target = null)
        {
            if (!self.IsReady(skillId) || self.IsCasting()) return false;

            var config = SkillConfigCategory.Instance.Get(skillId);
            var unit = self.GetParent<Unit>();

            var castComp = unit.GetComponent<SkillCastComponent>() ?? unit.AddComponent<SkillCastComponent>();
            castComp.SkillConfigId = skillId;
            castComp.Target = target;
            castComp.SkillStartTime = TimeInfo.Instance.ClientFrameTime();
            castComp.NextEventIndex = 0;

            // 缓存配置和事件数组，避免 TickCasting 每帧重复查表
            castComp.CachedConfig = config;
            castComp.CachedEvents = new SkillEventConfig[config.SkillEventIds.Length];
            for (int i = 0; i < config.SkillEventIds.Length; i++)
            {
                castComp.CachedEvents[i] = SkillEventConfigCategory.Instance.Get(config.SkillEventIds[i]);
            }

            await self.StartSkillTimeline();
            return true;
        }

        private static async ETTask StartSkillTimeline(this SkillComponent self)
        {
            TimerComponent timer = self.GetTimer();
            var unit = self.GetParent<Unit>();

            while (!self.IsDisposed)
            {
                var castComp = unit.GetComponent<SkillCastComponent>();
                if (castComp == null || castComp.SkillConfigId == 0) return;

                self.TickCasting(castComp);

                if (castComp.SkillConfigId == 0) return; // 技能已结束

                await timer.WaitFrameAsync();
            }
        }

        private static void TickCasting(this SkillComponent self, SkillCastComponent castComp)
        {
            var unit = self.GetParent<Unit>();
            var config = castComp.CachedConfig;
            var events = castComp.CachedEvents;
            long elapsed = TimeInfo.Instance.ClientFrameTime() - castComp.SkillStartTime;

            // 遍历未执行的事件（使用缓存的事件数组）
            while (castComp.NextEventIndex < events.Length
                && config.SkillEventTimestamps != null
                && castComp.NextEventIndex < config.SkillEventTimestamps.Length)
            {
                if (elapsed < config.SkillEventTimestamps[castComp.NextEventIndex]) break;

                var eventConfig = events[castComp.NextEventIndex];
                castComp.NextEventIndex++;

                ExecuteEvent(unit, castComp.Target, eventConfig);
            }

            // 全部事件执行完毕 → 结束技能，进入CD
            if (castComp.NextEventIndex >= events.Length && elapsed >= config.Duration)
            {
                self.StartCooldown(castComp.SkillConfigId, config.CD);
                castComp.SkillConfigId = 0;
                castComp.Target = default;
                castComp.CachedConfig = null;
                castComp.CachedEvents = null;
            }
        }

        // ═══════════════════════════════════════════════════
        //  事件分发
        // ═══════════════════════════════════════════════════

        private static void ExecuteEvent(Unit caster, Unit target, SkillEventConfig config)
        {
            switch ((SkillEventType)config.EventType)
            {
                case SkillEventType.PlayAnimation:
                    SkillEvent_PlayAnimation.Execute(caster, config);
                    break;
                case SkillEventType.FindTarget:
                    SkillEvent_FindTarget.Execute(caster, config);
                    break;
                case SkillEventType.SpawnVFX:
                    SkillEvent_SpawnVFX.Execute(caster, config);
                    break;
                case SkillEventType.ApplyValue:
                    SkillEvent_ApplyValue.Execute(caster, target, config);
                    break;
                case SkillEventType.AddBuff:
                    SkillEvent_AddBuff.Execute(caster, target, config);
                    break;
                case SkillEventType.SpawnBullet:
                    SkillEvent_SpawnBullet.Execute(caster, config);
                    break;
                default:
                    Log.Error($"[SkillEvent] 未知事件类型 EventType={config.EventType}, EventId={config.Id}, Caster={caster?.Id}");
                    break;
            }
        }

        // ═══════════════════════════════════════════════════
        //  工具方法
        // ═══════════════════════════════════════════════════

        public static bool IsReady(this SkillComponent self, int skillId)
        {
            long now = TimeInfo.Instance.ClientFrameTime();
            return !self.CooldownEndTime.TryGetValue(skillId, out var endTime) || now >= endTime;
        }

        public static void StartCooldown(this SkillComponent self, int skillId, float cdSeconds)
        {
            long now = TimeInfo.Instance.ClientFrameTime();
            self.CooldownEndTime[skillId] = now + (long)(cdSeconds * 1000);
        }

        public static void LearnSkill(this SkillComponent self, int skillId)
        {
            var list = new List<int>(self.SkillIds);
            if (list.Contains(skillId)) return;
            list.Add(skillId);
            self.SkillIds = list.ToArray();
        }

        public static int GetFirstReadySkill(this SkillComponent self)
        {
            foreach (int id in self.SkillIds)
            {
                if (self.IsReady(id)) return id;
            }
            return 0;
        }

        public static bool HasSkill(this SkillComponent self)
        {
            return self.SkillIds.Length != 0;
        }
    }
}