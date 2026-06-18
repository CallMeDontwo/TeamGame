using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public enum CodeMode
    {
        Client = 1,
        Server = 2,
        ClientServer = 3,
    }

    public enum BuildType
    {
        Debug,
        Release,
    }

    public enum EPlayMode
    {
        OfflinePlayMode,
        HostPlayMode,
        WebPlayMode,
    }

    [CreateAssetMenu(menuName = "ET/CreateGlobalConfig", fileName = "GlobalConfig", order = 0)]
    public class GlobalConfig : ScriptableObject
    {
        public AppType AppType;

        public BuildType BuildType;

        public CodeMode CodeMode;

        public bool EnableDll;

        public bool SimulateMode;

        public EPlayMode EPlayMode;

        public List<string> environments = new List<string>();

        public int Environment = 0;

        public string GetCurrentEnvironment() => this.environments[this.Environment];
    }
}