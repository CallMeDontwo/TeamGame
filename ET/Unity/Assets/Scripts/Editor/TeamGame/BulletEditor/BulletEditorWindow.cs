using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ET.TeamGame
{
    /// <summary>
    /// 子弹配置编辑器 — 直写 JSON 到 Assets/Bundles/Bullet/
    /// 菜单: Tools/子弹编辑器
    /// 无需 Play Mode，编辑即保存
    /// </summary>
    public class BulletEditorWindow : EditorWindow
    {
        private const string BULLETS_DIR = "Assets/Bundles/Bullet";
        private List<BulletData> _bullets = new();
        private int _selectedIdx = -1;
        private Vector2 _listScroll, _propScroll;

        // 新建
        private int _newId = 10001;
        private string _newName = "新子弹";

        // 过滤
        private string _searchFilter = "";

        [MenuItem("Tools/子弹编辑器")]
        public static void Open() => GetWindow<BulletEditorWindow>("子弹编辑器");

        private void OnEnable()
        {
            LoadAll();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();
            DrawList();
            DrawProps();
            EditorGUILayout.EndHorizontal();
        }

        // ═══════════════════════════════ 左侧：子弹列表 ═══════════════════════════════

        private void DrawList()
        {
            EditorGUILayout.BeginVertical(GUILayout.Width(240));

            EditorGUILayout.LabelField("子弹列表", EditorStyles.boldLabel);

            // 搜索
            _searchFilter = EditorGUILayout.TextField("搜索", _searchFilter);

            // 新建
            EditorGUILayout.BeginHorizontal();
            _newId = EditorGUILayout.IntField(_newId, GUILayout.Width(60));
            _newName = EditorGUILayout.TextField(_newName, GUILayout.Width(100));
            EditorGUILayout.EndHorizontal();
            if (GUILayout.Button("+ 新建") && _newId > 0)
            {
                Create(_newId, _newName);
                _newId++;
                _newName = "新子弹";
            }

            EditorGUILayout.Space();

            _listScroll = EditorGUILayout.BeginScrollView(_listScroll);
            for (int i = 0; i < _bullets.Count; i++)
            {
                var b = _bullets[i];

                // 过滤
                if (!string.IsNullOrEmpty(_searchFilter))
                {
                    string search = _searchFilter.ToLower();
                    if (!b.BulletName.ToLower().Contains(search) && !b.Id.ToString().Contains(search))
                        continue;
                }

                bool selected = i == _selectedIdx;
                GUI.backgroundColor = selected ? new Color(0.3f, 0.5f, 1f) : Color.white;
                string label = $"[{b.Id}] {b.BulletName}";
                if (GUILayout.Button(label, GUILayout.Height(24)))
                {
                    _selectedIdx = i;
                }
            }
            GUI.backgroundColor = Color.white;
            EditorGUILayout.EndScrollView();

            EditorGUILayout.Space();

            if (_selectedIdx >= 0 && GUILayout.Button("删除此子弹"))
            {
                Delete(_selectedIdx);
                _selectedIdx = -1;
            }

            if (GUILayout.Button("刷新列表"))
                LoadAll();

            EditorGUILayout.EndVertical();
        }

        // ═══════════════════════════════ 右侧：子弹属性 ═══════════════════════════════

        private void DrawProps()
        {
            EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));

            if (_selectedIdx < 0 || _selectedIdx >= _bullets.Count)
            {
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("选择左侧子弹编辑属性", EditorStyles.centeredGreyMiniLabel);
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndVertical();
                return;
            }

            var bullet = _bullets[_selectedIdx];

            // 标题行 + 按钮
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"子弹 {bullet.Id} — {bullet.BulletName}", EditorStyles.boldLabel, GUILayout.ExpandWidth(true));
            if (GUILayout.Button("保存", GUILayout.Width(60)))
                Save(bullet);
            if (GUILayout.Button("复制", GUILayout.Width(60)))
                Duplicate(bullet);
            EditorGUILayout.EndHorizontal();

            _propScroll = EditorGUILayout.BeginScrollView(_propScroll);

            // ---- 基础信息 ----
            EditorGUILayout.LabelField("基础信息", EditorStyles.miniBoldLabel);
            TwoColInt("ID", ref bullet.Id, 80, "名称", ref bullet.BulletName);
            TextField("描述", ref bullet.Desc, 300);

            EditorGUILayout.Space();

            // ---- 飞行属性 ----
            EditorGUILayout.LabelField("飞行属性", EditorStyles.miniBoldLabel);
            TwoColIntH("速度", ref bullet.Speed, 70, "/10000", "最大距离", ref bullet.MaxDistance, 70, "/10000");
            TwoColPopup("飞行类型", ref bullet.FlightType, new[] { "1-直线", "2-抛物线" }, new[] { 1, 2 },
                        "锁定追踪", ref bullet.IsHoming, new[] { "否", "是" }, new[] { 0, 1 });
            if (bullet.FlightType == 2)
            {
                if (bullet.FlightValue == null || bullet.FlightValue.Length < 1)
                    bullet.FlightValue = new int[] { 45 };
                IntFieldWithHint("发射角度", ref bullet.FlightValue[0], 60, "度");
            }

            EditorGUILayout.Space();

            // ---- 战斗属性 ----
            EditorGUILayout.LabelField("战斗属性", EditorStyles.miniBoldLabel);
            TwoColIntH("伤害", ref bullet.Damage, 70, "", "碰撞半径", ref bullet.CollisionRadius, 70, "/100");
            IntField("寻敌配置ID", ref bullet.TargetFinderId, 70);

            EditorGUILayout.Space();

            // ---- 发射偏移 ----
            EditorGUILayout.LabelField("发射偏移", EditorStyles.miniBoldLabel);
            TwoColIntH("X偏移", ref bullet.SpawnOffsetX, 70, "/100", "Y偏移", ref bullet.SpawnOffsetY, 70, "/100");

            EditorGUILayout.Space();

            // ---- 子弹类型 ----
            EditorGUILayout.LabelField("子弹类型", EditorStyles.miniBoldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("子弹类型", GUILayout.Width(55));
            bullet.BulletType = EditorGUILayout.IntPopup(bullet.BulletType,
                new[] { "1-普通", "2-弹射" }, new[] { 1, 2 }, GUILayout.Width(100));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            if (bullet.BulletType == 2)
            {
                if (bullet.BulletTypeValue == null || bullet.BulletTypeValue.Length < 2)
                    bullet.BulletTypeValue = new int[] { 3, 5000 };
                IntField("弹射次数", ref bullet.BulletTypeValue[0], 60);
                IntFieldWithHint("搜索半径", ref bullet.BulletTypeValue[1], 80, "/10000");
            }

            EditorGUILayout.Space();

            // ---- 视图 ----
            EditorGUILayout.LabelField("视图", EditorStyles.miniBoldLabel);
            TextField("预制体路径", ref bullet.PrefabPath, 300);

            EditorGUILayout.Space();

            // ---- 预览计算 ----
            DrawPreview(bullet);

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        // ═══════════════════════════════ 预览计算 ═══════════════════════════════

        private void DrawPreview(BulletData bullet)
        {
            EditorGUILayout.LabelField("预览计算", EditorStyles.miniBoldLabel);

            float speed = bullet.Speed / 10000f;
            float maxDist = bullet.MaxDistance / 10000f;
            float radius = bullet.CollisionRadius / 100f;

            EditorGUILayout.LabelField($"  实际速度: {speed:F3} 单位/秒");
            EditorGUILayout.LabelField($"  最大距离: {maxDist:F3} 单位");
            EditorGUILayout.LabelField($"  飞行时间: {(speed > 0 ? maxDist / speed : 0):F2} 秒");
            EditorGUILayout.LabelField($"  碰撞半径: {radius:F3} 单位");

            if (bullet.FlightType == 2 && bullet.FlightValue != null && bullet.FlightValue.Length > 0)
            {
                float angleDeg = bullet.FlightValue[0];
                float angleRad = angleDeg * Mathf.PI / 180f;
                float vx = speed * Mathf.Cos(angleRad);
                float vy = speed * Mathf.Sin(angleRad);
                EditorGUILayout.LabelField($"  水平速度: {vx:F3}, 垂直初速度: {vy:F3}");
                EditorGUILayout.LabelField($"  发射角度: {angleDeg}°");
            }
        }

        // ═══════════════════════════════ 便捷输入方法 ═══════════════════════════════

        private static void IntField(string label, ref int value, int width)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newVal = EditorGUILayout.IntField(value, GUILayout.Width(width));
            if (EditorGUI.EndChangeCheck()) value = newVal;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void IntFieldWithHint(string label, ref int value, int width, string hint)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int newVal = EditorGUILayout.IntField(value, GUILayout.Width(width));
            if (EditorGUI.EndChangeCheck()) value = newVal;
            if (!string.IsNullOrEmpty(hint))
                EditorGUILayout.LabelField(hint, GUILayout.Width(60));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void TextField(string label, ref string value, int width)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(55));
            value = EditorGUILayout.TextField(value ?? "", GUILayout.Width(width));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void TwoColInt(string l1, ref int v1, int w1, string l2, ref string v2)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(l1, GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int n1 = EditorGUILayout.IntField(v1, GUILayout.Width(w1));
            if (EditorGUI.EndChangeCheck()) v1 = n1;
            GUILayout.Space(15);
            EditorGUILayout.LabelField(l2, GUILayout.Width(55));
            v2 = EditorGUILayout.TextField(v2 ?? "", GUILayout.Width(100));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void TwoColIntH(string l1, ref int v1, int w1, string h1,
                                        string l2, ref int v2, int w2, string h2)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(l1, GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int n1 = EditorGUILayout.IntField(v1, GUILayout.Width(w1));
            if (EditorGUI.EndChangeCheck()) v1 = n1;
            if (!string.IsNullOrEmpty(h1))
                GUILayout.Label(h1, GUILayout.Width(50));
            GUILayout.Space(10);
            EditorGUILayout.LabelField(l2, GUILayout.Width(55));
            EditorGUI.BeginChangeCheck();
            int n2 = EditorGUILayout.IntField(v2, GUILayout.Width(w2));
            if (EditorGUI.EndChangeCheck()) v2 = n2;
            if (!string.IsNullOrEmpty(h2))
                GUILayout.Label(h2, GUILayout.Width(50));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private static void TwoColPopup(string l1, ref int v1, string[] d1, int[] val1,
                                         string l2, ref int v2, string[] d2, int[] val2)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(l1, GUILayout.Width(55));
            v1 = EditorGUILayout.IntPopup(v1, d1, val1, GUILayout.Width(100));
            GUILayout.Space(30);
            EditorGUILayout.LabelField(l2, GUILayout.Width(55));
            v2 = EditorGUILayout.IntPopup(v2, d2, val2, GUILayout.Width(60));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        // ═══════════════════════════════ 操作 ═══════════════════════════════

        private void Create(int id, string name)
        {
            if (_bullets.Any(b => b.Id == id))
            {
                EditorUtility.DisplayDialog("错误", $"子弹ID {id} 已存在", "确定");
                return;
            }

            var bullet = new BulletData
            {
                Id = id,
                BulletName = name,
                Speed = 50000,
                Damage = 10,
                FlightType = 1,
                FlightValue = new int[0],
                BulletType = 1,
                BulletTypeValue = new int[0],
                IsHoming = 0,
                MaxDistance = 80000,
                TargetFinderId = 0,
                CollisionRadius = 50,
                PrefabPath = "Bullet_Track",
                SpawnOffsetX = 0,
                SpawnOffsetY = 25,
            };
            _bullets.Add(bullet);
            _selectedIdx = _bullets.Count - 1;
            Save(bullet);
        }

        private void Duplicate(BulletData src)
        {
            // 找一个新 ID
            int newId = src.Id + 1;
            while (_bullets.Any(b => b.Id == newId)) newId++;

            var clone = new BulletData
            {
                Id = newId,
                BulletName = src.BulletName + "_副本",
                Desc = src.Desc,
                Speed = src.Speed,
                Damage = src.Damage,
                FlightType = src.FlightType,
                FlightValue = src.FlightValue?.ToArray(),
                BulletType = src.BulletType,
                BulletTypeValue = src.BulletTypeValue?.ToArray(),
                IsHoming = src.IsHoming,
                MaxDistance = src.MaxDistance,
                TargetFinderId = src.TargetFinderId,
                CollisionRadius = src.CollisionRadius,
                PrefabPath = src.PrefabPath,
                SpawnOffsetX = src.SpawnOffsetX,
                SpawnOffsetY = src.SpawnOffsetY,
            };
            _bullets.Add(clone);
            _selectedIdx = _bullets.Count - 1;
            Save(clone);
        }

        private void Delete(int idx)
        {
            var bullet = _bullets[idx];
            string path = GetPath(bullet.Id);
            if (File.Exists(path))
            {
                File.Delete(path);
                File.Delete(path + ".meta");
            }
            _bullets.RemoveAt(idx);
            AssetDatabase.Refresh();
        }

        // ═══════════════════════════════ 持久化 ═══════════════════════════════

        private void Save(BulletData bullet)
        {
            if (!Directory.Exists(BULLETS_DIR))
                Directory.CreateDirectory(BULLETS_DIR);

            string path = GetPath(bullet.Id);
            string json = JsonUtility.ToJson(bullet, true);
            File.WriteAllText(path, json);

            AssetDatabase.Refresh();
            Debug.Log($"BulletEditor: 已保存 {path}");
        }

        private void LoadAll()
        {
            _bullets.Clear();
            if (!Directory.Exists(BULLETS_DIR))
                Directory.CreateDirectory(BULLETS_DIR);

            var files = Directory.GetFiles(BULLETS_DIR, "bullet_*.json");
            foreach (var file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    var bullet = JsonUtility.FromJson<BulletData>(json);
                    if (bullet != null && bullet.Id > 0)
                        _bullets.Add(bullet);
                }
                catch (Exception e)
                {
                    Debug.LogError($"BulletEditor: 解析失败 {file}: {e.Message}");
                }
            }
            _bullets.Sort((a, b) => a.Id.CompareTo(b.Id));
            Debug.Log($"BulletEditor: 加载 {_bullets.Count} 个子弹");
        }

        private static string GetPath(int id) => $"{BULLETS_DIR}/bullet_{id}.json";
    }
}
