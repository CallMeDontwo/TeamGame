# AI 组件系统 — 设计与实现文档

> 版本: v1.0 | 状态: Phase1 完成 | 最后更新: 2026-06-12

---

## 一、架构总览

### 四层分层架构

```
┌─────────────────────────────────────────────┐
│            Layer 4: AI 决策层                │
│     AIComponent + AIHandler 链式决策         │
│       (固定顺序循环: Idle→Patrol→Chase→...)  │
├─────────────────────────────────────────────┤
│            Layer 3: 行为节点层               │
│     5个独立 AIHandler（单一职责）            │
│     Idle / Patrol / Chase / Attack / Return  │
├─────────────────────────────────────────────┤
│            Layer 2: 感知层                   │
│     PerceptionComponent × Unit遍历          │
│     筛选敌对目标 + 距离检测                 │
├─────────────────────────────────────────────┤
│            Layer 1: 基础能力层               │
│  MoveComponent  Pathfinding  StateComponent │
│  NumericComponent  AnimatorComponent        │
└─────────────────────────────────────────────┘
```

### 技术选型

- **决策模式**: Handler 链式决策（非行为树）
  - 优势：轻量、单一职责、配置驱动、易于调试
  - 劣势：不如行为树灵活组合
  - Phase1 采用固定顺序循环，Phase2 升级为状态驱动跳转
- **框架**: ET ECS 架构（Component + System 分离）
- **范围**: 纯客户端先行，不涉及服务端

---

## 二、核心组件定义

### 2.1 AIComponent — AI大脑

**路径**: `Model/Client/Code/Module/AI/AIComponent.cs`

```csharp
[ComponentOf(typeof(Unit))]
public sealed partial class AIComponent : Entity, IAwake<int>, IDestroy
{
    public int AIConfigId { get; set; }           // 关联 AIConfig 表
    public int CurrentHandler { get; set; }        // 当前 Handler ID
    public int HandlerIndex { get; set; }          // HandlerIds 数组索引
    public bool Paused { get; set; }               // 暂停（眩晕/冰冻）
    public long ChangeTime { get; set; }           // 上次切换时间
    public ETCancellationToken CancellationToken { get; set; }  // 协程取消令牌
}
```

### 2.2 PerceptionComponent — 感知系统

**路径**: `Model/Client/Code/Module/AI/PerceptionComponent.cs`

```csharp
[ComponentOf(typeof(Unit))]
public sealed partial class PerceptionComponent : Entity, IAwake, IDestroy
{
    public int SightRange { get; set; }            // 视野范围（int/100）
    public List<long> VisibleTargets { get; set; } // 可见 Unit ID 列表
    public long PrimaryTargetId { get; set; }      // 当前主目标 Unit ID
    public long LastScanTime { get; set; }         // 上次扫描时间
    public long ScanInterval { get; set; }          // 扫描间隔（默认500ms）
}
```

**扫描逻辑**: 遍历 `Scene.UnitComponent.Children` → 筛选敌对类型（Monster→Hero） → 距离检测（SightRange） → 最近目标设为主目标

### 2.3 AIDispatcherComponent — 行为分发器

**路径**: `Model/Client/Code/Module/AI/AIDispatcherComponent.cs`

```csharp
[ComponentOf(typeof(Scene))]
public sealed partial class AIDispatcherComponent : Entity, IAwake, IDestroy
{
    public Dictionary<int, AIHandler> AIHandlers { get; set; }
    // Key = Handler ID, Value = Handler 实例
}
```

### 2.4 AIHandler — 抽象基类

**路径**: `Model/Client/Code/Module/AI/AIHandler.cs`

```csharp
public abstract class AIHandler
{
    public abstract ETTask Execute(AIComponent ai, ETCancellationToken cancellationToken);
}
```

---

## 三、Handler 行为节点

### 预定义 Handler ID

| ID | 名称 | 类名 | 职责 |
|----|------|------|------|
| 1 | Idle | `IdleHandler` | 待机等待 PatrolWaitTime |
| 2 | Patrol | `PatrolHandler` | 向左推进 + 随机Y偏移 |
| 3 | Chase | `ChaseHandler` | 追向主目标（最多10步） |
| 4 | Attack | `AttackHandler` | 播放攻击动画（伤害待战斗系统） |
| 5 | ReturnToSpawn | `ReturnToSpawnHandler` | FlashTo 回出生点 |

### Handler 文件位置

**路径**: `Hotfix/Client/Code/Module/AI/Handler/`

| 文件 | 行数 |
|------|------|
| `IdleHandler.cs` | 25 |
| `PatrolHandler.cs` | 48 |
| `ChaseHandler.cs` | 79 |
| `AttackHandler.cs` | 40 |
| `ReturnToSpawnHandler.cs` | 34 |

---

## 四、AI 主循环（固定顺序）

**路径**: `Hotfix/Client/Code/Module/AI/AIComponentSystem.cs`

```
while (!self.IsDisposed)
{
    if (self.Paused) → 等待 CheckInterval 后继续
    
    获取 dispatcher.AIHandlers[self.CurrentHandler]
    await handler.Execute(self, token)
    
    HandlerIndex = (HandlerIndex + 1) % HandlerIds.Length
    CurrentHandler = HandlerIds[HandlerIndex]
    
    await timer.WaitAsync(CheckInterval, token)
}
```

**控制方法**:
- `StopAI()` — 取消令牌 + 标记销毁
- `PauseAI()` — 设置 Paused + 取消当前行为
- `ResumeAI()` — 取消暂停

---

## 五、AIConfig 配置表

### 格式说明

ET 导表规范：
- 第1行：注释/说明（生成 XML doc）
- 第2行：字段名（C# 属性名）
- 第3行：类型（int, string, int[], bool）
- 第4行+：数据
- **所有浮点数用 int/100 存储**（导表不支持 float）

### 字段定义

| 字段名 | 类型 | 说明 | 示例 |
|--------|------|------|------|
| Id | int | 配置ID | 1001 |
| AIName | string | AI名称描述 | "野狼·基础AI" |
| HandlerIds | int[] | 行为序列 | 1,2,3,4,5 |
| CheckInterval | int | 决策间隔(ms) | 500 |
| SightRange | int | 视野范围/100 | 800 → 8.0 |
| EnablePatrol | bool | 是否推进 | true |
| PatrolRadius | int | 推进距离/100 | 400 → 4.0 |
| PatrolWaitTime | int | 推进等待(ms) | 2000 |
| ChaseMaxDistance | int | 最大追击距离/100 | 1200 → 12.0 |
| AttackRange | int | 攻击距离/100 | 150 → 1.5 |

### 示例数据

| Id | AIName | HandlerIds | SightRange | PatrolRadius | AttackRange |
|----|--------|------------|------------|--------------|-------------|
| 1001 | 野狼·基础AI | 1,2,3,4,5 | 800 | 400 | 150 |
| 2001 | 巡逻精英 | 1,2,3,4,5 | 1200 | 600 | 200 |
| 3001 | 弓箭手·远程AI | 1,2,3,4,5 | 1500 | 0 | 500 |

**配置表路径**: `ET/Unity/Assets/Config/Excel/AIConfig.xlsx`
**生成代码路径**: `ET/Unity/Assets/Scripts/Model/Generate/ClientServer/Config/AIConfig.cs`

---

## 六、现有组件增强

### MonsterComponent

**路径**: `Model/Client/Code/Component/TeamGame/MonsterComponent.cs`

新增字段：
```csharp
/// <summary>关联的 AI 配置 ID（对应 AIConfig 表）</summary>
public int AIConfigId { get; set; }
```

### MonsterComponentSystem.InitFromConfig

**路径**: `Hotfix/Client/Code/System/TeamGame/MonsterComponentSystem.cs`

主要逻辑：
1. 根据 configId 查找 AIConfigId（简化策略：直接使用 configId 或默认 1001）
2. 挂载 `AIComponent` + `PerceptionComponent` 到 Unit
3. 同步感知参数（SightRange）

---

## 七、目录结构

```
ET/Unity/Assets/Scripts/
├── Model/Client/Code/Module/AI/
│   ├── AIComponent.cs              — AI大脑组件
│   ├── AIHandler.cs                — Handler抽象基类
│   ├── AIDispatcherComponent.cs    — 行为分发器
│   └── PerceptionComponent.cs      — 感知组件
│
├── Hotfix/Client/Code/Module/AI/
│   ├── AIComponentSystem.cs        — AI主循环 + Pause/Resume/Stop
│   ├── AIDispatcherComponentSystem.cs — 注册5个Handler
│   ├── PerceptionComponentSystem.cs   — 扫描敌对目标
│   └── Handler/
│       ├── IdleHandler.cs          — 待机
│       ├── PatrolHandler.cs        — 推进
│       ├── ChaseHandler.cs         — 追击
│       ├── AttackHandler.cs        — 攻击（播动画）
│       └── ReturnToSpawnHandler.cs — 返回出生点
│
├── Model/Client/Code/Component/TeamGame/
│   └── MonsterComponent.cs         — [增强] 加 AIConfigId
│
├── Hotfix/Client/Code/System/TeamGame/
│   └── MonsterComponentSystem.cs   — [增强] InitFromConfig 挂载AI
│
├── Model/Generate/ClientServer/Config/
│   └── AIConfig.cs                 — [导表生成] AI配置
│
└── Unity/Assets/Config/Excel/
    └── AIConfig.xlsx               — AI配置表源文件
```

---

## 八、设计决策记录

| 决策 | 选型 | 理由 |
|------|------|------|
| 架构 | 四层分层（决策→行为→感知→能力） | 职责清晰，松耦合 |
| 行为引擎 | Handler链式（非行为树） | 轻量、单一职责、配置驱动 |
| 循环模式 | Phase1固定顺序，Phase2状态驱动 | 先跑通再优化 |
| 服务端 | 暂不考虑 | 客户端先行 |
| 目标存储 | `List<long>`（存Unit ID） | 避免引用悬挂，ID稳定 |
| 暂停机制 | `Paused` 字段 | 支持眩晕/冰冻 |
| 浮点精度 | int/100 | 导表不支持float |
| TimerComponent | `self.Root().GetComponent<TimerComponent>()` | ET框架规范，非单例 |

---

## 九、已知问题

| # | 问题 | 影响 | 计划解决 |
|---|------|------|---------|
| 1 | 视图层不存在（ECS Unit ↔ GameObject 无同步） | 屏幕上看不到怪物移动 | Phase 1b |
| 2 | 客户端 UnitFactory 未创建 | 无法生成怪物实体 | Phase 1b |
| 3 | MoveToAsync 使用单点路径 | 移动不够平滑 | Phase 2 |
| 4 | AttackHandler 伤害逻辑为空 | 攻击无实际效果 | 等战斗系统 |
| 5 | 感知依赖 IdentityComponent | 英雄需挂载该组件 | 创建时确认 |

---

## 十、后续规划

| Phase | 内容 | 前置条件 |
|-------|------|---------|
| 1b | 客户端 UnitFactory + 视图层同步 | 当前 Phase1 稳定 |
| 2 | 状态驱动跳转（Check逻辑）+ 战斗/技能/Buff系统 | Phase1 稳定 |
| 3 | 扩展 Handler（Flee / Special / Summon） | 有新怪物类型需求 |
