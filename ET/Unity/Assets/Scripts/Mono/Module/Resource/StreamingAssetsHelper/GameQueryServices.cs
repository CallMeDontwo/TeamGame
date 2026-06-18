using UnityEngine;
using YooAsset;

namespace ET
{

#if UNITY_EDITOR
    using System.IO;

    /// <summary>
    /// 资源文件查询服务类
    /// </summary>
    public class GameQueryServices : IBuildinQueryServices
    {
        /// <summary>
        /// 查询内置文件的时候，是否比对文件哈希值
        /// </summary>
        public readonly bool CompareFileCRC;

        public GameQueryServices(bool compareFileCRC = false)
        {
            this.CompareFileCRC = compareFileCRC;
        }

        public bool Query(string packageName, string fileName, string fileCRC)
        {
            string filePath = Path.Combine(Application.streamingAssetsPath, StreamingAssetsDefine.RootFolderName, packageName, fileName);

            if (!File.Exists(filePath))
            {
                return false;
            }

            if (CompareFileCRC && HashUtility.FileCRC32(filePath) != fileCRC)
            {
                return false;
            }

            return true;
        }
    }
#else
    using System.Collections.Generic;

    /// <summary>
    /// 资源文件查询服务类
    /// </summary>
    public class GameQueryServices : IBuildinQueryServices
    {
        /// <summary>
        /// 查询内置文件的时候，是否比对文件哈希值
        /// </summary>
        public readonly bool CompareFileCRC;
        private readonly Dictionary<string, Dictionary<string, string>> _packages = new(10);

        public GameQueryServices(bool compareFileCRC = false)
        {
            this.CompareFileCRC = compareFileCRC;
            BuildinFileManifest manifest = Resources.Load<BuildinFileManifest>("BuildinFileManifest");
            if (manifest != null)
            {
                foreach (BuildinFileManifest.Element element in manifest.BuildinFiles)
                {
                    if (this._packages.TryGetValue(element.PackageName, out var package) == false)
                    {
                        package = new Dictionary<string, string>(512);
                        this._packages.Add(element.PackageName, package);
                    }

                    package.Add(element.FileName, element.FileCRC32);
                }
                Resources.UnloadAsset(manifest);
            }
        }

        public bool Query(string packageName, string fileName, string fileCRC)
        {
            if (!this._packages.TryGetValue(packageName, out var package))
            {
                return false;
            }

            if (!package.TryGetValue(fileName, out var element))
            {
                return false;
            }

            if (this.CompareFileCRC)
            {
                return element == fileCRC;
            }

            return true;
        }
    }
#endif
}