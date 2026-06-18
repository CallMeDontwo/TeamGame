using FairyGUI;
using UnityEditor;
using UnityEngine;

namespace FairyGUIEditor
{
    /// <summary>
    /// 
    /// </summary>
    [CustomEditor(typeof(StageCamera))]
    public class StageCameraEditor : Editor
    {
        string[] propertyToExclude;
        bool constantSize = false;
        float cameraSize = 0;
        float unitsPerPixel = 0;


        void OnEnable()
        {
            this.propertyToExclude = new string[] { "m_Script" };
        }

        public override void OnInspectorGUI()
        {
            StageCamera stageCamera = (StageCamera)this.target;
            this.serializedObject.Update();

            DrawPropertiesExcluding(this.serializedObject, this.propertyToExclude);

            this.constantSize = stageCamera.constantSize;
            this.cameraSize = stageCamera.cameraSize;
            this.unitsPerPixel = stageCamera.unitsPerPixel;
            this.constantSize = GUILayout.Toggle(this.constantSize, "Constant Size");
            if (this.constantSize)
            {
                this.cameraSize = EditorGUILayout.DelayedFloatField("Camera Size", this.cameraSize);
                if (stageCamera.constantSize != this.constantSize)
                    stageCamera.constantSize = this.constantSize;
                if (stageCamera.cameraSize != this.cameraSize)
                    stageCamera.cameraSize = this.cameraSize;
            }
            else
            {
                this.unitsPerPixel = EditorGUILayout.DelayedFloatField("Units Per Pixel", this.unitsPerPixel);
                if (stageCamera.constantSize != this.constantSize)
                    stageCamera.constantSize = this.constantSize;
                if (stageCamera.unitsPerPixel != this.unitsPerPixel)
                    stageCamera.unitsPerPixel = this.unitsPerPixel;
            }

            if (this.serializedObject.ApplyModifiedProperties())
                (this.target as StageCamera).ApplyModifiedProperties();
        }
    }
}
