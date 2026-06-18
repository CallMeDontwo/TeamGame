using ET;

namespace FairyGUI
{
    public static class TransitionExtension
    {
        public static ETTask PlayAsync(this Transition transition, ETCancellationToken token = null)
        {
            ETTask tcs = ETTask.Create(true);
            token?.Add(Cancel);
            transition.Play(() => tcs.SetResult());
            return tcs;

            void Cancel()
            {
                transition.Stop();
            }
        }

        public static ETTask PlayReverseAsync(this Transition transition, ETCancellationToken token = null)
        {
            ETTask tcs = ETTask.Create();
            token?.Add(Cancel);
            transition.PlayReverse(() => tcs.SetResult());
            return tcs;

            void Cancel()
            {
                transition.Stop();
            }
        }

    }
}