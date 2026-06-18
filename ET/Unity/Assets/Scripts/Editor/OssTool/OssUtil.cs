using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using YooAsset.Editor;

namespace ET
{
    public sealed class OssUtil : EditorWindow
    {
        private const string URL = "oss://miwanbuluo/ArcadeMaster/Bundles";

        private string[] packageList = null;
        private int lastIndex = -1;
        private int packageIndex = 0;
        private bool enableCustomUrl = false;
        private string couldurl = null;
        private string localFolder = null;
        private string Environment = null;

        [MenuItem("Tools/OssUtil")]
        static void DoIt() => GetWindow<OssUtil>();

        private void OnEnable()
        {
            this.lastIndex = -1;
            this.packageList = this.GetBuildPackageNames().ToArray();
            this.Environment = Resources.Load<GlobalConfig>("GlobalConfig").GetCurrentEnvironment();
        }

        private void OnGUI()
        {
            this.packageIndex = EditorGUILayout.Popup("Package", this.packageIndex, this.packageList);
            this.enableCustomUrl = EditorGUILayout.Toggle("CustomUrl", this.enableCustomUrl);
            if (this.lastIndex != this.packageIndex)
            {
                this.lastIndex = this.packageIndex;
                if (!this.enableCustomUrl)
                    this.couldurl = $"{URL}/{this.Environment}/{EditorUserBuildSettings.activeBuildTarget}/{Application.version}/{this.packageList[this.packageIndex]}/";
            }
            EditorGUILayout.LabelField("CouldUrl");
            EditorGUI.BeginDisabledGroup(!this.enableCustomUrl);
            this.couldurl = EditorGUILayout.TextField(this.couldurl);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.LabelField("LocalFolder");
            this.localFolder = EditorGUILayout.TextField(this.localFolder);
            if (GUILayout.Button("Select Folder", GUILayout.Height(30)))
            {
                string output = AssetBundleBuilderHelper.GetDefaultBuildOutputRoot();
                BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
                string packageName = this.packageList[this.packageIndex];
                this.localFolder = EditorUtility.OpenFolderPanel("选择文件夹", $"{output}/{buildTarget}/{packageName}", "");
            }
            if (GUILayout.Button("Upload", GUILayout.Height(30)))
            {
                if (string.IsNullOrWhiteSpace(this.localFolder))
                {
                    EditorUtility.DisplayDialog("请选择文件夹", "请选择文件夹", "确定");
                    return;
                }
                ShellHelper.Start($"ossutil.exe cp -r -f {this.localFolder} {this.couldurl}", "../Tools/ossutil/");
            }
        }

        private List<string> GetBuildPackageNames()
        {
            List<string> result = new List<string>();
            AssetBundleCollectorSettingData.Setting.Packages.ForEach(item => result.Add(item.PackageName));
            return result;
        }
    }
}