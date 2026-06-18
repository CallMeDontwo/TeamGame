using System;
using UnityEditor;

namespace ET
{
    [CustomEditor(typeof(GlobalConfig))]
    public class GlobalConfigEditor : Editor
    {
        private CodeMode codeMode;
        private BuildType buildType;
        private int lastEnv = 0;

        private void OnEnable()
        {
            GlobalConfig globalConfig = (GlobalConfig)this.target;
            globalConfig.BuildType = EditorUserBuildSettings.development ? BuildType.Debug : BuildType.Release;
            this.lastEnv = globalConfig.Environment;
            this.codeMode = globalConfig.CodeMode;
            this.buildType = globalConfig.BuildType;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GlobalConfig globalConfig = (GlobalConfig)this.target;

            if (this.codeMode != globalConfig.CodeMode)
            {
                this.codeMode = globalConfig.CodeMode;
                this.serializedObject.Update();
                AssemblyTool.DoCompile();
            }
            if (this.buildType != globalConfig.BuildType)
            {
                this.buildType = globalConfig.BuildType;
                EditorUserBuildSettings.development = this.buildType switch
                {
                    BuildType.Debug => true,
                    BuildType.Release => false,
                    _ => throw new ArgumentOutOfRangeException()
                };
                this.serializedObject.Update();
                AssemblyTool.DoCompile();
            }
            globalConfig.Environment = EditorGUILayout.Popup(globalConfig.Environment, globalConfig.environments.ToArray());
            if (this.lastEnv != globalConfig.Environment)
            {
                this.serializedObject.Update();
                EditorUtility.SetDirty(this.target);
            }
            AssetDatabase.SaveAssetIfDirty(this.target);
        }
    }
}