using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [Invoke]
    public class GetAllConfigBytes : AInvokeHandler<ConfigLoader.GetAllConfigBytes, ETTask<Dictionary<Type, byte[]>>>
    {
        public override async ETTask<Dictionary<Type, byte[]>> Handle(ConfigLoader.GetAllConfigBytes args)
        {
            Dictionary<Type, byte[]> output = new Dictionary<Type, byte[]>();
            HashSet<Type> configTypes = CodeTypes.Instance.GetTypes(typeof(ConfigAttribute));

            if (Define.IsEditor)
            {
                string ct = "cs";
                GlobalConfig globalConfig = Resources.Load<GlobalConfig>("GlobalConfig");
                CodeMode codeMode = globalConfig.CodeMode;
                switch (codeMode)
                {
                    case CodeMode.Client:
                        ct = "c";
                        break;
                    case CodeMode.Server:
                        ct = "s";
                        break;
                    case CodeMode.ClientServer:
                        ct = "cs";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                using ListComponent<Task> tasks = ListComponent<Task>.Create();
                using HashSetComponent<string> startConfigs = HashSetComponent<string>.Create();
                startConfigs.Add("StartMachineConfigCategory");
                startConfigs.Add("StartProcessConfigCategory");
                startConfigs.Add("StartSceneConfigCategory");
                startConfigs.Add("StartZoneConfigCategory");
                foreach (Type configType in configTypes)
                {
                    string configFilePath = startConfigs.Contains(configType.Name)
                        ? $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configType.Name}.bytes"
                        : $"../Config/Excel/{ct}/{configType.Name}.bytes";
                    tasks.Add(this.LoadConfigAsyncByFile(configType, configFilePath, output));
                }
                await Task.WhenAll(tasks);
            }
            else
            {
                using ListComponent<ETTask> tasks = ListComponent<ETTask>.Create();
                foreach (Type type in configTypes)
                {
                    tasks.Add(this.LoadConfigAsyncByYooAsset(type, output));
                }
                await ETTaskHelper.WaitAll(tasks);
            }

            return output;
        }

        private async Task LoadConfigAsyncByFile(Type type, string path, Dictionary<Type, byte[]> output)
        {
            output[type] = await File.ReadAllBytesAsync(path);
        }

        private async ETTask LoadConfigAsyncByYooAsset(Type type, Dictionary<Type, byte[]> output)
        {
            YooAsset.AssetHandle handle = YooAsset.YooAssets.LoadAssetSync<TextAsset>($"Assets/Bundles/Config/{type.Name}.bytes");
            await handle.Task;
            output[type] = handle.GetAssetObject<TextAsset>().bytes;
            handle.Release();
        }
    }

    [Invoke]
    public class GetOneConfigBytes : AInvokeHandler<ConfigLoader.GetOneConfigBytes, ETTask<byte[]>>
    {
        public override async ETTask<byte[]> Handle(ConfigLoader.GetOneConfigBytes args)
        {
            string ct = "cs";
            GlobalConfig globalConfig = Resources.Load<GlobalConfig>("GlobalConfig");
            CodeMode codeMode = globalConfig.CodeMode;
            switch (codeMode)
            {
                case CodeMode.Client:
                    ct = "c";
                    break;
                case CodeMode.Server:
                    ct = "s";
                    break;
                case CodeMode.ClientServer:
                    ct = "cs";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            using HashSetComponent<string> startConfigs = HashSetComponent<string>.Create();
            startConfigs.Add("StartMachineConfigCategory");
            startConfigs.Add("StartProcessConfigCategory");
            startConfigs.Add("StartSceneConfigCategory");
            startConfigs.Add("StartZoneConfigCategory");
            string configName = args.ConfigName;
            string configFilePath = startConfigs.Contains(configName)
                ? $"../Config/Excel/{ct}/{Options.Instance.StartConfig}/{configName}.bytes"
                : $"../Config/Excel/{ct}/{configName}.bytes";
            await ETTask.CompletedTask;
            return File.ReadAllBytes(configFilePath);
        }
    }
}