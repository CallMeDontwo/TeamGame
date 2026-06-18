using Unity.Mathematics;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 碰撞圆可视化 — SpriteRenderer 绘制半透明圆，大小匹配实际碰撞半径
    /// 直接挂在 Unit GameObject 上作为组件，不需要子对象
    /// </summary>
    [EnableClass]
    public class CollisionCircle : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private static Sprite sharedSprite;

        /// <summary>初始化（设置半径和颜色）</summary>
        public void Setup(float radius, Color color)
        {
            EnsureSharedSprite();

            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = sharedSprite;
            spriteRenderer.color = color;
            spriteRenderer.sortingOrder = 1;

            SetRadius(radius);
        }

        public void SetRadius(float radius)
        {
            if (spriteRenderer != null && radius > 0f)
            {
                // sharedSprite 的 pixelsPerUnit = TEX_SIZE，即纹理边长 = 1 单位
                // 所以 transform.scale = 直径 即得到正确大小
                float diameter = radius * 2f;
                transform.localScale = new Vector3(diameter, diameter, 1f);
            }
        }

        public void SetColor(Color color)
        {
            if (spriteRenderer != null)
                spriteRenderer.color = color;
        }

        private static void EnsureSharedSprite()
        {
            if (sharedSprite != null) return;

            const int size = 32;
            var tex = new Texture2D(size, size, TextureFormat.RGBA32, false);
            tex.filterMode = FilterMode.Bilinear;
            tex.wrapMode = TextureWrapMode.Clamp;

            Color[] pixels = new Color[size * size];
            float half = (size - 1) * 0.5f;
            float r = half * 0.88f;
            float rSq = r * r;

            for (int y = 0; y < size; y++)
            for (int x = 0; x < size; x++)
            {
                float dSq = (x - half) * (x - half) + (y - half) * (y - half);
                if (dSq <= rSq)
                {
                    float edge = r - math.sqrt(dSq);
                    float a = math.saturate(edge * 3f + 0.3f) * 0.45f;
                    pixels[y * size + x] = new Color(1, 1, 1, a);
                }
                else
                {
                    pixels[y * size + x] = Color.clear;
                }
            }

            tex.SetPixels(pixels);
            tex.Apply();

            // PPU = size，sprite 边长 = 1 单位
            sharedSprite = Sprite.Create(tex, new Rect(0, 0, size, size), new Vector2(0.5f, 0.5f), size);
        }
    }
}
