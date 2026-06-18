using ET;

namespace FairyGUI
{
    public static class GTweenerExtension
    {
        public static ETTask PlayAsync(this GTweener tweener)
        {
            ETTask task = ETTask.Create(true);
            tweener.OnComplete(() => task.SetResult());
            return task;
        }
    }
}