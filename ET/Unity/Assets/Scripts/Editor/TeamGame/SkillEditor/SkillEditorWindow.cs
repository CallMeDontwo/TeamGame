using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能时间轴编辑器 — 直写 JSON 到 Assets/Bundles/Config/Skills/
    /// 菜单: Tools/技能编辑器
    /// 无需 Play Mode，编辑即保存
    /// </summary>
    public class SkillEditorWindow : EditorWindow
    {
        private const string SKILLS_DIR = "Assets/Bundles/Skill";
        private List<SkillData> _skills = new();
        private int _selectedSkillIdx = -1;
        private int _selectedEventIdx = -1;
        private Vector2 _skillListScroll, _timelineScroll, _propScroll;

        // 新建技能
        private int _newSkillId = 1001;
        private string _newSkillName = "新技能";

        // 时间轴参数
        private const float TIMELINE_LEFT = 80f;
        private const float RULER_HEIGHT = 24f;
        private const float TRACK_HEIGHT = 48f;
        private float _msPerPixel = 0.5f; // 缩放：越小=越放大

        [MenuItem("Tools/技能编辑器")]
        public static void Open() => GetWindow<SkillEditorWindow>("技能编辑器");

        private void OnEnable()
        {
            LoadAllSkills();
        }

        private void OnGUI()
        {
            GUILayout.BeginHorizontal();
            DrawSkillList();
            DrawTimeline();
            DrawEventProps();
            GUILayout.EndHorizontal();
        }

        // ═══════════════════════════════ 左侧：技能列表 ═══════════════════════════════

        private void DrawSkillList()
        {
            GUILayout.BeginVertical(GUILayout.Width(220));
            EditorGUILayout.LabelField("技能列表", EditorStyles.boldLabel);

            // 新建
            GUILayout.BeginHorizontal();
            _newSkillId = EditorGUILayout.IntField(_newSkillId, GUILayout.Width(60));
            _newSkillName = EditorGUILayout.TextField(_newSkillName, GUILayout.Width(100));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("+ 新建") && _newSkillId > 0)
            {
                CreateSkill(_newSkillId, _newSkillName);
                _newSkillId++;
                _newSkillName = "新技能";
            }

            EditorGUILayout.Space();
            _skillListScroll = GUILayout.BeginScrollView(_skillListScroll);
            for (int i = 0; i < _skills.Count; i++)
            {
                var s = _skills[i];
                bool selected = i == _selectedSkillIdx;
                GUI.backgroundColor = selected ? new Color(0.3f, 0.5f, 1f) : Color.white;
                if (GUILayout.Button($"[{s.SkillId}] {s.Name}", GUILayout.Height(28)))
                {
                    _selectedSkillIdx = i;
                    _selectedEventIdx = -1;
                }
            }
            GUI.backgroundColor = Color.white;
            GUILayout.EndScrollView();

            if (_selectedSkillIdx >= 0 && GUILayout.Button("删除此技能"))
            {
                DeleteSkill(_selectedSkillIdx);
                _selectedSkillIdx = -1;
                _selectedEventIdx = -1;
            }

            if (GUILayout.Button("刷新列表"))
                LoadAllSkills();

            GUILayout.EndVertical();
        }

        // ═══════════════════════════════ 中间：时间轴 ═══════════════════════════════

        private void DrawTimeline()
        {
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true));

            if (_selectedSkillIdx < 0 || _selectedSkillIdx >= _skills.Count)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("选择左侧技能查看时间轴", EditorStyles.centeredGreyMiniLabel);
                GUILayout.FlexibleSpace();
                GUILayout.EndVertical();
                return;
            }

            var skill = _skills[_selectedSkillIdx];
            EditorGUILayout.LabelField($"技能 {skill.SkillId} — {skill.Name}", EditorStyles.boldLabel);

            // 基本属性 — 名称/描述
            EditorGUILayout.BeginHorizontal();
            skill.Name = EditorGUILayout.TextField("名称", skill.Name);
            skill.Desc = EditorGUILayout.TextField("描述", skill.Desc);
            EditorGUILayout.EndHorizontal();

            // CD
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("CD(秒)", GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newCD = EditorGUILayout.IntField(skill.CD, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck()) skill.CD = newCD;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // 时长
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("时长(ms)", GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newDur = EditorGUILayout.IntField(skill.Duration, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck()) skill.Duration = newDur;
            if (skill.Duration < 100) skill.Duration = 100;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // 施法距离
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("施法距离", GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newRange = EditorGUILayout.IntField(skill.CastRange, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck()) skill.CastRange = newRange;
            GUILayout.Label("/100", GUILayout.Width(35));
            if (skill.CastRange > 0)
                GUILayout.Label($"= {(skill.CastRange / 100f):F2} 单位");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // 攻击可达高度
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("可达高度", GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newReachH = EditorGUILayout.IntField(skill.ReachHeight, GUILayout.Width(120));
            if (EditorGUI.EndChangeCheck()) skill.ReachHeight = newReachH;
            GUILayout.Label("/100", GUILayout.Width(35));
            if (skill.ReachHeight > 0)
                GUILayout.Label($"= {skill.ReachHeight / 100f:F2} 层差");
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // 按钮
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("保存", GUILayout.Width(80)))
                SaveSkill(skill);
            if (GUILayout.Button("+事件", GUILayout.Width(80)))
                AddEvent(skill);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            // 缩放控制
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("-", GUILayout.Width(22))) _msPerPixel = Mathf.Min(_msPerPixel * 2f, 8f);
            if (GUILayout.Button("+", GUILayout.Width(22))) _msPerPixel = Mathf.Max(_msPerPixel * 0.5f, 0.05f);
            GUILayout.Label($"缩放: 1px = {_msPerPixel:F2}ms", GUILayout.Width(120));
            if (GUILayout.Button("重置", GUILayout.Width(40))) _msPerPixel = 0.5f;
            EditorGUILayout.EndHorizontal();

            // 鼠标滚轮缩放
            var evt = Event.current;
            if (evt.type == EventType.ScrollWheel && GUILayoutUtility.GetLastRect().Contains(evt.mousePosition))
            {
                _msPerPixel = Mathf.Clamp(_msPerPixel * (evt.delta.y > 0 ? 1.2f : 0.8f), 0.05f, 8f);
                evt.Use();
            }

            EditorGUILayout.Space();

            // 时间轴区域
            float totalWidth = skill.Duration / _msPerPixel + 160;
            _timelineScroll = GUILayout.BeginScrollView(_timelineScroll, GUILayout.Height(200));

            var trackRect = new Rect(TIMELINE_LEFT, 0, totalWidth, RULER_HEIGHT + TRACK_HEIGHT + 30);

            // 刻度尺
            EditorGUI.DrawRect(new Rect(TIMELINE_LEFT, 0, totalWidth, RULER_HEIGHT), new Color(0.2f, 0.2f, 0.2f));
            for (int t = 0; t <= skill.Duration; t += GetTickStep(skill.Duration))
            {
                float x = TIMELINE_LEFT + t / _msPerPixel;
                var labelStyle = new GUIStyle(EditorStyles.miniLabel) { alignment = TextAnchor.MiddleCenter };
                GUI.Label(new Rect(x - 20, 2, 40, 16), $"{t}ms", labelStyle);
                EditorGUI.DrawRect(new Rect(x, RULER_HEIGHT - 6, 1, 6), Color.gray);
            }

            // 轨道
            var trackY = RULER_HEIGHT;
            EditorGUI.DrawRect(new Rect(TIMELINE_LEFT, trackY, totalWidth, TRACK_HEIGHT), new Color(0.25f, 0.25f, 0.3f));

            // 事件块（同时间戳垂直错开）
            if (skill.Events != null)
            {
                // 按时间戳分组，算垂直偏移
                var tsGroups = new Dictionary<int, int>();
                var tsOffsets = new int[skill.Events.Count];
                for (int i = 0; i < skill.Events.Count; i++)
                {
                    int ts = skill.Events[i].Timestamp;
                    if (!tsGroups.ContainsKey(ts)) tsGroups[ts] = 0;
                    tsOffsets[i] = tsGroups[ts]++;
                }
                int maxStack = 1;
                foreach (var v in tsGroups.Values) maxStack = Math.Max(maxStack, v);
                float stepY = (TRACK_HEIGHT - 12) / maxStack;

                for (int i = 0; i < skill.Events.Count; i++)
                {
                    var ev = skill.Events[i];
                    float bx = TIMELINE_LEFT + ev.Timestamp / _msPerPixel - 30;
                    float by = trackY + 6 + tsOffsets[i] * stepY;
                    Color c = GetEventColor(ev.EventType);
                    var bRect = new Rect(bx, by, 60, stepY - 2);
                    EditorGUI.DrawRect(bRect, c);
                    GUI.Label(bRect, GetEventLabel(ev.EventType), new GUIStyle(EditorStyles.miniLabel)
                    {
                        alignment = TextAnchor.MiddleCenter,
                        normal = { textColor = Color.white },
                        fontSize = 10,
                    });

                    if (GUI.Button(bRect, "", GUIStyle.none))
                    {
                        _selectedEventIdx = i;
                    }

                    // 选中高亮
                    if (i == _selectedEventIdx)
                    {
                        EditorGUI.DrawRect(bRect, new Color(1, 1, 0, 0.3f));
                    }
                }
            }

            GUILayout.EndScrollView();
            GUILayout.Space(10);
            GUILayout.EndVertical();
        }

        // ═══════════════════════════════ 右侧：事件属性 ═══════════════════════════════

        private void DrawEventProps()
        {
            GUILayout.BeginVertical(GUILayout.Width(500));
            EditorGUILayout.LabelField("事件属性", EditorStyles.boldLabel);

            if (_selectedSkillIdx < 0 || _selectedEventIdx < 0)
            {
                GUILayout.Label("选择左侧事件编辑");
                GUILayout.EndVertical();
                return;
            }

            var skill = _skills[_selectedSkillIdx];
            if (_selectedEventIdx >= skill.Events.Count)
            {
                _selectedEventIdx = -1;
                GUILayout.EndVertical();
                return;
            }

            var ev = skill.Events[_selectedEventIdx];
            EditorGUILayout.LabelField($"事件 #{_selectedEventIdx + 1}", EditorStyles.miniBoldLabel);

            ev.EventType = EditorGUILayout.IntPopup("类型", ev.EventType,
                new[] { "1-动画", "2-找目标", "3-特效", "4-数值", "5-Buff", "6-子弹" },
                new[] { 1, 2, 3, 4, 5, 6 });

            ev.Timestamp = EditorGUILayout.IntField("时间戳(ms)", ev.Timestamp);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField($"参数（{GetEventHint(ev.EventType)}）", EditorStyles.miniLabel);

            ev.IntParam1 = EditorGUILayout.IntField("IntParam1", ev.IntParam1);
            ev.IntParam2 = EditorGUILayout.IntField("IntParam2", ev.IntParam2);
            ev.IntParam3 = EditorGUILayout.IntField("IntParam3", ev.IntParam3);
            ev.FloatParam1 = EditorGUILayout.FloatField("FloatParam1", ev.FloatParam1);
            ev.FloatParam2 = EditorGUILayout.FloatField("FloatParam2", ev.FloatParam2);
            ev.FloatParam3 = EditorGUILayout.FloatField("FloatParam3", ev.FloatParam3);
            ev.FloatParam4 = EditorGUILayout.FloatField("FloatParam4", ev.FloatParam4);
            ev.StringParam = EditorGUILayout.TextField("StringParam", ev.StringParam ?? "");

            EditorGUILayout.Space();

            if (GUILayout.Button("删除此事件"))
            {
                skill.Events.RemoveAt(_selectedEventIdx);
                _selectedEventIdx = -1;
                SaveSkill(skill);
            }
            if (GUILayout.Button("上移")) MoveEvent(skill, _selectedEventIdx, -1);
            if (GUILayout.Button("下移")) MoveEvent(skill, _selectedEventIdx, 1);

            EditorGUILayout.Space();

            EditorGUILayout.HelpBox(GetEventHint(ev.EventType), MessageType.Info);

            GUILayout.EndVertical();
        }

        // ═══════════════════════════════ 操作 ═══════════════════════════════

        private void AddEvent(SkillData skill)
        {
            if (skill.Events == null) skill.Events = new List<SkillEventData>();
            int ts = skill.Duration / 2;
            skill.Events.Add(new SkillEventData
            {
                EventType = 4,
                Timestamp = ts,
                IntParam1 = 10,
            });
            skill.Events.Sort((a, b) => a.Timestamp.CompareTo(b.Timestamp));
            _selectedEventIdx = skill.Events.Count - 1;
            SaveSkill(skill);
        }

        private void CreateSkill(int id, string name)
        {
            if (_skills.Any(s => s.SkillId == id))
            {
                EditorUtility.DisplayDialog("错误", $"技能ID {id} 已存在", "确定");
                return;
            }

            var skill = new SkillData
            {
                SkillId = id,
                Name = name,
                Duration = 1000,
                Events = new List<SkillEventData>()
            };
            _skills.Add(skill);
            _selectedSkillIdx = _skills.Count - 1;
            SaveSkill(skill);
        }

        private void DeleteSkill(int idx)
        {
            var skill = _skills[idx];
            string path = GetSkillPath(skill.SkillId);
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Delete(path + ".meta");
            }
            _skills.RemoveAt(idx);
            AssetDatabase.Refresh();
        }

        private void MoveEvent(SkillData skill, int idx, int dir)
        {
            int newIdx = idx + dir;
            if (newIdx < 0 || newIdx >= skill.Events.Count) return;
            var tmp = skill.Events[idx];
            skill.Events[idx] = skill.Events[newIdx];
            skill.Events[newIdx] = tmp;
            _selectedEventIdx = newIdx;
            SaveSkill(skill);
        }

        // ═══════════════════════════════ 持久化 ═══════════════════════════════

        private void SaveSkill(SkillData skill)
        {
            if (!Directory.Exists(SKILLS_DIR))
                Directory.CreateDirectory(SKILLS_DIR);

            string path = GetSkillPath(skill.SkillId);
            string json = JsonUtility.ToJson(skill, true);
            File.WriteAllText(path, json);

            // YooAsset Runtime 通过标签 "Skill" 加载，需在 YooAsset Collector 中给 Skills 目录加标签
            AssetDatabase.Refresh();
            Debug.Log($"已保存: {path}");
        }

        private void LoadAllSkills()
        {
            _skills.Clear();
            if (!Directory.Exists(SKILLS_DIR))
                Directory.CreateDirectory(SKILLS_DIR);

            var files = Directory.GetFiles(SKILLS_DIR, "skill_*.json");
            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var skill = JsonUtility.FromJson<SkillData>(json);
                    if (skill != null && skill.SkillId > 0)
                        _skills.Add(skill);
                }
                catch (Exception e)
                {
                    Debug.LogError($"SkillEditor: 解析失败 {file}: {e.Message}");
                }
            }
            _skills.Sort((a, b) => a.SkillId.CompareTo(b.SkillId));
            Debug.Log($"SkillEditor: 加载 {_skills.Count} 个技能");
        }

        private static string GetSkillPath(int skillId) => $"{SKILLS_DIR}/skill_{skillId}.json";

        // ═══════════════════════════════ 工具方法 ═══════════════════════════════

        private static int GetTickStep(int duration)
        {
            if (duration <= 500) return 100;
            if (duration <= 2000) return 200;
            return 500;
        }

        private static Color GetEventColor(int type) => type switch
        {
            1 => new Color(0.65f, 0.89f, 0.63f, 0.9f),  // 动画 绿
            2 => new Color(0.98f, 0.89f, 0.69f, 0.9f),  // 找目标 黄
            3 => new Color(0.54f, 0.71f, 0.98f, 0.9f),  // VFX 蓝
            4 => new Color(0.95f, 0.54f, 0.66f, 0.9f),  // 数值 红
            5 => new Color(0.80f, 0.65f, 0.97f, 0.9f),  // Buff 紫
            6 => new Color(0.58f, 0.89f, 0.84f, 0.9f),  // 子弹 青
            _ => Color.gray,
        };

        private static string GetEventLabel(int type) => type switch
        {
            1 => "动画", 2 => "找目标", 3 => "VFX", 4 => "数值", 5 => "Buff", 6 => "子弹", _ => "?"
        };

        private static string GetEventHint(int type) => type switch
        {
            1 => "StringParam=动画名",
            2 => "IntParam1=TargetFinderConfigId",
            3 => "StringParam=特效路径, P1=X偏移(/100,朝左自动取反), P2=Y偏移(/100), P3=持续时间(ms)",
            4 => "IntParam1=数值, P2=0伤害目标/1治疗自己/2伤害AOE",
            5 => "IntParam1=BuffConfigId",
            6 => "IntParam1=BulletConfigId",
            _ => ""
        };
    }
}
