using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ET
{
    public static class ETTaskHelper
    {
        public static bool IsCancel(this ETCancellationToken self)
        {
            if (self == null)
            {
                return false;
            }
            return self.IsDispose();
        }

        private class CoroutineBlocker : IDisposable
        {
            private static readonly ConcurrentQueue<CoroutineBlocker> pool = new ConcurrentQueue<CoroutineBlocker>();

            private int count;

            private ETTask tcs;

            public async ETTask RunSubCoroutineAsync(ETTask task)
            {
                try
                {
                    await task;
                }
                finally
                {
                    --this.count;

                    if (this.count <= 0 && this.tcs != null)
                    {
                        ETTask t = this.tcs;
                        this.tcs = null;
                        t.SetResult();
                    }
                }
            }

            public async ETTask WaitAsync()
            {
                if (this.count <= 0)
                {
                    return;
                }
                this.tcs = ETTask.Create(true);
                await this.tcs;
            }

            public void Dispose()
            {
                this.count = 0;
                this.tcs = null;
                pool.Enqueue(this);
            }

            public static CoroutineBlocker Fetch(int count)
            {
                CoroutineBlocker blocker = pool.TryDequeue(out CoroutineBlocker b) ? b : new CoroutineBlocker();
                blocker.count = count;
                return blocker;
            }
        }

        public static async ETTask WaitAny(ICollection<ETTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(1);

            foreach (ETTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAny(ETTask task1, ETTask task2)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(1);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAny(ETTask task1, ETTask task2, ETTask task3)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(1);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAny(ETTask task1, ETTask task2, ETTask task3, ETTask task4)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(1);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task4).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAny(ETTask task1, ETTask task2, ETTask task3, ETTask task4, ETTask task5)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(1);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task4).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task5).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAll(ICollection<ETTask> tasks)
        {
            if (tasks.Count == 0)
            {
                return;
            }

            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(tasks.Count);

            foreach (ETTask task in tasks)
            {
                coroutineBlocker.RunSubCoroutineAsync(task).Coroutine();
            }

            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAll(ETTask task1, ETTask task2)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(2);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAll(ETTask task1, ETTask task2, ETTask task3)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(3);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAll(ETTask task1, ETTask task2, ETTask task3, ETTask task4)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(4);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task4).Coroutine();
            await coroutineBlocker.WaitAsync();
        }

        public static async ETTask WaitAll(ETTask task1, ETTask task2, ETTask task3, ETTask task4, ETTask task5)
        {
            using CoroutineBlocker coroutineBlocker = CoroutineBlocker.Fetch(5);
            coroutineBlocker.RunSubCoroutineAsync(task1).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task2).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task3).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task4).Coroutine();
            coroutineBlocker.RunSubCoroutineAsync(task5).Coroutine();
            await coroutineBlocker.WaitAsync();
        }
    }
}