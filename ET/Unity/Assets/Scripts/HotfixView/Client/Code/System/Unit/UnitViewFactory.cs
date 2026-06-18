using ET.TeamGame;
using FairyGUI;
using Spine;
using Spine.Unity;
using UnityEngine;
using YooAsset;

namespace ET.TeamGame
{
    /// <summary>
    /// Unit 视图工厂 — 为逻辑层的 Unit 创建对应的 GameObject
    /// 只能在视图层（HotfixView）调用，逻辑层通过 EventSystem 事件触发
    /// </summary>
    public static class UnitViewFactory
    {
        /// <summary>为 Unit 创建视图（加载预制体 + 实例化 + 挂组件）</summary>
        public static async ETTask CreateView(Unit unit)
        {
            string prefabPath = GetPrefabPath(unit);

            // 从对象池获取 GameObject
            GameObject go = await GameObjectPool.FetchAsync(prefabPath);
            if (go == null)
            {
                Log.Error($"创建视图失败: {prefabPath}");
                return;
            }

            go.name = $"{unit.ConfigId}_{unit.Id}";

            // 已有 UnitGameObjectComponent 则跳过
            if (unit.TryGetComponent(out UnitGameObjectComponent existing))
            {
                GameObjectPool.Recycle(go);
                Log.Warning($"Unit {unit.Id} 已有视图，跳过创建");
                return;
            }

            var view = unit.AddComponent<UnitGameObjectComponent>();
            view.GameObject = go;

            // 子弹 Unit 不需要动画/血条/状态
            bool isBullet = unit.GetComponent<BulletComponent>() != null;

            if (!isBullet)
            {
                // 添加动画视图
                go.AddComponent<AnimatorView>().skeleton = go.GetComponentInChildren<SkeletonAnimation>();

                // 创建血条UI
                CreateHPBar(unit, go);

                // 初始动画同步 — 视图创建时 AI 可能已经开始运行，状态已切换
                var stateComp = unit.GetComponent<StateComponent>();
                if (stateComp != null)
                {
                    EventSystem.Instance.Publish(unit.Scene(), new UnitStateChanged()
                    {
                        Unit = unit,
                        OldState = UnitState.None,
                        NewState = stateComp.State,
                    });
                }
            }

            // 初始位置同步
            go.transform.position = unit.Position;

            // 碰撞圆可视化
            CreateCollisionCircle(unit, go, isBullet);
        }

        /// <summary>根据 Unit 类型创建碰撞圆可视化</summary>
        private static void CreateCollisionCircle(Unit unit, GameObject go, bool isBullet)
        {
            float radius;
            Color color;
            float offsetY = 0f;

            if (isBullet)
            {
                var bc = unit.GetComponent<BulletComponent>();
                var bulletCfg = BulletConfigCategory.Instance.Get(bc.BulletConfigId);
                radius = bulletCfg.CollisionRadius / 100f;
                color = new Color(1f, 0.92f, 0.016f, 0.5f); // 黄色半透明
            }
            else
            {
                var identity = unit.GetComponent<IdentityComponent>();
                if (identity == null) return;

                switch (identity.UnitType)
                {
                    case UnitType.Hero:
                        radius = HeroConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius / 100f;
                        color = new Color(0f, 0.8f, 0f, 0.4f);   // 绿色半透明
                        offsetY = radius;  // 身体中心 = 锚点 + 半径
                        break;
                    case UnitType.Monster:
                        radius = MonsterConfigCategory.Instance.Get(unit.ConfigId).CollisionRadius / 100f;
                        color = new Color(0.9f, 0f, 0f, 0.4f);    // 红色半透明
                        offsetY = radius;  // 身体中心 = 锚点 + 半径
                        break;
                    default:
                        radius = 0.4f;
                        color = new Color(0.5f, 0.5f, 0.5f, 0.4f); // 灰色
                        offsetY = radius;
                        break;
                }
            }

            if (radius <= 0f) return;

            // 子对象：圆心偏移到身体中心
            var circleGo = new GameObject("CollisionCircle");
            circleGo.transform.SetParent(go.transform, false);
            circleGo.transform.localPosition = new Vector3(0, offsetY, 0);
            var circle = circleGo.AddComponent<CollisionCircle>();
            circle.Setup(radius, color);
        }

        private static string GetPrefabPath(Unit unit)
        {
            // 子弹：从 BulletComponent 读取 PrefabPath
            var bulletComp = unit.GetComponent<BulletComponent>();
            if (bulletComp != null && !string.IsNullOrEmpty(bulletComp.PrefabPath))
                return bulletComp.PrefabPath;

            // 普通 Unit：YooAsset 配置为 AddressByFileName，直接用 ConfigId 作为地址
            return $"{unit.ConfigId}";
        }

        /// <summary>创建血条UI — 作为GameObject的子3D物体</summary>
        private static void CreateHPBar(Unit unit, GameObject gameObject)
        {
            var numeric = unit.GetComponent<NumericComponent>();
            if (numeric == null) return;

            int maxHp = numeric.GetAsInt(NumericType.MaxHP);
            int curHp = numeric.GetAsInt(NumericType.HP);
            if (maxHp <= 0) return;

            var hpBar = gameObject.EnsureComponent<HPBarComponent>();
            if (!hpBar.IsCreated)
            {
                hpBar.Create(maxHp, curHp);
            }
        }
    }
}
