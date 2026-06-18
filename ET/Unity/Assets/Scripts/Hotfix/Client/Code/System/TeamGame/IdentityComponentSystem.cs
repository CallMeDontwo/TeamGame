namespace ET.TeamGame
{
    [EntitySystemOf(typeof(IdentityComponent))]
    [FriendOf(typeof(IdentityComponent))]
    public static partial class IdentityComponentSystem
    {
        [EntitySystem]
        private static void Awake(this IdentityComponent self)
        {
            self.UnitType = UnitType.None;
            self.Name     = null;
            self.Level    = 1;
        }

        [EntitySystem]
        private static void Destroy(this IdentityComponent self)
        {
            self.Name = null;
        }

        /// <summary>从 HeroConfig / MonsterConfig 加载身份数据（TODO: 按角色类型区分）</summary>
        public static void LoadFromConfig(this IdentityComponent self, int configId)
        {
            //self.UnitType = ...;
            //self.Name     = ...;
            //self.Level    = ...;
        }

        /// <summary>升级</summary>
        public static void LevelUp(this IdentityComponent self)
        {
            self.Level++;
        }
    }
}
