using System;
using System.Collections;
using CommandLine;
using UnityEngine;

namespace ET
{
    public class Global : MonoBehaviour
    {
        public static Global Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private async void Start()
        {
            try
            {
                DontDestroyOnLoad(this.gameObject);

                ETTask.ExceptionHandler += Log.Error;
                AppDomain.CurrentDomain.UnhandledException += (sender, e) => Log.Error(e.ExceptionObject.ToString());

                // 命令行参数
                Parser.Default.ParseArguments<Options>(Array.Empty<string>())
                    .WithParsed((o) => World.Instance.AddSingleton(o))
                    .WithNotParsed(error => throw new Exception($"命令行格式错误! {error}"));
                Options.Instance.StartConfig = $"StartConfig/Localhost";

                World.Instance.AddSingleton<TimeInfo>();
                World.Instance.AddSingleton<FiberManager>();
                World.Instance.AddSingleton<Logger, ILog>(new UnityLogger());
                World.Instance.AddSingleton<CodeLoader>();
                World.Instance.AddSingleton<ResourcesComponent>();

                await AssetLoader.Instance.LoadAsync();
                await AotLoader.Instance.LoadAsync();
                await CodeLoader.Instance.DownloadAsync();
                CodeLoader.Instance.Start();
            }
            catch (Exception e)
            {
                Log.Error(e);
                throw;
            }
        }

        private void Update()
        {
            TimeInfo.Instance.Update();
            FiberManager.Instance.Update();
        }

        private void LateUpdate()
        {
            FiberManager.Instance.LateUpdate();
        }

        private void OnApplicationQuit()
        {
            World.Instance.Dispose();
        }

        public static Coroutine Coroutine(IEnumerator routine)
        {
            return Instance.StartCoroutine(routine);
        }
    }
}