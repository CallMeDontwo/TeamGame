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

        /// <summary>
        /// 创建测试用英雄和怪物（只在调试阶段使用）
        /// </summary>
        public static async ETTask CreateTestUnits(this UnitManager self, Scene scene)
        {
            Scene s = scene ?? self.Scene();

            // 创建英雄（configId=1001，位置 x=-5）
            await UnitFactory.CreateHero(s, 1001, new float3(-5, 0, 0));

            // 创建怪物（configId=2001，位置 x=5）
            await UnitFactory.CreateMonster(s, 99001, new float3(0, 0, 0));
            //tawait UnitFactory.CreateMonster(s, 2001, new float3(5, 1.5f, 0));
        }
    }
}
