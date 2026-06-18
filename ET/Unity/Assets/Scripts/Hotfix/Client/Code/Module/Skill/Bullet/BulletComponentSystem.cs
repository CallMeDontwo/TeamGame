namespace ET.TeamGame
{
    [EntitySystemOf(typeof(BulletComponent))]
    [FriendOf(typeof(BulletComponent))]
    public static partial class BulletComponentSystem
    {
        [EntitySystem]
        private static void Awake(this BulletComponent self)
        {
        }

        [EntitySystem]
        private static void Destroy(this BulletComponent self)
        {
            self.Caster = default;
        }
    }
}
