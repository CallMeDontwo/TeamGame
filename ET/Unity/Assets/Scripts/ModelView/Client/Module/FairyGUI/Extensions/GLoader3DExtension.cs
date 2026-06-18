using Spine.Unity;
using UnityEngine;

namespace FairyGUI
{
    public static class GLoader3DExtension
    {
        public static void SetLoaderSpine(this GLoader3D self, SkeletonDataAsset skeleton)
        {
            float width = self.width;
            float height = self.height;
            self.fill = FillType.ScaleFree;
            self.SetSpine(skeleton, (int)width, (int)height, new Vector2(width, height) * self.pivot);
        }

        public static void SetLoaderSpine(this GLoader3D self, SkeletonDataAsset skeleton, float scale)
        {
            self.SetLoaderSpine(skeleton);
            self.spineAnimation.transform.localScale *= scale;
        }

        public static void SetLoaderSpine(this GLoader3D self, SkeletonDataAsset skeleton, Vector2 scale)
        {
            self.SetLoaderSpine(skeleton);
            self.spineAnimation.transform.localScale *= scale;
        }
    }
}