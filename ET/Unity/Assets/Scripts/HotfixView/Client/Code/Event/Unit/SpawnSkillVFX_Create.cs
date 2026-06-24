using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 监听 SpawnSkillVFX 事件 → 从对象池获取特效预制体并实例化到施法者位置
    /// VFX 粒子播完后自动回池
    /// </summary>
    [Event(SceneType.Current)]
    internal class SpawnSkillVFX_Create : AEvent<Scene, SpawnSkillVFX>
    {
        protected override async ETTask Run(Scene scene, SpawnSkillVFX a)
        {
            var caster = a.Caster;
            if (caster.IsDisposed) return;

            string prefabPath = a.PrefabPath;
            if (string.IsNullOrEmpty(prefabPath)) return;

            // 从对象池获取 VFX 实例
            var vfx = await GameObjectPool.FetchAsync(prefabPath);
            if (vfx == null)
            {
                Log.Warning($"技能特效加载失败: {prefabPath}");
                return;
            }

            // 挂到施法者下 + 设置本地偏移（人物朝左时 OffsetX 取反，保持"右正"语义）
            if (caster.TryGetComponent(out UnitGameObjectComponent view) && view.GameObject != null)
            {
                vfx.transform.SetParent(view.GameObject.transform);
            }
            int facingSign = caster.Forward.x < 0 ? -1 : 1;
            vfx.transform.localPosition = new Vector3(a.OffsetX * facingSign, a.OffsetY, 0);
            vfx.name = $"VFX_{a.Caster.Id}_{prefabPath}";

            // 设置定时回收
            await scene.GetComponent<TimerComponent>().WaitAsync(a.Duration);

            // 解除父子关系后回池
            vfx.transform.SetParent(null);
            GameObjectPool.Recycle(vfx);
        }
    }
}
