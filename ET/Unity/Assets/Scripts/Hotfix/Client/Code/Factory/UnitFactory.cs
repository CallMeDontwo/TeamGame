using System.Linq;
using Unity.Mathematics;

namespace ET.TeamGame
{
    /// <summary>
    /// Unit 工厂 — 创建英雄/怪物等各类 Unit 实体
    /// 前置条件：Scene 上已挂载 UnitManager（TeamGameSceneCreater0 已添加）
    /// </summary>
    public static class UnitFactory
    {
        /// <summary>创建英雄 Unit</summary>
        public static async ETTask<Unit> CreateHero(Scene scene, int configId, float3 position)
        {
            var unitManager = scene.GetComponent<UnitManager>();

            var unit = unitManager.AddChild<Unit, int>(configId);

            var identity = unit.AddComponent<IdentityComponent>();
            identity.UnitType = UnitType.Hero;
            identity.Name = "英雄";
            identity.Level = 1;

            unit.AddComponent<StateComponent>();

            // 数值组件（从配置的 NumericType + NumericValue 数组批量初始化）
            var heroConfig = HeroConfigCategory.Instance.Get(configId);
            identity.Height = heroConfig.Height;
            var numeric = unit.AddComponent<NumericComponent>();
            numeric.InitFromArrays(heroConfig.NumericType, heroConfig.NumericValue);
            numeric.Set(NumericType.HP, numeric.GetAsInt(NumericType.MaxHP));

            unit.AddComponent<MoveComponent>();

            // 攻击组件（普攻走技能系统管道）
            var attackComp = unit.AddComponent<AttackComponent>();
            attackComp.BasicAttackSkillId = heroConfig.BasicAttackSkillId;

            //动画组件
            unit.AddComponent<AnimatorComponent>();

            // 技能组件（从 HeroConfig.SkillIds 初始化，过滤 0）
            var skillComp = unit.AddComponent<SkillComponent>();
            if (heroConfig.SkillIds != null)
                skillComp.SkillIds = heroConfig.SkillIds.Where(id => id > 0).ToArray();

            // 计算单位最大可达高度 = max(普攻, 所有技能)
            if (heroConfig.BasicAttackSkillId > 0)
            {
                var basicSkill = SkillDataStore.Get(heroConfig.BasicAttackSkillId);
                if (basicSkill != null)
                    attackComp.ReachHeight = basicSkill.ReachHeight;
            }
            if (skillComp.SkillIds != null)
            {
                foreach (int sid in skillComp.SkillIds)
                {
                    var data = SkillDataStore.Get(sid);
                    if (data != null && data.ReachHeight > attackComp.ReachHeight)
                        attackComp.ReachHeight = data.ReachHeight;
                }
            }

            unit.AddComponent<PerceptionComponent>();
           var aIComponent = unit.AddComponent<AIComponent, int>(heroConfig.AIconfigId);

            var perception = unit.GetComponent<PerceptionComponent>();
            if (perception != null)
            {
                var aiConfig = AIConfigCategory.Instance.Get(heroConfig.AIconfigId);
                perception.InitFromConfig(aiConfig.SightRange);
            }

            unit.SetPositon(position, false);

            // 通知视图层创建 GameObject
            await EventSystem.Instance.PublishAsync(scene, new AfterHeroCreate() { Unit = unit });
           aIComponent.ResumeAI();
            return unit;
        }

        /// <summary>创建怪物 Unit</summary>
        public static async ETTask<Unit> CreateMonster(Scene scene, int configId, float3 position)
        {
            var unitManager = scene.GetComponent<UnitManager>();

            var unit = unitManager.AddChild<Unit, int>(configId);

            var identity = unit.AddComponent<IdentityComponent>();
            identity.UnitType = UnitType.Monster;
            identity.Name     = "怪物";

            unit.AddComponent<StateComponent>();

            // 数值组件（从配置的 NumericType + NumericValue 数组批量初始化）
            var monsterConfig = MonsterConfigCategory.Instance.Get(configId);
            identity.Height = monsterConfig.Height;
            var numeric = unit.AddComponent<NumericComponent>();
            numeric.InitFromArrays(monsterConfig.NumericType, monsterConfig.NumericValue);
            numeric.Set(NumericType.HP, numeric.GetAsInt(NumericType.MaxHP));

            unit.AddComponent<PerceptionComponent>();
            unit.AddComponent<MonsterComponent>().InitFromConfig(configId, monsterConfig);
            unit.AddComponent<MoveComponent>();
            unit.AddComponent<AnimatorComponent>();

            // 技能组件（从 MonsterConfig.SkillIds 初始化，过滤 0）
            var skillComp = unit.AddComponent<SkillComponent>();
            if (monsterConfig.SkillIds != null)
                skillComp.SkillIds = monsterConfig.SkillIds.Where(id => id > 0).ToArray();

            // 攻击组件（普攻走技能系统管道）
            var attackComp = unit.AddComponent<AttackComponent>();
            attackComp.BasicAttackSkillId = monsterConfig.BasicAttackSkillId;

            // 计算单位最大可达高度 = max(普攻, 所有技能)
            if (monsterConfig.BasicAttackSkillId > 0)
            {
                var basicSkill = SkillDataStore.Get(monsterConfig.BasicAttackSkillId);
                if (basicSkill != null)
                    attackComp.ReachHeight = basicSkill.ReachHeight;
            }
            if (skillComp.SkillIds != null)
            {
                foreach (int sid in skillComp.SkillIds)
                {
                    var data = SkillDataStore.Get(sid);
                    if (data != null && data.ReachHeight > attackComp.ReachHeight)
                        attackComp.ReachHeight = data.ReachHeight;
                }
            }

            unit.SetPositon(position, false);

            // 通知视图层创建 GameObject
           await EventSystem.Instance.PublishAsync(scene, new AfterMonsterCreate() { Unit = unit });
            unit.GetComponent<AIComponent>().ResumeAI();
            return unit;
        }
    }
}
