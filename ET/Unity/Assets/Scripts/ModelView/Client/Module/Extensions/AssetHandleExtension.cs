using YooAsset;

namespace ET
{
    public static class AssetHandleExtension
    {
        public static async ETTask<AssetHandle> WaitAsync(this AssetHandle handle)
        {
            await handle.Task;
            return handle;
        }

        public static void SafeDispose(this AssetHandle handle)
        {
            if (handle != null && handle.IsValid)
            {
                handle.Dispose();
            }
        }

        public static void SafeRelease(this AssetHandle handle)
        {
            handle.SafeDispose();
        }
    }
}