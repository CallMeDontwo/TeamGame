# 拾掇灵伏魔 — 项目流程总览

> 基于 ET 框架的横版策略+类 RTS 游戏，纯客户端先行。

---

## 一、启动流程

```
Unity Global.Start()
  │
  ├─ CodeLoader.LoadAssembly() → 加载 Model/Hotfix 程序集
  ├─ ET.Entry.StartAsync()
  │   ├─ 添加全局单例（TimeInfo, IdGenerater, ObjectPool...）
  │   ├─ CodeTypes.CreateCode() → 创建 [Code] 单例
  │   │   ├─ EventSystem → 注册 [Event] / [Invoke] 处理器
  │   │   └─ SceneCreatDispatcher → 注册 [SceneCreat] 创建器
  │   ├─ ConfigLoader.LoadAsync() → 加载 Excel 配置
  │   └─ AppInit → 初始化 FairyGUI + 显示 Loading
  │
  └─ FiberManager.Create(Main, -1, 0, SceneType.Main, "Main")
        └─ FiberCreate_Main 事件
              │
              ├─ MainSceneCreater1.OnCreate (Order=1)
              │   └─ 添加 Timer, Session, CurrentScenes 等核心组件
              ├─ MainSceneCreater3~7.OnCreate (Order=3~7)
              │   └─ 资源加载、FGUI层级面板
              │
              └─ MainSceneCreater1.OnCreateComplete (Order=1)
                    └─ CurrentSceneFactory.Create(parent, 0, "TeamGame")
                          └─ CreateInternal
                                ├─ EntitySceneFactory.CreateScene(SceneType.Current, "TeamGame")
                                ├─ scene.AddComponent<TimerComponent>() ← 自动
                                ├─ TeamGameSceneCreater0.OnCreate (Order=0, Data)
                                │   └─ scene.AddComponent<UnitManager>()
                                └─ TeamGameSceneCreater1.OnCreate (Order=10, View)
                                      ├─ YooAssets.LoadSceneAsync("TeamGame") → 加载.unity
                                      ├─ 创建 AIDebugger GameObject (F1开关)
                                      └─ UnitManager.CreateTestUnits(scene)
                                            ├─ UnitFactory.CreateHero(1001, (-5,0,0))
                                            │   → Identity + State + Numeric + Move + AI + Perception
                                            └─ UnitFactory.CreateMonster(2001, (5,0,0))
                                                → Identity + State + Numeric + Perception + Monster + AI + Move
```

---

## 二、场景 ECS 结构

```
Scene("TeamGame")
  │
  ├── UnitManager              → 所有 Unit 的父容器
  │   ├── Unit (Hero)
  │   │   ├── IdentityComponent     [Hero, "英雄", Lv1]
  │   │   ├── StateComponent        [Idle/Move/Attack/Hit/Death]
  │   │   ├── NumericComponent      [Speed=config.MoveSpeed/100, HP=config.MaxHP]
  │   │   ├── MoveComponent         [框架逐帧移动]
  │   │   ├── AIComponent           [AIConfigId, CurrentBehaviorName, AttackNextTime]
  │   │   ├── PerceptionComponent   [SightRange, PrimaryTargetId, ScanInterval=100ms]
  │   │   └── UnitGameObjectComponent  [持有 GameObject + Transform]
  │   ├── Unit (Monster)
  │   │   ├── IdentityComponent     [Monster]
  │   │   ├── StateComponent
  │   │   ├── NumericComponent      [Speed, HP from MonsterConfig]
  │   │   ├── PerceptionComponent
  │   │   ├── MonsterComponent      [AIConfigId, IsElite, IsBoss]
  │   │   ├── AIComponent
  │   │   ├── MoveComponent
  │   │   └── UnitGameObjectComponent
  │   └── ...
  │
  └── TimerComponent              ← CurrentSceneFactory 自动添加

GameObject (Debug):
  └── AIDebugger : MonoBehaviour  [OnGUI, F1 toggle]
```

---

## 三、AI 系统架构

### 主循环（每帧决策 + 非阻塞移动）

```
AIComponent.StartAICycle()
  │
  ├─ while (!IsDisposed)
  │   ├─ scan: perception?.ScanSurrounding()         (内部100ms频率控制)
  │   ├─ decide: DecideNextHandler()
  │   │   ├─ 有目标+在攻击范围 → Attack
  │   │   ├─ 有目标+不在攻击范围 → Chase
  │   │   ├─ 无目标+怪物+有Patrol → Patrol
  │   │   └─ 无目标 → Idle
  │   ├─ execute:
  │   │   ├─ Attack → StopMovement + FaceTarget + TryAttack (冷却控制)
  │   │   ├─ Chase  → DoChaseTo().Coroutine() (每帧覆盖方向)
  │   │   ├─ Patrol → DoPatrolStep().Coroutine() (小步左推, minX=-2)
  │   │   └─ Idle   → StopMovement
  │   └─ await WaitFrameAsync()
```

### 移动控制（非阻塞）

```
DoChaseTo(unit, target, attackRange, speed):
  stopPos = targetPos - normalize(targetPos - selfPos) * attackRange
  mc.MoveToAsync([selfPos, stopPos], speed).Coroutine()  ← 不等待
  FaceDirection(stopPos - selfPos)

DoPatrolStep(unit, config, speed):
  newPos.x -= step (每帧~0.5单位)
  clamp newPos.x >= -2
  mc.MoveToAsync([selfPos, newPos], speed).Coroutine()
```

### 攻击冷却

```csharp
if (now >= AttackNextTime) {
    AttackNextTime = now + attackCD;
    // 播放攻击
}
```

---

## 四、感知系统

```
PerceptionComponent.ScanSurrounding()
  │
  ├─ 限频：每 ScanInterval(100ms) 扫描一次
  ├─ 遍历 UnitManager.Children
  │   ├─ 跳过自己 + 无 IdentityComponent 的单位
  │   └─ IsEnemy() 双向判定：Monster↔Hero 互为敌对
  ├─ 距离检测：dist² ≤ sightRange²
  └─ 选择最近目标作为 PrimaryTargetId
```

---

## 五、动画系统

```
逻辑层 → 视图层事件驱动
─────────────────────────
AI → StateComponent.ChangeState(Move)
     │
     ├─ StateComponent.State = Move
     └─ Publish(UnitStateChanged)
          │
          ▼ HotfixView
     UnitStateChanged_PlayAnimation
          │
          ├─ AnimatorComponent.CurrentAnimation = "walk"   (逻辑层记录)
          └─ AnimatorView.PlayAnimation("walk")             (视图层 Spine播放)
```

状态→动画映射：Idle="idle", Move/Move="walk", Attack="attack", Hit="hit", Death="death"

初始同步：UnitViewFactory.CreateView 末尾补发 UnitStateChanged 事件，解决视图异步加载时序差。

---

## 六、Excel 配置表

| 表名 | 关键字段 | 状态 |
|------|---------|------|
| AIConfig | HandlerIds, SightRange, AttackRange, PatrolRadius, CheckInterval... | ✅ |
| HeroConfig | AIconfigId, MoveSpeed, MaxHP, Attack, Scale... | ✅ |
| MonsterConfig | AIconfigId, MoveSpeed, MaxHP, Attack... | ✅ |

**约束**：float→int/100, bool→int(0/1)

---

## 七、文件索引

### AI 系统
| 路径 | 说明 |
|------|------|
| `Model/Client/Code/Module/AI/AIComponent.cs` | AI数据 + AIHandlerResult枚举 |
| `Model/Client/Code/Module/AI/PerceptionComponent.cs` | 感知数据 |
| `Hotfix/Client/Code/Module/AI/AIComponentSystem.cs` | 决策循环 + 行为方法 |
| `Hotfix/Client/Code/Module/AI/PerceptionComponentSystem.cs` | 感知扫描 |

### 动画系统
| 路径 | 说明 |
|------|------|
| `Model/Client/Code/Component/TeamGame/StateComponent.cs` | 状态数据 |
| `Model/Client/Code/Component/TeamGame/AnimatorComponent.cs` | 动画数据 |
| `Hotfix/Client/Code/System/TeamGame/StateComponentSystem.cs` | 状态切换+事件发布 |
| `ModelView/Client/MonoBehaviour/World/Unit/AnimatorView.cs` | Spine播放(视图层) |
| `HotfixView/Client/Code/Event/World/Unit/UnitStateChanged_PlayAnimation.cs` | 动画事件处理器 |

### 场景系统
| 路径 | 说明 |
|------|------|
| `Hotfix/Client/Code/System/Scene/Game/TeamGameSceneCreater0.cs` | 数据层初始化 |
| `HotfixView/Client/Code/System/Scene/TeamGameSceneCreater1.cs` | 视图层加载+测试Unit |
| `Hotfix/Client/Code/Factory/UnitFactory.cs` | Unit创建（含AI/数值初始化） |

### View 层
| 路径 | 说明 |
|------|------|
| `ModelView/Client/Code/Component/World/Unit/UnitGameObjectComponent.cs` | GameObject持有 |
| `HotfixView/Client/Code/System/Unit/UnitViewFactory.cs` | 预制体加载+视图组件挂载 |
| `HotfixView/Client/Code/Event/World/Unit/ChangePosition_Event.cs` | 位置同步 |
| `HotfixView/Client/Code/Event/World/Unit/ChangeRotation_Event.cs` | 朝向同步(Forward.x<0→FlipX) |

### 调试
| 路径 | 说明 |
|------|------|
| `ModelView/Client/MonoBehaviour/Debug/AIDebugger.cs` | AI状态OnGUI面板(F1) |

---

## 八、运行时调试

按 **F1** 打开/关闭 AI Debug Overlay：

```
┌───────┬──────┬──────┬──────────┬──────┬──────┬──────┐
│UnitID │Type  │AIcfg │Behavior  │Target│HP    │Sight │
│1001   │Hero  │1001  │Attack    │2001  │500   │10.0  │
│2001   │Mons… │1001  │Chase     │1001  │300   │10.0  │
└───────┴──────┴──────┴──────────┴──────┴──────┴──────┘
```

颜色：Attack=红, Chase=黄, Patrol=绿, Idle=灰
