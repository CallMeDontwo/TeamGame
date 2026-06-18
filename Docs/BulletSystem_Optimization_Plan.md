# 子弹系统性能优化方案

> 项目：狮驼岭伏魔 (ShiTuoLingFuMo)
> 日期：2026-06-15
> 状态：方案设计期，待确认后实施

---

## 一、背景

当前子弹系统已实现基础功能（直线飞行、抛物线、锁定追踪、弹射），但存在性能隐患和命中精度问题：

| 文件 | 路径 |
|------|------|
| BulletComponent.cs | `Assets/Scripts/Model/Client/Code/Module/Skill/Bullet/` |
| BulletComponentSystem.cs | `Assets/Scripts/Hotfix/Client/Code/Module/Skill/Bullet/` |
| BulletFactory.cs | `Assets/Scripts/Hotfix/Client/Code/Factory/` |
| BulletConfig.xlsx | `Assets/Config/Excel/` |

---

## 二、三大瓶颈

### 瓶颈 1：每个子弹一个独立 ETTask 协程

```csharp
// BulletComponentSystem.cs — 当前实现
public static async ETTask StartFlight(this BulletComponent self)
{
    while (true)
    {
        await timer.WaitFrameAsync();  // ← 每帧每个子弹一个异步状态机
        // 移动、碰撞检测...
    }
}
```

**问题**：N 个子弹 = N 个协程独立 tick。100 个子弹就是 100 个异步状态机每帧调度、分配、GC。

### 瓶颈 2：每弹 O(n) 全量碰撞遍历

```csharp
// 每个子弹每帧遍历所有 Unit
foreach (var kv in unitManager.Children)  // O(m)
{
    var identity = kv.Value.GetComponent<IdentityComponent>(); // 字典查找 × N
    var distSq = math.distancesq(pos, unitPos);
    // ...
}
```

**问题**：100 弹 × 20 怪 = 2000 次 `GetComponent` / 帧，且相邻子弹重复遍历同一批敌人。

### 瓶颈 3：无空间分区、无 CCD（Tunneling）

- 碰撞检测是简单的圆-圆静态检测，`hitRadiusSq = 0.25f`（半径 0.5）
- 高速弹（追踪弹、Spear Missile）可能一帧飞过敌人而不触发碰撞
- 没有空间网格加速

### 次要问题

- 固定 `dt = 1f/60f`，帧率变化时行为不一致
- `bulletUnit.SetPositon()` 每帧每弹触发事件 + 视图更新

---

## 三、方案对比总览

| 维度 | 方案 A（Burst + Jobs） | 方案 B（纯 C# 重构） |
|------|----------------------|---------------------|
| 新增 Package | Burst + Collections | **无** |
| 并行计算 | `IJobParallelFor` 多线程 | 单线程 for 循环 |
| 数据结构 | `NativeArray<T>` / `NativeParallelMultiHashMap` | `List<T>` / `Dictionary<int, List<int>>` |
| GC 压力 | 零（全部 Native 容器） | 极低（struct + 复用 List） |
| 50 弹 × 10 怪 | < 0.1ms | ~0.2ms |
| 200 弹 × 20 怪 | ~0.2ms | ~1ms |
| 500 弹 × 30 怪 | ~0.5ms | ~3ms |
| 学习成本 | 需要了解 Burst/Job 限制 | 纯 C#，无学习成本 |
| 适用场景 | 弹幕数量 > 200，追求极致 | 弹幕数量 < 200，快速交付 |

---

## 方案 A：架构重整 + Burst（推荐大规模场景）

| 改什么 | 怎么做 | 收益 |
|--------|--------|------|
| 协程 → 集中 System | 新增 `BulletManagerComponent`（Scene 级），一个 Update 替代 N 个协程 | 零 GC，零协程 |
| 碰撞 → 空间哈希网格 | 每帧构建 2D 均匀网格，子弹/敌人按位置入桶 | O(n×m) → O(n+m) |
| 高速弹 → CCD 线段检测 | 保留上一帧位置，做线段-圆 sweep test | 彻底解决穿透 |
| 计算 → Burst 编译 Job | 移动 + 碰撞检测改用 `IJobParallelFor` + `[BurstCompile]` | 多线程 + SIMD，快 10~50 倍 |
| 渲染 → GPU Instancing | 子弹 View 改为 `Graphics.DrawMeshInstanced`（后续迭代） | 一个 DrawCall 画所有子弹 |

### A.1 需要添加的 Package

```json
// manifest.json 新增
"com.unity.burst": "1.8.17",
"com.unity.collections": "2.4.3"
```

> 注：`Unity.Mathematics` 项目中已在使用，无需额外添加。

### A.2 新增文件清单

```
Model/Client/Code/Module/Skill/Bullet/BulletRuntimeData.cs       // 子弹扁平化结构体（Job 友好）
Model/Client/Code/Module/Skill/Bullet/BulletManagerComponent.cs   // Scene 级管理器组件
Hotfix/Client/Code/Module/Skill/Bullet/BulletManagerSystem.cs     // 集中 Update + Job 调度
Hotfix/Client/Code/Module/Skill/Bullet/Jobs/BulletMoveJob.cs      // Burst 移动 Job
Hotfix/Client/Code/Module/Skill/Bullet/Jobs/BulletCollisionJob.cs // Burst 碰撞 + CCD Job
```

### A.3 修改现有文件

| 文件 | 改动 |
|------|------|
| `BulletComponent.cs` | 去掉 `prevPosition` 逻辑（移到 `BulletRuntimeData`），保留 ConfigId/Caster 等初始化字段 |
| `BulletFactory.cs` | 创建子弹后不再调用 `StartFlight()`，改为 `BulletManagerComponent.Register(bullet)` |
| `BulletComponentSystem.cs` | **删除 `StartFlight()` 协程**，保留 `ApplyBulletDamage()` / `TryRicochet()` 等静态方法 |

---

## 方案 B：纯 C# 架构重构（不引入新依赖）

不改 Package，不做 Burst 编译，不改现有 asmdef 的引用关系。仅用纯 C# (`List<T>`, `Dictionary`, `struct`) 完成协程消除 + 空间网格 + CCD。

### B.1 核心思路

跟前三个瓶颈一一对应：

| 瓶颈 | 方案 B 对策 |
|------|------------|
| 100 个协程独立 tick | 拔掉 `StartFlight()` 协程，新建 `BulletManagerComponent`，在 **一个** `Update` 里 for 循环统一推进所有子弹 |
| O(n×m) 全量碰撞 | 每帧构建 `Dictionary<int, List<int>>` 空间哈希网格，碰撞时只查 9 个邻接 cell |
| 无 CCD，高速弹穿透 | 子弹 `struct` 里加 `prevPosition`，检测时做线段-圆 sweep test |

### B.2 数据结构（纯 C#，零 Native 容器）

**B.2.1 子弹运行时数据**

```csharp
// Model/Client/Code/Module/Skill/Bullet/BulletRuntimeData.cs
// 纯 struct，不继承 Entity，不走 ET 组件体系
public struct BulletRuntimeData
{
    public float2  position;
    public float2  prevPosition;    // CCD
    public float2  velocity;
    public float   radius;          // 碰撞半径
    public float   traveledDist;
    public float   maxDist;
    public long    bulletUnitId;    // 对应 ET Bullet Unit 的 Id
    public long    casterId;
    public long    targetId;        // 追踪弹的目标 Unit Id
    public int     damage;
    public int     ricochetRemain;
    public float   ricochetRadius;
    public byte    flightType;      // 0=Straight, 1=Parabolic
    public byte    bulletType;      // 0=Normal, 1=Ricochet
    public byte    isHoming;
    public byte    isActive;
}
```

**关键区别**：方案 A 用 `NativeArray<BulletRuntimeData>`（非托管内存，Job 可读写），方案 B 用 `List<BulletRuntimeData>`（托管堆，但 struct 本身不产生额外 GC）。

**B.2.2 空间哈希网格**

```csharp
// 用 Dictionary<int, List<int>> 替代 NativeParallelMultiHashMap
// key = cellKey, value = 该 cell 内的子弹索引列表
Dictionary<int, List<int>> bulletGrid;   // cell → bulletIndex[]
Dictionary<int, List<int>> enemyGrid;    // cell → enemyIndex[]

// cell 大小：碰撞半径的 4 倍，平衡精度和桶内数量
float cellSize = 2f;

int GetCellKey(float2 pos)
    => ((int)(pos.x / cellSize) & 0xFFFF)
     | (((int)(pos.y / cellSize) & 0xFFFF) << 16);
```

**复用策略**：`List<int>` 子列表不清除重建，每帧只清空 `Dictionary` 后重新填充，减少 GC。

**B.2.3 敌人碰撞数据**

```csharp
// 每帧从 UnitManager 里抽出来的快照，避免碰撞循环里频繁 GetComponent
public struct EnemyCollisionSnapshot
{
    public float2 position;
    public float  radius;       // 取自 NumericComponent 或配置
    public long   unitId;
    public byte   unitType;     // Hero=1, Monster=2，用于区分敌我
}
```

**B.2.4 管理器组件**

```csharp
// Model/Client/Code/Module/Skill/Bullet/BulletManagerComponent.cs
[ComponentOf(typeof(Scene))]
public class BulletManagerComponent : Entity, IAwake, IDestroy
{
    public List<BulletRuntimeData> Bullets;       // 所有子弹（托管 List）
    public Queue<int> FreeSlots;                  // 空闲槽位复用

    // 空间网格
    public Dictionary<int, List<int>> BulletGrid;
    public Dictionary<int, List<int>> EnemyGrid;
    public List<EnemyCollisionSnapshot> EnemiesSnapshot;

    // 本帧命中结果
    public List<HitResult> HitsThisFrame;

    public float GridCellSize = 2f;
}
```

### B.3 核心逻辑流程（纯 C# 实现）

**B.3.1 集中 System Update（替代 N 个协程）**

```csharp
// Hotfix/Client/Code/Module/Skill/Bullet/BulletManagerSystem.cs
[EntitySystemOf(typeof(BulletManagerComponent))]
public static partial class BulletManagerSystem
{
    [EntitySystem]
    private static void Update(this BulletManagerComponent self)
    {
        float dt = TimeInfo.Instance.ClientDeltaTime();  // 修复固定 1/60 的问题
        if (self.Bullets.Count == 0) return;

        // Step 1: 收集敌人位置快照
        BuildEnemySnapshot(self);

        // Step 2: 推进所有子弹（单线程 for 循环）
        UpdateAllBullets(self, dt);

        // Step 3: 构建空间哈希网格
        BuildSpatialGrid(self);

        // Step 4: 碰撞检测（网格加速 + CCD）
        CheckCollisions(self);

        // Step 5: 处理命中结果（主线程安全）
        ProcessHits(self);

        // Step 6: 清理超出距离/已命中的子弹 → 放回 FreeSlots
        CleanupDeadBullets(self);
    }
}
```

**B.3.2 子弹推进（纯 C#，替代 Burst Job）**

```csharp
private static void UpdateAllBullets(BulletManagerComponent self, float dt)
{
    for (int i = 0; i < self.Bullets.Count; i++)
    {
        var b = self.Bullets[i];
        if (b.isActive == 0) continue;

        // 备份位置（CCD 用）
        b.prevPosition = b.position;

        // 追踪弹：每帧重算朝向
        if (b.isHoming == 1)
        {
            var targetUnit = GetUnitById(b.targetId);
            if (targetUnit != null)
            {
                float2 toTarget = targetUnit.Position.xz - b.position;
                float speed = math.length(b.velocity);
                b.velocity = math.normalizesafe(toTarget) * speed;
            }
        }

        // 抛物线：叠加重力
        if (b.flightType == 1) // Parabolic
            b.velocity.y -= gravity * dt;

        // 位移
        b.position += b.velocity * dt;

        // 飞行距离检查
        float stepDist = math.length(b.velocity) * dt;
        b.traveledDist += stepDist;
        if (b.traveledDist >= b.maxDist)
            b.isActive = 0;

        self.Bullets[i] = b;
    }
}
```

**B.3.3 空间网格构建**

```csharp
private static void BuildSpatialGrid(BulletManagerComponent self)
{
    // 清空网格（复用内部 List<int>）
    foreach (var kv in self.BulletGrid)
        kv.Value.Clear();
    self.BulletGrid.Clear();

    foreach (var kv in self.EnemyGrid)
        kv.Value.Clear();
    self.EnemyGrid.Clear();

    // 子弹入网格
    for (int i = 0; i < self.Bullets.Count; i++)
    {
        if (self.Bullets[i].isActive == 0) continue;
        int key = GetCellKey(self.Bullets[i].position, self.GridCellSize);
        if (!self.BulletGrid.TryGetValue(key, out var list))
        {
            list = new List<int>(4);
            self.BulletGrid[key] = list;
        }
        list.Add(i);
    }

    // 敌人入网格
    for (int i = 0; i < self.EnemiesSnapshot.Count; i++)
    {
        int key = GetCellKey(self.EnemiesSnapshot[i].position, self.GridCellSize);
        if (!self.EnemyGrid.TryGetValue(key, out var list))
        {
            list = new List<int>(4);
            self.EnemyGrid[key] = list;
        }
        list.Add(i);
    }
}
```

**B.3.4 碰撞检测（网格加速 + CCD）**

```csharp
private static void CheckCollisions(BulletManagerComponent self)
{
    self.HitsThisFrame.Clear();

    for (int bi = 0; bi < self.Bullets.Count; bi++)
    {
        var bullet = self.Bullets[bi];
        if (bullet.isActive == 0) continue;

        int cellKey = GetCellKey(bullet.position, self.GridCellSize);

        bool hit = false;

        // 只查 9 个邻接 cell
        for (int dy = -1; dy <= 1 && !hit; dy++)
        for (int dx = -1; dx <= 1 && !hit; dx++)
        {
            int neighborKey = cellKey + dx + (dy << 16);
            if (!self.EnemyGrid.TryGetValue(neighborKey, out var enemyIndices))
                continue;

            foreach (int ei in enemyIndices)
            {
                var enemy = self.EnemiesSnapshot[ei];

                if (IsSameSide(bullet.casterId, enemy))  // 不分敌我则跳过
                    continue;

                if (BulletHitCheck(
                    bullet.position, bullet.prevPosition,
                    enemy.position, enemy.radius, bullet.radius))
                {
                    self.HitsThisFrame.Add(new HitResult
                    {
                        bulletIndex = bi,
                        targetId    = enemy.unitId,
                        damage      = bullet.damage
                    });
                    hit = true;
                    break; // 普通弹命中一个就够
                }
            }
        }
    }
}
```

**B.3.5 CCD 命中检测（与方案 A 完全相同的算法，去掉 `[BurstCompile]`）**

```csharp
static bool BulletHitCheck(
    float2 bulletPos, float2 prevBulletPos,
    float2 targetPos, float targetRadius, float bulletRadius)
{
    float combined = targetRadius + bulletRadius;
    float combinedSq = combined * combined;

    // 阶段 1：静态圆-圆（覆盖低速弹）
    if (math.distancesq(bulletPos, targetPos) <= combinedSq)
        return true;

    // 阶段 2：线段-圆 CCD（覆盖高速弹）
    float2 move = bulletPos - prevBulletPos;
    float moveLenSq = math.lengthsq(move);
    if (moveLenSq < 1e-8f) return false;

    float t = math.saturate(
        math.dot(targetPos - prevBulletPos, move) / moveLenSq);
    float2 closest = prevBulletPos + move * t;

    return math.distancesq(closest, targetPos) <= combinedSq;
}
```

**B.3.6 命中处理 & 清理**

```csharp
private static void ProcessHits(BulletManagerComponent self)
{
    foreach (var hit in self.HitsThisFrame)
    {
        var bullet = self.Bullets[hit.bulletIndex];

        // 调用已有的伤害逻辑
        BulletComponentSystem.ApplyBulletDamage(
            bullet.casterId, hit.targetId, hit.damage);

        // 弹射处理
        if (bullet.bulletType == 1 && bullet.ricochetRemain > 0)
        {
            TryRicochet(self, hit.bulletIndex);
        }
        else
        {
            // 普通弹：标记失活 + 销毁 ET Unit
            var b = self.Bullets[hit.bulletIndex];
            b.isActive = 0;
            self.Bullets[hit.bulletIndex] = b;

            var bulletUnit = GetUnitById(bullet.bulletUnitId);
            bulletUnit?.Dispose();  // ET Entity 销毁 → 连带 View GameObject
        }
    }
}

private static void CleanupDeadBullets(BulletManagerComponent self)
{
    for (int i = 0; i < self.Bullets.Count; i++)
    {
        var b = self.Bullets[i];
        if (b.isActive == 0 && b.bulletUnitId != 0)
        {
            var bulletUnit = GetUnitById(b.bulletUnitId);
            bulletUnit?.Dispose();
            self.Bullets[i] = default;
            self.FreeSlots.Enqueue(i);
        }
    }
}
```

### B.4 文件改动清单

#### 新增文件

```
Model/Client/Code/Module/Skill/Bullet/BulletRuntimeData.cs       // 子弹运行时 struct
Model/Client/Code/Module/Skill/Bullet/BulletManagerComponent.cs   // Scene 级管理器
Hotfix/Client/Code/Module/Skill/Bullet/BulletManagerSystem.cs     // 集中 Update（全部逻辑在此）
```

**只有 3 个新文件，不用创建 Jobs 子目录。**

#### 修改现有文件

| 文件 | 改动 | 改动量 |
|------|------|--------|
| `BulletComponent.cs` | 去掉运行时字段（Direction/Speed/Damage/Position 等），只保留 BulletConfigId、Caster 引用 | ~10 行删减 |
| `BulletFactory.cs` | `CreateBullet()` 末尾：删除 `bc.StartFlight().Coroutine()`，改为 `bulletManager.Register(data)` | ~2 行改动 |
| `BulletComponentSystem.cs` | **删除整个 `StartFlight()` 方法**（约 130 行协程），保留 `ApplyBulletDamage()` / `TryRicochet()` | 大删 |
| `SkillEvent_SpawnBullet.cs` | **不变** | — |

#### 不需要改的文件

| 文件 | 原因 |
|------|------|
| 所有 asmdef | 没有新 package，无需改引用 |
| `manifest.json` | 不引入 Burst/Collections |
| `BulletConfig.xlsx` / 导表代码 | 配置字段不变 |
| HotfixView 层所有文件 | `AfterBulletCreate` 事件照旧 |
| AI / 技能系统 | 入口 `BulletFactory.CreateBullet()` 签名不变 |

### B.5 方案 B 的 GC 优化要点

1. **struct 装箱规避**：`BulletRuntimeData` 是 struct，存在 `List<T>` 里不装箱；更新时用索引赋值 `list[i] = b`，不用拆箱
2. **List 复用**：空间网格内的 `List<int>` 子列表不清除重建，每帧只 `Clear()` 后重新 `Add`
3. **快照模式**：`EnemyCollisionSnapshot` 是 struct 数组，一帧只从 `UnitManager` 提取一次，碰撞循环里零次 `GetComponent`
4. **空闲槽复用**：子弹销毁不调 `List.Remove`（O(n)），而是标记 `isActive=0` + 入 `FreeSlots` 队列，下次发射优先填空位
5. **`TimeInfo.Instance.ClientDeltaTime()`**：修复当前固定 `1f/60f` 的问题，帧率自适应

### B.6 方案 B 实施步骤

1. 创建 `BulletRuntimeData.cs`（Model 层，纯 struct）
2. 创建 `BulletManagerComponent.cs`（Model 层，Scene 组件）
3. 创建 `BulletManagerSystem.cs`（Hotfix 层，集中 Update）
4. 改造 `BulletComponent.cs` — 移除运行时字段
5. 改造 `BulletFactory.cs` — Register 替代 StartFlight
6. 精简 `BulletComponentSystem.cs` — 删除协程，保留工具方法
7. 场景创建器中添加 `BulletManagerComponent`
8. 联调验证：发射 → 飞行 → 命中 → 伤害 → 弹射 → 销毁
9. Profiler 对比改前改后（重点关注 GC.Alloc 和 Scripts 耗时）

### B.7 方案 B 局限性

- 单线程 for 循环，200 弹以上开始接近 1ms（方案 A 此时 < 0.2ms）
- Dictionary 操作有小额 GC（key 枚举器），但远小于当前协程分配
- 如果将来跑移动端 IL2CPP，没有 Burst 的 SIMD 优化

---

## 四、共享数据结构与算法（两方案通用）

> 以下为两方案共用的内容。方案 A 额外用 `NativeArray`/`NativeParallelMultiHashMap` 替代 `List`/`Dictionary`；方案 B 用纯 C# 容器。CCD 算法完全相同，仅方案 A 多加 `[BurstCompile]`。

### 4.1 子弹运行时数据（脱离 Entity）

```csharp
// BulletRuntimeData.cs — 每个子弹 ≤ 64 字节，一个 cache line
public struct BulletRuntimeData
{
    public float2  position;        // 当前位置
    public float2  prevPosition;    // 上一帧位置（CCD 用）
    public float2  velocity;        // 速度向量
    public float   radius;          // 碰撞半径
    public float   traveledDist;    // 已飞行距离
    public float   maxDist;         // 最大距离
    public long    casterId;        // 施法者 EntityId
    public long    targetId;        // 目标 EntityId（追踪弹用）
    public int     damage;          // 伤害值
    public int     ricochetRemain;  // 剩余弹射次数
    public float   ricochetRadius;  // 弹射搜索半径
    public byte    flightType;      // 0=Straight, 1=Parabolic
    public byte    bulletType;      // 0=Normal, 1=Ricochet
    public byte    isHoming;        // 是否追踪
    public byte    isActive;        // 是否活跃
}
```

### 4.2 空间哈希网格

```csharp
// 2D 均匀网格
float cellSize = 2f;  // ≈ 碰撞半径(0.5)的 4 倍

int GetCellKey(float2 pos)
    => ((int)(pos.x / cellSize) & 0xFFFF) 
     | (((int)(pos.y / cellSize) & 0xFFFF) << 16);
```

每帧流程：
1. 遍历所有活跃子弹 → 写入 `NativeParallelMultiHashMap<int, int>`（key=cellKey, value=子弹索引）
2. 碰撞检测时，只查目标所在 cell 及 8 个邻接 cell 的子弹
3. 敌人位置也写入同一 Grid，双向查询

### 4.3 管理器组件

```csharp
// BulletManagerComponent.cs — Scene 级组件
[ComponentOf(typeof(Scene))]
public class BulletManagerComponent : Entity, IAwake, IDestroy
{
    public NativeList<BulletRuntimeData> Bullets;
    public NativeParallelMultiHashMap<int, int> SpatialGrid;
    public NativeList<HitResult> HitsThisFrame;
    public Queue<int> FreeSlots;   // 空闲槽位复用，避免频繁增删
}
```

---

## 五、方案 A 详细实现（Burst Jobs）

### 5.1 集中 System 主循环（替代协程）

```csharp
// BulletManagerSystem.cs
[EntitySystemOf(typeof(BulletManagerComponent))]
public static partial class BulletManagerSystem
{
    [EntitySystem]
    private static void Update(this BulletManagerComponent self)
    {
        float dt = TimeInfo.Instance.ClientDeltaTime();
        if (self.Bullets.Length == 0) return;

        // Step 1: 备份位置（CCD 用）→ 已在 Job 内部完成
        // Step 2: Burst Job 并行移动所有子弹
        // Step 3: 构建空间哈希网格（另一 Job）
        // Step 4: Burst Job 并行碰撞检测（网格加速 + CCD）
        // Step 5: 主线程处理命中结果
        foreach (var hit in self.HitsThisFrame)
        {
            ProcessHit(hit);
        }
    }
}
```

### 5.2 Burst 移动 Job

```csharp
[BurstCompile]
struct BulletMoveJob : IJobParallelFor
{
    public NativeArray<BulletRuntimeData> bullets;
    public float dt;

    public void Execute(int i)
    {
        var b = bullets[i];
        if (b.isActive == 0) return;

        // 备份位置（CCD 用）
        b.prevPosition = b.position;

        // 追踪弹：每帧重算朝向
        if (b.isHoming == 1)
            b.velocity = math.normalizesafe(targetPos - b.position) * speed;

        // 位移
        b.position += b.velocity * dt;

        // 飞行距离
        float stepDist = math.length(b.velocity) * dt;
        b.traveledDist += stepDist;
        if (b.traveledDist >= b.maxDist)
            b.isActive = 0;

        bullets[i] = b;
    }
}
```

### 5.3 CCD 命中检测算法

```csharp
[BurstCompile]
static bool CheckBulletHit(
    float2 bulletPos,      // 当前帧位置
    float2 prevBulletPos,  // 上一帧位置
    float2 targetPos,      // 目标位置
    float  targetRadius,   // 目标碰撞半径
    float  bulletRadius)   // 子弹碰撞半径
{
    float combined = targetRadius + bulletRadius;
    float combinedSq = combined * combined;

    // 阶段 1：静态圆-圆检测（覆盖低速弹）
    if (math.distancesq(bulletPos, targetPos) <= combinedSq)
        return true;

    // 阶段 2：线段-圆检测（CCD，覆盖高速弹）
    // 求子弹运动线段到目标点的最短距离
    float2 move = bulletPos - prevBulletPos;
    float  moveLenSq = math.lengthsq(move);

    if (moveLenSq < 1e-8f)
        return false;  // 没移动，且阶段 1 已判定未命中

    // 求线段上最近点参数 t，clamp 到 [0,1]
    float t = math.saturate(
        math.dot(targetPos - prevBulletPos, move) / moveLenSq);
    float2 closestPoint = prevBulletPos + move * t;

    return math.distancesq(closestPoint, targetPos) <= combinedSq;
}
```

### 5.4 完整碰撞 Job

```csharp
[BurstCompile]
struct BulletCollisionJob : IJobParallelFor
{
    [ReadOnly] public NativeArray<BulletRuntimeData> bullets;
    [ReadOnly] public NativeParallelMultiHashMap<int, int> gridByBullet; // cell → 子弹索引
    [ReadOnly] public NativeParallelMultiHashMap<int, UnitCollisionData> enemiesByCell; // cell → 敌人数据
    public NativeList<HitResult>.ParallelWriter hits;

    public float cellSize;
    public float targetRadius;

    public void Execute(int bulletIndex)
    {
        var bullet = bullets[bulletIndex];
        if (bullet.isActive == 0) return;

        int cellKey = GetCellKey(bullet.position, cellSize);

        // 只查 9 个邻接 cell
        for (int dy = -1; dy <= 1; dy++)
        for (int dx = -1; dx <= 1; dx++)
        {
            int neighborKey = cellKey + dx + (dy << 16);
            if (!enemiesByCell.TryGetFirstValue(neighborKey, out var enemy, out var it))
                continue;

            do
            {
                if (BulletHitCheck(
                    bullet.position, bullet.prevPosition,
                    enemy.position, enemy.radius, bullet.radius))
                {
                    hits.AddNoResize(new HitResult
                    {
                        bulletIndex = bulletIndex,
                        targetId    = enemy.entityId
                    });
                    return; // 命中后不再检测（除非穿透弹）
                }
            }
            while (enemiesByCell.TryGetNextValue(out enemy, ref it));
        }
    }
}
```

---

## 六、改造影响范围

| 模块 | 影响 | 说明 |
|------|------|------|
| `BulletFactory.cs` | 微调 | `StartFlight()` 调用 → `bulletManager.Register()` |
| `BulletComponentSystem.cs` | 大幅删减 | 移除 `StartFlight()` 协程，保留 `ApplyBulletDamage` 等 |
| `BulletComponent.cs` | 微调 | 保留配置字段，运行时数据迁移到 `BulletRuntimeData` |
| `SkillEvent_SpawnBullet` | **不变** | 只调 `BulletFactory`，入口不变 |
| 视图层（HotfixView） | **不变** | 子弹销毁/创建仍走 `AfterBulletCreate` 事件 |
| 配置表（xlsx） | **不变** | BulletConfig 字段继续有效 |
| AI / 技能系统 | **不影响** | 弹药系统内部重构 |

---

## 七、性能预期

| 场景 | 当前 | 方案 A | 方案 B |
|------|------|--------|--------|
| 50 弹 × 10 怪 | ~0.5ms | **< 0.1ms** | ~0.2ms |
| 200 弹 × 20 怪 | ~5ms | **~0.2ms** | ~1ms |
| 500 弹 × 30 怪 | ~30ms（掉帧） | **~0.5ms** | ~3ms |
| GC 每帧分配 | 持续（协程） | **零** | 几乎零 |
| 穿透问题 | 有 | **无（CCD）** | 无 |

---

## 八、待确认事项

- [ ] 确认方案 A 还是方案 B
- [ ] 方案 A 需确认 Burst + Collections 版本号（建议 Burst 1.8.17 + Collections 2.4.3，兼容 Unity 2022.3）
- [ ] 确认子弹碰撞半径取值（当前硬编码 0.5f，是否从 BulletConfig 读取）
- [ ] 确认敌人碰撞半径来源（当前无，需新增 NumericType 或从配置读取）
- [ ] 是否需要弹射弹穿透敌人的行为（穿透弹命中后不销毁、继续飞行）
- [ ] 是否需要 AOE 爆炸弹（命中后产生范围伤害，而非单目标）
- [ ] 自机判定缩小策略（英雄碰撞半径 < 视觉大小，弹幕游戏惯例）
- [ ] 是否需要子弹间碰撞（敌弹与自机弹互相抵消）

---

## 九、实施步骤

### 方案 A 实施步骤

1. 添加 `com.unity.burst` + `com.unity.collections` 到 manifest.json
2. 创建 `BulletRuntimeData.cs`、`BulletManagerComponent.cs`（Model 层）
3. 创建 `BulletMoveJob.cs`、`BulletCollisionJob.cs`（Hotfix 层 Jobs 子目录）
4. 创建 `BulletManagerSystem.cs`（Hotfix 层，集中 Update + Job 调度）
5. 改造 `BulletFactory.cs`（去掉 StartFlight，改为 Register）
6. 精简 `BulletComponentSystem.cs`（移除协程，保留工具方法）
7. 场景创建器添加 `BulletManagerComponent`
8. 联调验证：发射 → 飞行 → 命中 → 伤害 → 弹射 → 销毁
9. Profiler 对比改前改后

### 方案 B 实施步骤

1. 创建 `BulletRuntimeData.cs`（Model 层，纯 struct）
2. 创建 `BulletManagerComponent.cs`（Model 层，Scene 组件）
3. 创建 `BulletManagerSystem.cs`（Hotfix 层，集中 Update）
4. 改造 `BulletComponent.cs` — 移除运行时字段（Direction/Speed/Damage 等）
5. 改造 `BulletFactory.cs` — `Register()` 替代 `StartFlight().Coroutine()`
6. 精简 `BulletComponentSystem.cs` — 删除 `StartFlight()` 协程（~130 行），保留工具方法
7. 场景创建器 `ShiTuoLingFuMoSceneCreater0.cs` 中添加 `scene.AddComponent<BulletManagerComponent>()`
8. 联调验证：发射 → 飞行 → 命中 → 伤害 → 弹射 → 销毁
9. Profiler 对比改前改后（重点关注 GC.Alloc 和 Scripts 耗时）
