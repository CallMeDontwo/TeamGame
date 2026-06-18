using System.IO;
using UnityEditor;

namespace ET
{
    public static class ToolsEditor
    {
        [MenuItem("ET/ExportExcel _F7", false, 0)]
        public static void ExcelExporter()
        {
#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            const string tools = "./Tool";
#else
            const string tools = ".\\Tool.exe";
#endif
            EditorApplication.delayCall += () =>
            {
                ShellHelper.Run($"{tools} --AppType=ExcelExporter --Console=1", "../Bin/");
                AssetDatabase.Refresh();
            };
        }

        [MenuItem("ET/ExportProto _F8", false, 1)]
        public static void Proto2CS()
        {
#if UNITY_EDITOR_OSX || UNITY_EDITOR_LINUX
            const string tools = "./Tool";
#else
            const string tools = ".\\ExportProto.exe";
#endif
            EditorApplication.delayCall += () =>
            {
                ShellHelper.Run($"{tools} --Type=Directory --ProtoPath=../../Server/proto --ExportPath=../Unity/Assets/Scripts/Model/Generate/Client/Message/", "../Bin/");
                AssetDatabase.Refresh();
            };
        }

        [MenuItem("ET/BuildPlayer", false, 2)]
        public static void BuildPlayer()
        {
            EditorApplication.delayCall += () => BuildHelper.Build(EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
        }
    }
}