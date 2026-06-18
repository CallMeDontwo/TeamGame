using System.Collections;
#if ENABLE_IL2CPP
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HybridCLR;
#endif

namespace ET
{
    internal sealed class AotLoader
    {
        public static readonly AotLoader Instance = new AotLoader();

        public ETTask LoadAsync()
        {
            ETTask task = ETTask.Create(true);
            Global.Coroutine(this.LoadAsync(task));
            return task;
        }

        public IEnumerator LoadAsync(ETTask task)
        {
#if ENABLE_IL2CPP
            TextAsset txtAsset = Resources.Load<TextAsset>("Assemblies/assemblies");
            List<string> assemblies = txtAsset.text.Split(';').ToList();
            Resources.UnloadAsset(txtAsset);
            List<ResourceRequest> list = new List<ResourceRequest>();
            assemblies.ForEach(item => list.Add(Resources.LoadAsync<TextAsset>($"Assemblies/{item}")));
            while (list.Count > 0)
            {
                for (int i = list.Count - 1; i >= 0; i--)
                {
                    if (list[i].isDone)
                    {
                        TextAsset dllAsset = list[i].asset as TextAsset;
                        LoadImageErrorCode err = RuntimeApi.LoadMetadataForAOTAssembly(dllAsset.bytes, HomologousImageMode.SuperSet);
                        Log.Info($"LoadMetadataForAOTAssembly:{dllAsset.name}.  ret:{err}");
                        Resources.UnloadAsset(list[i].asset);
                        list.RemoveAt(i);
                    }
                }
                yield return null;
            }
#endif
            task.SetResult();
            yield break;
        }
    }
}