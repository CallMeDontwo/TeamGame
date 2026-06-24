using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Mathematics;

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

            var skillData = SkillDataStore.Get(skillId);
            if (skillData == null) return false;

            var unit = self.GetParent<Unit>();

            // 距离检查（+0.01f 容差对抗 float 精度误差）
            if (skillData.CastRange > 0 && target != null && !target.IsDisposed)
            {
                float castRange = skillData.CastRange / 100f;
                float dist = math.distance(unit.Position, target.Position);
                if (dist > castRange + 0.01f) return false;
            }

            // 高度检查
            if (target != null && !target.IsDisposed)
            {
                int selfH = unit.GetComponent<IdentityComponent>()?.Height ?? 0;
                int targetH = target.GetComponent<IdentityComponent>()?.Height ?? 0;
                int hDiff = math.abs(selfH - targetH);
                if (hDiff > skillData.ReachHeight) return false;
            }

            var castComp = unit.GetComponent<SkillCastComponent>() ?? unit.AddComponent<SkillCastComponent>();
            castComp.SkillConfigId = skillId;
            castComp.Target = target;
            castComp.AoeTargets.Clear();
            castComp.SkillStartTime = TimeInfo.Instance.ClientFrameTime();
            castComp.NextEventIndex = 0;

            // 缓存技能数据和事件列表，避免 TickCasting 每帧查表
            castComp.CachedData = skillData;
            castComp.CachedEvents = skillData.Events;

            await self.StartSkillTimeline();
            return true;
        }

        private static async ETTask StartSkillTimeline(this SkillComponent self)
        {
            TimerComponent timer = self.GetTimer();
            var unit = self.GetParent<Unit>();
            var castComp = unit.GetComponent<SkillCastComponent>();
            if (castComp == null || castComp.SkillConfigId == 0) return;

            var stateComp = unit.GetComponent<StateComponent>();
            while (!self.IsDisposed && !unit.IsDisposed)
            {

                // 单位死亡时立即中断技能时间轴
                if ( stateComp.State == UnitState.Death)
                {
                    castComp.SkillConfigId = 0;
                    castComp.Target = default;
                    castComp.AoeTargets.Clear();
                    castComp.CachedData = null;
                    castComp.CachedEvents = null;
                    return;
                }

                self.TickCasting(castComp);

                if (castComp.SkillConfigId == 0) return; // 技能已结束

                await timer.WaitFrameAsync();
            }
        }

        private static void TickCasting(this SkillComponent self, SkillCastComponent castComp)
        {
            var unit = self.GetParent<Unit>();
            var skillData = castComp.CachedData;
            var events = castComp.CachedEvents;
            if (skillData == null || events == null) return;

            long elapsed = TimeInfo.Instance.ClientFrameTime() - castComp.SkillStartTime;

            // 遍历未执行的事件
            while (castComp.NextEventIndex < events.Count)
            {
                var ev = events[castComp.NextEventIndex];
                if (elapsed < ev.Timestamp) break;

                castComp.NextEventIndex++;

                ExecuteEvent(unit, castComp, ev);
            }

            // 全部事件执行完毕 → 结束技能，进入CD
            if (castComp.NextEventIndex >= events.Count && elapsed >= skillData.Duration)
            {
                self.StartCooldown(castComp.SkillConfigId, skillData.CD);
                castComp.SkillConfigId = 0;
                castComp.Target = default;
                castComp.AoeTargets.Clear();
                castComp.CachedData = null;
                castComp.CachedEvents = null;
            }
        }

        // ═══════════════════════════════════════════════════
        //  事件分发
        // ═══════════════════════════════════════════════════

        private static void ExecuteEvent(Unit caster, SkillCastComponent castComp, SkillEventData ev)
        {
            switch ((SkillEventType)ev.EventType)
            {
                case SkillEventType.PlayAnimation:
                    SkillEvent_PlayAnimation.Execute(caster, ev);
                    break;
                case SkillEventType.FindTarget:
                    SkillEvent_FindTarget.Execute(caster, castComp, ev);
                    break;
                case SkillEventType.SpawnVFX:
                    SkillEvent_SpawnVFX.Execute(caster, ev);
                    break;
                case SkillEventType.ApplyValue:
                    SkillEvent_ApplyValue.Execute(caster, castComp, ev);
                    break;
                case SkillEventType.AddBuff:
                    SkillEvent_AddBuff.Execute(caster, castComp, ev);
                    break;
                case SkillEventType.SpawnBullet:
                    SkillEvent_SpawnBullet.Execute(caster, castComp, ev);
                    break;
                default:
                    Log.Error($"[SkillEvent] 未知事件类型 EventType={ev.EventType}, Caster={caster?.Id}");
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