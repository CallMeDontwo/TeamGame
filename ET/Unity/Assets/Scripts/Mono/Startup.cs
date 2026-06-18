using UnityEngine;

namespace ET
{
    internal static class Startup
    {
        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        public static void Start()
        {
            new GameObject("Global").AddComponent<Global>().gameObject.AddComponent<ReferenceTypes>();
        }
    }
}