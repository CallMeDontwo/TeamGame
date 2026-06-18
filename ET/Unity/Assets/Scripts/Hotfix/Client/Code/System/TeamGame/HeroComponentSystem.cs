namespace ET.TeamGame
{
    [EntitySystemOf(typeof(HeroComponent))]
    [FriendOf(typeof(HeroComponent))]
    public static partial class HeroComponentSystem
    {
        [EntitySystem]
        private static void Awake(this HeroComponent self)
        {
            self.EXP           = 0;
            self.SkillIds      = System.Array.Empty<int>();
            self.EquipIds      = System.Array.Empty<int>();
            self.TeamSlot      = 0;
            self.IsPlayerControlled = false;
        }

        [EntitySystem]
        private static void Destroy(this HeroComponent self)
        {
            self.SkillIds = System.Array.Empty<int>();
            self.EquipIds = System.Array.Empty<int>();
        }

        /// <summary>
        /// 增加经验值，返回是否升级
        /// </summary>
        public static bool AddEXP(this HeroComponent self, long amount)
        {
            self.EXP += amount;
            // TODO: 读取经验表判断升级
            return false;
        }

        /// <summary>
        /// 学会新技能（去重）
        /// </summary>
        public static void LearnSkill(this HeroComponent self, int skillConfigId)
        {
            if (System.Array.IndexOf(self.SkillIds, skillConfigId) >= 0)
            {
                return;
            }

            int[] newSkills = new int[self.SkillIds.Length + 1];
            System.Array.Copy(self.SkillIds, newSkills, self.SkillIds.Length);
            newSkills[self.SkillIds.Length] = skillConfigId;
            self.SkillIds = newSkills;
        }

        /// <summary>
        /// 装备物品
        /// </summary>
        public static void Equip(this HeroComponent self, int equipConfigId)
        {
            int[] newEquips = new int[self.EquipIds.Length + 1];
            System.Array.Copy(self.EquipIds, newEquips, self.EquipIds.Length);
            newEquips[self.EquipIds.Length] = equipConfigId;
            self.EquipIds = newEquips;
        }
    }
}
