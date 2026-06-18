using System;
using System.Collections.Generic;

namespace ET
{
    [Code]
    public class SceneCreatDispatcher : Singleton<SceneCreatDispatcher>, ISingletonAwake
    {
        private readonly Dictionary<string, List<ASceneCreater>> creaters = new Dictionary<string, List<ASceneCreater>>();

        private sealed class Comparer : Object, IComparer<ASceneCreater>
        {
            public int Compare(ASceneCreater x, ASceneCreater y) => x.GetOrder().CompareTo(y.GetOrder());
        }

        public void Awake()
        {
            Comparer comparer = new Comparer();
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(SceneCreatAttribute));
            foreach (Type type in types)
            {
                ASceneCreater creater = Activator.CreateInstance(type) as ASceneCreater;
                if (string.IsNullOrEmpty(creater.GetSceneName()))
                {
                    continue;
                }
                if (!this.creaters.TryGetValue(creater.GetSceneName(), out var list))
                {
                    list = new List<ASceneCreater>();
                    this.creaters.Add(creater.GetSceneName(), list);
                }
                list.Add(creater);
            }
            this.creaters.Values.Foreach(item => item.Sort(comparer));
        }

        public async ETTask<bool> TryCreate(Scene parent, SceneArguments args)
        {
            try
            {
                if (this.creaters.TryGetValue(args.Name, out List<ASceneCreater> creater))
                {
                    bool result = true;
                    for (int i = 0; i < creater.Count; i++)
                    {
                        bool res = await creater[i].TryCreate(parent, args);
                        result = res && result;
                    }
                    return result;
                }
                throw new Exception($"不存在场景{args.Name}的Handle");

            }
            catch (Exception e)
            {
                Log.Error(e);
                return false;
            }
        }

        public async ETTask OnCreate(Scene parent, Scene scene, SceneArguments args)
        {
            try
            {
                if (this.creaters.TryGetValue(scene.Name, out List<ASceneCreater> creater))
                {
                    for (int i = 0; i < creater.Count; i++)
                    {
                        await creater[i].OnCreate(parent, scene, args);
                    }
                    return;
                }
                throw new Exception($"不存在场景{scene.Name}的Handle");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public async ETTask OnCreateComplete(Scene parent, Scene scene, SceneArguments args)
        {
            try
            {
                if (this.creaters.TryGetValue(scene.Name, out List<ASceneCreater> creater))
                {
                    for (int i = 0; i < creater.Count; i++)
                    {
                        await creater[i].OnCreateComplete(parent, scene, args);
                    }
                    return;
                }
                throw new Exception($"不存在场景{scene.Name}的Handle");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public async ETTask OnDestory(Scene parent, Scene scene, SceneArguments args)
        {
            try
            {
                if (this.creaters.TryGetValue(scene.Name, out List<ASceneCreater> creater))
                {
                    for (int i = creater.Count - 1; i > 0; i--)
                    {
                        await creater[i].OnDestroy(parent, scene, args);
                    }
                    return;
                }
                throw new Exception($"不存在场景{scene.Name}的Handle");
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}