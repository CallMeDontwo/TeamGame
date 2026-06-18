# Unit 视图层完成

## 新增文件（4个）

| 文件 | 说明 |
|------|------|
| `HotfixView/.../UnitViewFactory.cs` | 加载预制体+实例化+挂 UnitGameObjectComponent |
| `HotfixView/.../UnitGameObjectComponentSystem.cs` | Unit 销毁时自动 Destroy(GameObject) |
| `HotfixView/.../AfterHeroViewCreate_CreateView.cs` | 监听英雄创建事件，调用 ViewFactory |
| `HotfixView/.../AfterMonsterViewCreate_CreateView.cs` | 监听怪物创建事件，调用 ViewFactory |

## 修改文件（2个）

| 文件 | 改动 |
|------|------|
| `Model/Share/.../EventType_Unit.cs` | 新增 `AfterHeroViewCreate` / `AfterMonsterViewCreate` 事件结构体 |
| `Hotfix/Client/.../UnitFactory.cs` | CreateHero/Monster 末尾发布对应事件 |

## 完整流程

```
逻辑层                                    视图层
UnitFactory.CreateHero()                  
  └─ AddChild<Unit>(configId)              
  └─ IdentityComponent(Hero)              
  └─ EventSystem.Publish(                 
       AfterHeroViewCreate { Unit })      
        ──── 事件跨层 ─────►  [Event] AfterHeroViewCreate_CreateView
                                └─ UnitViewFactory.CreateView(unit)
                                    ├─ YooAssets.LoadAssetAsync("Prefabs/Hero/{configId}")
                                    ├─ Instantiate
                                    └─ unit.AddComponent<UnitGameObjectComponent>()

Unit.SetPosition()                        [Event] ChangePosition_Event (已有)
  └─ ChangePosition 事件 ──►  sync Transform.position

Unit.Dispose()                            UnitGameObjectComponentSystem.Destroy
  └─ 组件自动销毁       ──►  Object.Destroy(GameObject)
```
