using UnityEditor;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// ET菜单顺序
    /// </summary>
    public static class ETMenuItemPriority
    {
        public const int BuildTool = 1001;
        public const int ChangeDefine = 1002;
        public const int Compile = 1003;
        public const int Reload = 1004;
        public const int NavMesh = 1005;
        public const int ServerTools = 1006;
    }

    public class BuildEditor : EditorWindow
    {
        private BuildTarget activeBuildTarget;
        private BuildTarget selectedBuildTarget;
        private BuildOptions buildOptions;

        private GlobalConfig globalConfig;

        [MenuItem("ET/Build Tool", false, ETMenuItemPriority.BuildTool)]
        public static void ShowWindow()
        {
            GetWindow<BuildEditor>(DockDefine.Types);
        }

        private void OnEnable()
        {
            this.globalConfig = AssetDatabase.LoadAssetAtPath<GlobalConfig>("Assets/Resources/GlobalConfig.asset");
            this.activeBuildTarget = EditorUserBuildSettings.activeBuildTarget;
            this.selectedBuildTarget = this.activeBuildTarget;
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("BuildTarget:");
            this.selectedBuildTarget = (BuildTarget)EditorGUILayout.EnumPopup(this.selectedBuildTarget);

            EditorGUILayout.LabelField("BuildOptions:");
            this.buildOptions = (BuildOptions)EditorGUILayout.EnumFlagsField(this.buildOptions);

            GUILayout.Space(5);

            if (GUILayout.Button("BuildPackage", GUILayout.Height(40)))
            {
                if (this.globalConfig.CodeMode != CodeMode.Client)
                {
                    Log.Error("build package CodeMode must be CodeMode.Client, please select Client");
                    return;
                }

                if (this.selectedBuildTarget != this.activeBuildTarget)
                {
                    switch (EditorUtility.DisplayDialogComplex("Warning!", $"current platform is {this.activeBuildTarget}, if change to {this.selectedBuildTarget}, may be take a long time", "change", "cancel", "no change"))
                    {
                        case 0:
                            this.activeBuildTarget = this.selectedBuildTarget;
                            break;
                        case 1:
                            return;
                        case 2:
                            this.selectedBuildTarget = this.activeBuildTarget;
                            break;
                    }
                }

                BuildHelper.Build(this.selectedBuildTarget, this.buildOptions);
                return;
            }

            if (GUILayout.Button("ExcelExporter", GUILayout.Height(40)))
            {
                ToolsEditor.ExcelExporter();
                return;
            }

            if (GUILayout.Button("Proto2CS", GUILayout.Height(40)))
            {
                ToolsEditor.Proto2CS();
                return;
            }

            GUILayout.Space(5);
        }
    }
}