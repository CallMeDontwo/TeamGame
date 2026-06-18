using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ET
{
    public class CodeLoader : Singleton<CodeLoader>, ISingletonAwake
    {
        private GlobalConfig Config;
        private Assembly modelAssembly;
        private Assembly modelViewAssembly;
        private Dictionary<string, TextAsset> dlls;

        public void Awake()
        {
            this.Config = Resources.Load<GlobalConfig>("GlobalConfig");
        }

        public async ETTask DownloadAsync()
        {
            if (!Define.IsEditor)
            {
                this.dlls = await ResourcesComponent.Instance.LoadAllAssetsAsync<TextAsset>($"Assets/Bundles/Code/Unity.Model.dll.bytes");
            }
        }

        public void Start()
        {
            (Assembly modelAssembly, Assembly modelViewAssembly) = this.LoadAssembly("Unity.Model", "Unity.ModelView", () => !this.Config.SimulateMode);
            (Assembly hotfixAssembly, Assembly hotfixViewAssembly) = this.LoadAssembly("Unity.Hotfix", "Unity.HotfixView", () => !this.Config.SimulateMode || this.Config.EnableDll);

            this.modelAssembly = modelAssembly;
            this.modelViewAssembly = modelViewAssembly;

            World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]{
                typeof (World).Assembly, typeof (Global).Assembly,
                this.modelAssembly, this.modelViewAssembly,
                hotfixAssembly,hotfixViewAssembly
            });

            new StaticMethod(this.modelAssembly, "ET.Entry", "Start").Run();
        }

        private (Assembly, Assembly) LoadAssembly(string assemblyName1, string assemblyName2, Func<bool> func)
        {
            if (!Define.IsEditor)
            {
                byte[] assembly1DllBytes = this.dlls[$"{assemblyName1}.dll"].bytes;
                byte[] assembly1PdbBytes = this.dlls[$"{assemblyName1}.pdb"].bytes;
                byte[] assembly2DllBytes = this.dlls[$"{assemblyName2}.dll"].bytes;
                byte[] assembly2PdbBytes = this.dlls[$"{assemblyName2}.pdb"].bytes;
                //如果需要测试，可替换成下面注释的代码直接加载Assets/Bundles/Code/Hotfix.dll.bytes，但真正打包时必须使用上面的代码
                //byte[] assembly1DllBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assembly1}.dll.bytes"));
                //byte[] assembly1PdbBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assembly1}.pdb.bytes"));
                //byte[] assembly2DllBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assembly2}.dll.bytes"));
                //byte[] assembly2PdbBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assembly2}.pdb.bytes"));
                return (Assembly.Load(assembly1DllBytes, assembly1PdbBytes), Assembly.Load(assembly2DllBytes, assembly2PdbBytes));
            }
            else
            {
                if (func())
                {
                    byte[] assembly1DllBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assemblyName1}.dll.bytes"));
                    byte[] assembly1PdbBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assemblyName1}.pdb.bytes"));
                    byte[] assembly2DllBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assemblyName2}.dll.bytes"));
                    byte[] assembly2PdbBytes = File.ReadAllBytes(Path.Combine(Define.CodeDir, $"{assemblyName2}.pdb.bytes"));
                    return (Assembly.Load(assembly1DllBytes, assembly1PdbBytes), Assembly.Load(assembly2DllBytes, assembly2PdbBytes));
                }
                else
                {
                    Assembly assembly1 = null;
                    Assembly assembly2 = null;
                    foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                    {
                        string name = ass.GetName().Name;
                        if (name == assemblyName1)
                        {
                            assembly1 = ass;
                        }
                        if (name == assemblyName2)
                        {
                            assembly2 = ass;
                        }
                        if (assembly1 != null && assembly2 != null)
                        {
                            break;
                        }
                    }
                    return (assembly1, assembly2);
                }
            }
        }

        public void Reload()
        {
            (Assembly hotfixAssembly, Assembly hotfixViewAssembly) = this.LoadAssembly("Unity.Hotfix", "Unity.HotfixView", () => !this.Config.SimulateMode || this.Config.EnableDll);

            CodeTypes codeTypes = World.Instance.AddSingleton<CodeTypes, Assembly[]>(new[]
            {
                typeof (World).Assembly, typeof (Global).Assembly,
                this.modelAssembly, this.modelViewAssembly,
                hotfixAssembly,hotfixViewAssembly
            });
            codeTypes.CreateCode();

            Log.Info($"reload dll finish!");
        }
    }
}