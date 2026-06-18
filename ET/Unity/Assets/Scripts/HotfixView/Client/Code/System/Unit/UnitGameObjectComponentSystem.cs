using UnityEngine;

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
                // 清理碰撞圆子对象（避免回池后残留）
                var circle = self.GameObject.transform.Find("CollisionCircle");
                if (circle != null)
                    UnityEngine.Object.Destroy(circle.gameObject);

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
