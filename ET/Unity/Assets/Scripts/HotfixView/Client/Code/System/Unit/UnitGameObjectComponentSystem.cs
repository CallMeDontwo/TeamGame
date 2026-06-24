namespace ET.TeamGame
{
    [EntitySystemOf(typeof(UnitGameObjectComponent))]
    [FriendOf(typeof(UnitGameObjectComponent))]
    public static partial class UnitGameObjectComponentSystem
    {
        [EntitySystem]
        private static void Destroy(this UnitGameObjectComponent self)
        {
            if (self.GameObject != null)
            {
                // 清理视图组件再回池
                self.GameObject.GetComponent<HPBarComponent>()?.Release();
                self.GameObject.GetComponent<AnimatorView>()?.Clear();

                // 重置 Transform 残留（朝向、位置等会被新 Unit 覆盖）
                self.GameObject.transform.localRotation = UnityEngine.Quaternion.identity;

                GameObjectPool.Recycle(self.GameObject);
                self.GameObject = null;
            }
        }
        [EntitySystem]
        private static void Awake(this ET.TeamGame.UnitGameObjectComponent self, UnityEngine.GameObject args2)
        {

        }
        [EntitySystem]
        private static void Awake(this ET.TeamGame.UnitGameObjectComponent self)
        {

        }

        public static void SetAnimationFlipX(this UnitGameObjectComponent self, bool flipX)
        {
            if (self.Animation == null) return;
            self.Animation.skeleton.ScaleX = flipX ? -1 : 1;
        }
    }
}
