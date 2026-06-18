using System.Collections.Generic;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// AI 调试 Overlay — 运行时按 F1 开关，显示场景中所有 Unit 的 AI 状态
    /// 挂载在 TeamGame 场景根节点上，由 TeamGameSceneCreater1 在 LoadScene 后添加
    /// </summary>
    [EnableClass]
    public class AIDebugger : MonoBehaviour
    {
        public Scene scene;
        private bool visible = true;

        private struct UnitDebugInfo
        {
            public long Id;
            public int ConfigId;
            public string TypeName;
            public string Behavior;
            public long TargetId;
            public float HP;
            public int AIConfigId;
            public string SightRange;
        }

        private readonly List<UnitDebugInfo> cache = new();

        private void OnGUI()
        {
            // F1 开关
            if (Event.current.type == EventType.KeyDown && Event.current.keyCode == KeyCode.F1)
            {
                visible = !visible;
                Event.current.Use();
            }

            if (!visible) return;

            // 收集数据
            CollectData();

            // 半透明黑底
            DrawBackground();

            // 绘制表格
            DrawTable();
        }

        private void DrawBackground()
        {
            int rows = cache.Count + 1;
            float x = 8;
            float y = 8;
            float w = 880;
            float totalH = 40 + rows * 32 + 8;

            Color old = GUI.color;
            GUI.color = new Color(0.08f, 0.08f, 0.1f, 0.92f);
            GUI.DrawTexture(new Rect(x, y, w, totalH), Texture2D.whiteTexture);
            GUI.color = old;

            GUI.color = new Color(1, 1, 1, 0.3f);
            GUI.DrawTexture(new Rect(x, y, w, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x, y + totalH - 1, w, 1), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x, y, 1, totalH), Texture2D.whiteTexture);
            GUI.DrawTexture(new Rect(x + w - 1, y, 1, totalH), Texture2D.whiteTexture);
            GUI.color = old;
        }

        private void CollectData()
        {
            cache.Clear();

            Scene teamScene = scene;
            if (teamScene == null) return;

            var unitManager = teamScene.GetComponent<UnitManager>();
            if (unitManager == null) return;

            foreach (var kv in unitManager.Children)
            {
                if (kv.Value is not Unit unit) continue;

                var info = new UnitDebugInfo
                {
                    Id = unit.Id,
                    ConfigId = unit.ConfigId,
                    SightRange = "—",
                };

                var identity = unit.GetComponent<IdentityComponent>();
                if (identity != null)
                {
                    info.TypeName = identity.UnitType.ToString();
                }

                var ai = unit.GetComponent<AIComponent>();
                if (ai != null)
                {
                    info.Behavior = ai.CurrentBehaviorName;
                    info.AIConfigId = ai.AIConfigId;
                }
                else
                {
                    info.Behavior = "—(no AI)";
                }

                var numeric = unit.GetComponent<NumericComponent>();
                if (numeric != null)
                {
                    info.HP = numeric.GetAsFloat(NumericType.HP);
                }

                var perception = unit.GetComponent<PerceptionComponent>();
                if (perception != null)
                {
                    info.TargetId = perception.PrimaryTargetId;
                    info.SightRange = (perception.SightRange / 100f).ToString("F1");
                }

                cache.Add(info);
            }
        }

        private void DrawTable()
        {
            GUIStyle labelStyle = new(GUI.skin.label) { fontSize = 18, fontStyle = FontStyle.Bold };
            GUIStyle headerStyle = new(GUI.skin.label) { fontSize = 16, fontStyle = FontStyle.Bold };
            GUIStyle boxStyle = new(GUI.skin.box) { fontSize = 18, fontStyle = FontStyle.Bold };

            float x = 10;
            float y = 10;
            float w = 880;
            float rowH = 30;

            GUI.Box(new Rect(x, y, w, 36), "AI Debug (F1 to toggle)", boxStyle);

            y += 40;
            float[] colW = { 100, 80, 80, 120, 80, 80, 100 };
            string[] headers = { "UnitID", "Type", "AIcfg", "Behavior", "Target", "HP", "Sight" };

            float colX = x + 8;
            GUI.contentColor = Color.gray;
            for (int i = 0; i < headers.Length; i++)
            {
                GUI.Label(new Rect(colX, y, colW[i], rowH), headers[i], headerStyle);
                colX += colW[i];
            }
            GUI.contentColor = Color.white;

            y += rowH + 2;
            foreach (var info in cache)
            {
                colX = x + 8;

                Color rowColor = info.Behavior switch
                {
                    "Attack" => Color.red,
                    "Chase" => Color.yellow,
                    "Patrol" => Color.green,
                    "Idle" => Color.gray,
                    _ => Color.white,
                };
                GUI.contentColor = rowColor;

                GUI.Label(new Rect(colX, y, colW[0], rowH), info.Id.ToString(), labelStyle);
                colX += colW[0];
                GUI.Label(new Rect(colX, y, colW[1], rowH), info.TypeName, labelStyle);
                colX += colW[1];
                GUI.contentColor = Color.white;
                GUI.Label(new Rect(colX, y, colW[2], rowH), info.AIConfigId.ToString(), labelStyle);
                colX += colW[2];
                GUI.contentColor = rowColor;
                GUI.Label(new Rect(colX, y, colW[3], rowH), info.Behavior, labelStyle);
                colX += colW[3];
                GUI.Label(new Rect(colX, y, colW[4], rowH), info.TargetId != 0 ? info.TargetId.ToString() : "—", labelStyle);
                colX += colW[4];
                GUI.Label(new Rect(colX, y, colW[5], rowH), info.HP.ToString("F0"), labelStyle);
                colX += colW[5];
                GUI.Label(new Rect(colX, y, colW[6], rowH), info.SightRange, labelStyle);
                colX += colW[6];

                GUI.contentColor = Color.white;
                y += rowH + 2;
            }
        }
    }
}
