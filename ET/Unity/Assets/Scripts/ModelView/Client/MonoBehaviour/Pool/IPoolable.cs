namespace ET
{
    /// <summary>
    /// 池化回调接口 — 挂载在 GameObject 上的组件实现此接口,在取/还时执行自定义逻辑
    /// </summary>
    public interface IPoolable
    {
        /// <summary>从池中取出时调用（激活前）</summary>
        void OnSpawn();

        /// <summary>回收到池时调用（停用前）</summary>
        void OnRecycle();
    }
}
