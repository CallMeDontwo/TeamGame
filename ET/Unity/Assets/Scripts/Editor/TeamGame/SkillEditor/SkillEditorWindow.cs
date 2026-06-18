using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 技能编辑器 — 可视化编排技能时间轴事件
    /// 菜单 Window / Skill Editor 打开
    /// </summary>
    public class SkillEditorWindow : EditorWindow
    {
        // 布局
        private Rect _leftPanel, _centerPanel, _rightPanel;
        private float _leftWidth = 180f, _rightWidth = 260f;

        // 数据
        private List<SkillConfig> _skills;
        private int _selectedSkillIndex = -1;
        private SkillConfig CurrentSkill => _selectedSkillIndex >= 0 ? _skills[_selectedSkillIndex] : null;
        private bool _configLoaded;

        // 时间轴
        private Vector2 _timelineScroll;
        private float _pixelsPerMs = 0.5f;
        private float _trackHeight = 32f;
        private int _selectedEventIndex = -1;
        private int _dragEventIndex = -1;
        private float _dragStartX;

        [MenuItem("Window/Skill Editor")]
        public static void Open()
        {
            var window = GetWindow<SkillEditorWindow>("技能编辑器");
            window.minSize = new Vector2(900, 500);
            window.Show();
        }

        private void OnEnable()
        {
            _skills = new List<SkillConfig>();
            CheckConfigLoaded();
            if (_configLoaded) RefreshSkills();
            EditorApplication.playModeStateChanged += OnPlayModeChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeChanged;
        }

        private void OnPlayModeChanged(PlayModeStateChange state)
        {
            if (state == PlayModeStateChange.EnteredPlayMode)
            {
                CheckConfigLoaded();
                if (_configLoaded) RefreshSkills();
                Repaint();
            }
        }

        private void CheckConfigLoaded()
        {
            try { _configLoaded = SkillConfigCategory.Instance != null; }
            catch { _configLoaded = false; }
        }

        private void RefreshSkills()
        {
            _skills = new List<SkillConfig>();
            try
            {
                var inst = SkillConfigCategory.Instance;
                if (inst != null)
                {
                    var dict = inst.GetAll();
                    if (dict != null)
                        _skills.AddRange(dict.Values);
                }
            }
            catch { }
            _skills.Sort((a, b) => a.Id.CompareTo(b.Id));
        }

        private void OnGUI()
        {
            if (!_configLoaded)
            {
                EditorGUILayout.HelpBox("配置未加载，请先进入 Play Mode 运行游戏", MessageType.Info);
                if (GUILayout.Button("重试加载", GUILayout.Height(30)))
                {
                    CheckConfigLoaded();
                    if (_configLoaded) RefreshSkills();
                }
                return;
            }

            CalculateLayout();
            DrawLeftPanel();
            DrawCenterPanel();
            DrawRightPanel();

            // 拖拽时持续重绘
            if (_dragEventIndex >= 0) Repaint();
        }

        // ═══════════════════════════════════
        // 布局
        // ═══════════════════════════════════

        private void CalculateLayout()
        {
            float totalW = position.width;
            float totalH = position.height;
            _leftPanel = new Rect(0, 0, _leftWidth, totalH);
            _centerPanel = new Rect(_leftWidth, 0, totalW - _leftWidth - _rightWidth, totalH);
            _rightPanel = new Rect(totalW - _rightWidth, 0, _rightWidth, totalH);
        }

        // ═══════════════════════════════════
        // 左侧 — 技能列表
        // ═══════════════════════════════════

        private void DrawLeftPanel()
        {
            GUILayout.BeginArea(_leftPanel, EditorStyles.helpBox);
            EditorGUILayout.LabelField("技能列表", EditorStyles.boldLabel);

            EditorGUILayout.Space(4);

            if (GUILayout.Button("+ 新建技能", GUILayout.Height(28)))
            {
                CreateNewSkill();
            }

            EditorGUILayout.Space(6);

            if (_skills == null || _skills.Count == 0)
            {
                EditorGUILayout.LabelField("暂无技能配置", EditorStyles.centeredGreyMiniLabel);
            }
            else
            {
                for (int i = 0; i < _skills.Count; i++)
                {
                    var skill = _skills[i];
                    bool isSelected = i == _selectedSkillIndex;
                    GUI.backgroundColor = isSelected ? new Color(0.3f, 0.6f, 0.9f) : Color.white;
                    if (GUILayout.Button($"{skill.Id}: {skill.Name}", GUILayout.Height(24)))
                    {
                        _selectedSkillIndex = i;
                        _selectedEventIndex = -1;
                    }
                    GUI.backgroundColor = Color.white;
                }
            }

            EditorGUILayout.Space(6);
            if (_selectedSkillIndex >= 0 && GUILayout.Button("删除当前技能", GUILayout.Height(24)))
            {
                if (EditorUtility.DisplayDialog("删除技能", $"确定删除 {CurrentSkill.Name}？", "确定", "取消"))
                {
                    DeleteSkill(_selectedSkillIndex);
                }
            }

            GUILayout.EndArea();
        }

        private void CreateNewSkill()
        {
            int newId = _skills.Count > 0 ? _skills[^1].Id + 1 : 1001;
            var skill = new SkillConfig
            {
                Id = newId,
                Name = "新技能",
                CD = 5,
                Duration = 1500,
                SkillEventIds = System.Array.Empty<int>(),
                SkillEventTimestamps = System.Array.Empty<int>(),
            };
            _skills.Add(skill);
            _selectedSkillIndex = _skills.Count - 1;
            _selectedEventIndex = -1;
        }

        private void DeleteSkill(int index)
        {
            _skills.RemoveAt(index);
            if (_selectedSkillIndex >= _skills.Count) _selectedSkillIndex = _skills.Count - 1;
            _selectedEventIndex = -1;
        }

        // ═══════════════════════════════════
        // 中间 — 时间轴
        // ═══════════════════════════════════

        private void DrawCenterPanel()
        {
            GUILayout.BeginArea(_centerPanel, EditorStyles.helpBox);
            if (CurrentSkill == null)
            {
                EditorGUILayout.LabelField("请在左侧选择技能", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandHeight(true));
                GUILayout.EndArea();
                return;
            }

            DrawSkillHeader();
            DrawTimeline();
            GUILayout.EndArea();
        }

        private void DrawSkillHeader()
        {
            EditorGUILayout.BeginHorizontal();
            CurrentSkill.Name = EditorGUILayout.TextField("名称", CurrentSkill.Name);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            CurrentSkill.Duration = EditorGUILayout.IntField("时长(ms)", CurrentSkill.Duration);
            CurrentSkill.CD = EditorGUILayout.IntField("CD(s)", CurrentSkill.CD);
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("+ 添加事件", GUILayout.Height(24)))
            {
                AddEvent(CurrentSkill.Duration / 2);
            }

            float pixelsPerMs = EditorGUILayout.Slider("缩放", _pixelsPerMs, 0.1f, 2f);
            if (Mathf.Abs(pixelsPerMs - _pixelsPerMs) > 0.001f)
            {
                _pixelsPerMs = pixelsPerMs;
            }
        }

        private void DrawTimeline()
        {
            if (CurrentSkill == null) return;

            float trackAreaY = 130f;
            float timelineW = CurrentSkill.Duration * _pixelsPerMs + 40f;
            float viewW = _centerPanel.width - 20f;
            float totalW = Mathf.Max(timelineW, viewW);
            float totalH = 200f;

            _timelineScroll = GUI.BeginScrollView(
                new Rect(10, trackAreaY, viewW, totalH),
                _timelineScroll,
                new Rect(0, 0, totalW, totalH));

            // 时间轴标尺
            DrawTimelineRuler(CurrentSkill.Duration, timelineW);

            // 事件轨道
            float trackY = 30f;
            GUI.Box(new Rect(20, trackY, timelineW - 40, _trackHeight), "");
            DrawEventBlocks(CurrentSkill, trackY);

            // 拖拽预览
            if (_dragEventIndex >= 0)
            {
                var evt = Event.current;
                if (evt.type == EventType.MouseUp && evt.button == 0)
                {
                    _dragEventIndex = -1;
                    evt.Use();
                }
            }

            GUI.EndScrollView();
        }

        private void DrawTimelineRuler(int durationMs, float totalW)
        {
            float startX = 20f;
            float rulerY = 10f;
            float stepPx = 100f * _pixelsPerMs; // 每 100ms 一格

            // 时间刻度线
            for (int ms = 0; ms <= durationMs; ms += 100)
            {
                float x = startX + ms * _pixelsPerMs;
                Handles.color = Color.gray;
                Handles.DrawLine(new Vector3(x, rulerY), new Vector3(x, rulerY + 18f));
                if (ms % 500 == 0) Handles.DrawLine(new Vector3(x, rulerY), new Vector3(x, rulerY + 22f));
                Handles.color = Color.white;

                GUI.Label(new Rect(x - 15, rulerY - 2, 30, 14), $"{ms}", EditorStyles.miniLabel);
            }
        }

        private void DrawEventBlocks(SkillConfig skill, float trackY)
        {
            if (skill.SkillEventIds == null) return;

            for (int i = 0; i < skill.SkillEventIds.Length; i++)
            {
                float x = 20f + skill.SkillEventTimestamps[i] * _pixelsPerMs;
                float w = 80f;

                var eventCfg = FindEventConfig(skill.SkillEventIds[i]);
                string label = eventCfg != null ? ((SkillEventType)eventCfg.EventType).ToString() : "?";

                Color blockColor = (SkillEventType)eventCfg.EventType switch
                {
                    SkillEventType.PlayAnimation => new Color(0.3f, 0.7f, 1.0f),
                    SkillEventType.SpawnVFX => new Color(1.0f, 0.6f, 0.2f),
                    SkillEventType.ApplyValue => new Color(1.0f, 0.3f, 0.3f),
                    SkillEventType.AddBuff => new Color(0.5f, 1.0f, 0.3f),
                    SkillEventType.FindTarget => new Color(0.8f, 0.5f, 1.0f),
                    SkillEventType.SpawnBullet => new Color(0.9f, 0.8f, 0.2f), // 金色
                    _ => Color.gray,
                };

                bool isSelected = i == _selectedEventIndex;
                if (isSelected) blockColor = Color.white;

                Rect blockRect = new Rect(x, trackY + 4, w, _trackHeight - 8);
                EditorGUI.DrawRect(blockRect, blockColor);

                // 事件名
                var style = new GUIStyle(EditorStyles.miniLabel)
                {
                    alignment = TextAnchor.MiddleCenter,
                    normal = { textColor = Color.white },
                };
                GUI.Label(blockRect, label, style);

                // 点击选中
                var evt = Event.current;
                if (evt.type == EventType.MouseDown && blockRect.Contains(evt.mousePosition))
                {
                    if (evt.button == 0)
                    {
                        _selectedEventIndex = i;
                        _dragStartX = evt.mousePosition.x - blockRect.x;
                        _dragEventIndex = i;
                        evt.Use();
                        Repaint();
                    }
                }

                // 拖拽移动时间戳
                if (_dragEventIndex == i && evt.type == EventType.MouseDrag)
                {
                    float newX = evt.mousePosition.x - _dragStartX;
                    int newMs = Mathf.Clamp(Mathf.RoundToInt((newX - 20f) / _pixelsPerMs), 0, skill.Duration);
                    skill.SkillEventTimestamps[i] = newMs;
                    evt.Use();
                    Repaint();
                }
            }
        }

        // ═══════════════════════════════════
        // 右侧 — 事件属性编辑
        // ═══════════════════════════════════

        private void DrawRightPanel()
        {
            GUILayout.BeginArea(_rightPanel, EditorStyles.helpBox);
            EditorGUILayout.LabelField("事件属性", EditorStyles.boldLabel);

            if (CurrentSkill == null || CurrentSkill.SkillEventIds == null || _selectedEventIndex < 0
                || _selectedEventIndex >= CurrentSkill.SkillEventIds.Length)
            {
                EditorGUILayout.LabelField("请选中时间轴上的事件", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandHeight(true));
                GUILayout.EndArea();
                return;
            }

            int eventId = CurrentSkill.SkillEventIds[_selectedEventIndex];
            int timestamp = CurrentSkill.SkillEventTimestamps[_selectedEventIndex];
            var eventCfg = FindEventConfig(eventId);
            if (eventCfg == null)
            {
                EditorGUILayout.LabelField($"事件配置 {eventId} 未找到", EditorStyles.centeredGreyMiniLabel, GUILayout.ExpandHeight(true));
                GUILayout.EndArea();
                return;
            }

            EditorGUILayout.Space(4);

            // 时间戳
            int newTs = EditorGUILayout.IntField("时间戳(ms)", timestamp);
            if (newTs != timestamp) CurrentSkill.SkillEventTimestamps[_selectedEventIndex] = Mathf.Clamp(newTs, 0, CurrentSkill.Duration);

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField($"事件类型: {(SkillEventType)eventCfg.EventType}", EditorStyles.boldLabel);

            // 根据事件类型展示不同的参数编辑
            DrawEventTypeParams(eventCfg);

            EditorGUILayout.Space(10);
            if (GUILayout.Button("删除此事件", GUILayout.Height(24)))
            {
                RemoveEvent(_selectedEventIndex);
            }

            GUILayout.EndArea();
        }

        private void DrawEventTypeParams(SkillEventConfig cfg)
        {
            // 事件类型下拉选择
            cfg.EventType = (int)(SkillEventType)EditorGUILayout.EnumPopup("事件类型", (SkillEventType)cfg.EventType);

            EditorGUILayout.Space(4);
            EditorGUILayout.LabelField("参数", EditorStyles.boldLabel);

            // 始终展示三个通用参数
            cfg.IntParam1 = EditorGUILayout.IntField("IntParam1", cfg.IntParam1);
            cfg.IntParam2 = EditorGUILayout.IntField("IntParam2", cfg.IntParam2);
            cfg.StringParam = EditorGUILayout.TextField("StringParam", cfg.StringParam);

            // 参数含义提示
            ShowParamHint((SkillEventType)cfg.EventType);
        }

        private static void ShowParamHint(SkillEventType eventType)
        {
            string hint = eventType switch
            {
                SkillEventType.PlayAnimation => "StringParam = 动画名",
                SkillEventType.FindTarget => "IntParam1 = 目标类型",
                SkillEventType.SpawnVFX => "IntParam1 = X偏移 | IntParam2 = Y偏移 | StringParam = 特效路径",
                SkillEventType.ApplyValue => "IntParam1 = 数值 | IntParam2 = 0伤害/1治疗",
                SkillEventType.AddBuff => "IntParam1 = BuffConfigId",
                SkillEventType.SpawnBullet => "IntParam1 = BulletConfigId（指向BulletConfig表）",
                _ => "",
            };
            if (!string.IsNullOrEmpty(hint))
            {
                EditorGUILayout.LabelField(hint, EditorStyles.miniLabel);
            }
        }

        // ═══════════════════════════════════
        // 事件操作
        // ═══════════════════════════════════

        private void AddEvent(int defaultTimestamp)
        {
            if (CurrentSkill == null) return;

            // 创建新的事件配置，默认类型为 PlayAnimation
            int newEventId = GetNextEventId();
            var newEvent = new SkillEventConfig
            {
                Id = newEventId,
                EventType = (int)SkillEventType.PlayAnimation,
            };

            AddEventToConfig(newEvent);

            int[] newIds = AppendInt(CurrentSkill.SkillEventIds, newEventId);
            int[] newTs = AppendInt(CurrentSkill.SkillEventTimestamps, defaultTimestamp);
            CurrentSkill.SkillEventIds = newIds;
            CurrentSkill.SkillEventTimestamps = newTs;
            _selectedEventIndex = newIds.Length - 1;
        }

        private void RemoveEvent(int index)
        {
            if (CurrentSkill == null) return;
            var skill = CurrentSkill;

            skill.SkillEventIds = RemoveAt(skill.SkillEventIds, index);
            skill.SkillEventTimestamps = RemoveAt(skill.SkillEventTimestamps, index);
            if (_selectedEventIndex >= skill.SkillEventIds.Length)
                _selectedEventIndex = skill.SkillEventIds.Length - 1;
        }

        private static int GetNextEventId()
        {
            var inst = SkillEventConfigCategory.Instance;
            if (inst == null) return 1;
            var dict = inst.GetAll();
            if (dict == null) return 1;
            int maxId = 0;
            foreach (var kv in dict) { if (kv.Key > maxId) maxId = kv.Key; }
            return maxId + 1;
        }

        private static void AddEventToConfig(SkillEventConfig cfg)
        {
            var inst = SkillEventConfigCategory.Instance;
            if (inst == null) return;
            var dict = inst.GetAll();
            dict[cfg.Id] = cfg;
        }

        private static SkillEventConfig FindEventConfig(int eventId)
        {
            var inst = SkillEventConfigCategory.Instance;
            if (inst == null) return null;
            var dict = inst.GetAll();
            SkillEventConfig cfg = null;
            dict?.TryGetValue(eventId, out  cfg);
            return cfg;
        }

        private static int[] AppendInt(int[] arr, int val)
        {
            var list = new List<int>(arr ?? System.Array.Empty<int>()) { val };
            return list.ToArray();
        }

        private static int[] RemoveAt(int[] arr, int index)
        {
            var list = new List<int>(arr ?? System.Array.Empty<int>());
            if (index >= 0 && index < list.Count) list.RemoveAt(index);
            return list.ToArray();
        }
    }
}