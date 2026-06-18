using System;
using System.Collections.Generic;
using System.Linq;
using FairyGUI;

namespace ET
{
    public sealed class ViewController : Singleton<ViewController>, ISingletonAwake
    {
        private sealed class MutipileViewPanel<T> : ViewPanel<T> where T : GComponent
        {
            public override Type GType => typeof(T);

            public override void Init()
            {
            }

            public override async ETTask InitAsync()
            {
                foreach (GComponent item in this.GComponent.GetChildren().Where(obj => obj is GComponent).Cast<GComponent>())
                {
                    await Instance.InitPanel(item);
                }
            }

            public override void Reload()
            {
                throw new NotImplementedException();
            }
        }

        public readonly Dictionary<Type, Type> ViewTypes = new Dictionary<Type, Type>();
        public readonly Dictionary<string, Type> NameOfPanelType = new Dictionary<string, Type>();
        public readonly Dictionary<string, Type> NameOfGObjectType = new Dictionary<string, Type>();

        public Scene RootScene { get; set; }
        private readonly HashSet<Type> ManualTypes = new HashSet<Type>();
        private readonly List<ViewPanel> DontReload = new List<ViewPanel>();
        private readonly Dictionary<Type, ViewPanel> TypeOfPanel = new Dictionary<Type, ViewPanel>();
        private readonly Dictionary<Type, ViewPanel> GTypeOfPanel = new Dictionary<Type, ViewPanel>();

        public void Awake()
        {
            this.LoadType();
            this.LoadData();
            this.ReinitAll();
        }

        private void LoadType()
        {
            this.ViewTypes.Clear();
            this.NameOfPanelType.Clear();
            this.NameOfGObjectType.Clear();
            HashSet<Type> types = CodeTypes.Instance.GetTypes(typeof(ViewMethodAttribute));
            types.Where(type => !type.IsGenericType).Foreach(type =>
            {
                ViewPanel panel = Activator.CreateInstance(type) as ViewPanel;
                this.ViewTypes.Add(panel.GType, type);
                this.NameOfPanelType.Add(type.Name, type);
                this.NameOfGObjectType.Add(panel.GType.Name, panel.GType);
            });
        }

        private void LoadData()
        {
            if (Instance != null)
            {
                this.RootScene = Instance.RootScene;
                Instance.ManualTypes.Foreach(item => this.ManualTypes.Add(item));
                Instance.DontReload.Foreach(item => this.DontReload.Add(item));
                Instance.TypeOfPanel.Foreach(item => this.TypeOfPanel.Add(item.Key, item.Value));
                Instance.GTypeOfPanel.Foreach(item => this.GTypeOfPanel.Add(item.Key, item.Value));
            }
        }

        public async ETTask<ViewPanel> InitPanel<T>(T gObject, bool manual = false) where T : GComponent
        {
            if (gObject is null)
            {
                Log.Error("初始化的FObject为null");
                return null;
            }
            Dictionary<Type, Type> viewTypes = this.ViewTypes;
            if (viewTypes.TryGetValue(gObject.GetType(), out Type type))
            {
                try
                {
                    ViewPanel panel = Activator.CreateInstance(type) as ViewPanel;
                    panel.Controller = this;
                    panel.GComponent = gObject;
                    panel.Init();
                    await panel.InitAsync();
                    this.TypeOfPanel.Add(type, panel);
                    this.GTypeOfPanel.Add(panel.GType, panel);
                    if (manual)
                        this.ManualTypes.Add(panel.GType);
                    return panel;
                }
                catch (Exception e)
                {
                    Log.Error(e);
                }
            }
            return null;
        }

        public async ETTask InitChildren<T>(T gObject) where T : GComponent
        {
            MutipileViewPanel<T> panel = new MutipileViewPanel<T>();
            panel.Controller = this;
            panel.GComponent = gObject;
            await panel.InitAsync();
            this.TypeOfPanel.Add(panel.GetType(), panel);
            this.GTypeOfPanel.Add(panel.GType, panel);
            this.DontReload.Add(panel);
        }

        public void ReinitAll()
        {
            if (Instance is null)
                return;
            using ListComponent<GComponent> gObjects = ListComponent<GComponent>.Create();
            this.TypeOfPanel.Values.Foreach(item => gObjects.Add(item.GComponent));
            this.TypeOfPanel.Clear();
            this.GTypeOfPanel.Clear();
            this.DontReload.Foreach(item => this.TypeOfPanel.Add(item.GetType(), item));
            this.DontReload.Foreach(item => this.GTypeOfPanel.Add(item.GType, item));
            foreach (GComponent gObject in gObjects)
            {
                Dictionary<Type, Type> viewTypes = this.ViewTypes;
                if (viewTypes.TryGetValue(gObject.GetType(), out Type type))
                {
                    try
                    {
                        ViewPanel panel = Activator.CreateInstance(type) as ViewPanel;
                        panel.Controller = this;
                        panel.GComponent = gObject;
                        panel.Reload();
                        this.TypeOfPanel.Add(type, panel);
                        this.GTypeOfPanel.Add(panel.GType, panel);
                    }
                    catch (Exception e)
                    {
                        Log.Error(e);
                    }
                }
            }
        }

        public ViewPanel Get(string type)
        {
            this.TypeOfPanel.TryGetValue(this.GetPanelType(type), out ViewPanel panel);
            return panel;
        }

        public T Get<T>() where T : ViewPanel
        {
            this.TypeOfPanel.TryGetValue(typeof(T), out ViewPanel panel);
            return panel as T;
        }

        public T Show<T>() where T : ViewPanel
        {
            T panel = this.Get<T>();
            panel?.Show();
            return panel;
        }

        public T Show<T>(object args) where T : ViewPanel
        {
            T panel = this.Get<T>();
            panel?.Show(args);
            return panel;
        }

        public void Hide<T>() where T : ViewPanel
        {
            this.Get<T>()?.Hide();
        }

        public void Close<T>() where T : ViewPanel
        {
            this.Get<T>()?.Close();
        }

        public void Remove(Type type)
        {
            if (this.TypeOfPanel.Remove(type, out ViewPanel viewPanel))
            {
                this.GTypeOfPanel.Remove(viewPanel.GType);
                viewPanel.Dispose();
            }
        }

        public void Remove(string type)
        {
            this.Remove(this.GetPanelType(type));
        }

        public void Remove<T>() where T : ViewPanel
        {
            this.Remove(typeof(T));
        }

        public ViewPanel GetByGType(string type)
        {
            this.GTypeOfPanel.TryGetValue(this.GetGObjectType(type), out ViewPanel viewPanel);
            return viewPanel;
        }

        public ViewPanel<T> GetByGType<T>() where T : GComponent
        {
            this.GTypeOfPanel.TryGetValue(typeof(T), out ViewPanel viewPanel);
            return viewPanel as ViewPanel<T>;
        }

        public ViewPanel<T> ShowByGType<T>() where T : GComponent
        {
            ViewPanel<T> panel = this.GetByGType<T>();
            panel.Show();
            return panel;
        }

        public ViewPanel<T> ShowByGType<T>(object args) where T : GComponent
        {
            ViewPanel<T> panel = this.GetByGType<T>();
            panel.Show(args);
            return panel;
        }

        public ViewPanel<T> HideByGType<T>() where T : GComponent
        {
            ViewPanel<T> panel = this.GetByGType<T>();
            panel.Hide();
            return panel;
        }

        public ViewPanel<T> CloseByGType<T>() where T : GComponent
        {
            ViewPanel<T> panel = this.GetByGType<T>();
            panel.Close();
            return panel;
        }

        public void RemoveByGType(Type type)
        {
            if (this.GTypeOfPanel.Remove(type, out ViewPanel viewPanel))
            {
                this.TypeOfPanel.Remove(viewPanel.GetType());
                viewPanel.Dispose();
            }
        }

        public void RemoveByGType(string type)
        {
            this.RemoveByGType(this.GetGObjectType(type));
        }

        public void RemoveByGType<T>() where T : GComponent
        {
            this.RemoveByGType(typeof(T));
        }

        public void HideAll()
        {
            this.TypeOfPanel.Values.Where(item => item is not IViewWindow).Foreach(item => item.Hide());
        }

        public void CloseAll()
        {
            this.TypeOfPanel.Values.Where(item => item is not IViewWindow).Foreach(item => item.Close());
        }

        public void RemoveAll()
        {
            using ListComponent<Type> list = ListComponent<Type>.Create();
            list.AddRange(this.GTypeOfPanel.Keys.Where(item => !this.ManualTypes.Contains(item)));
            list.Foreach(item => this.RemoveByGType(item));
        }

        private Type GetPanelType(string type)
        {
            return this.NameOfPanelType.TryGetValue(type, out Type panelType) ? panelType : default;
        }

        private Type GetGObjectType(string type)
        {
            return this.NameOfGObjectType.TryGetValue(type, out Type gObjectType) ? gObjectType : default;
        }

        protected override void Destroy()
        {
            this.RootScene = null;
            this.ManualTypes.Clear();
            this.DontReload.Clear();
            this.TypeOfPanel.Clear();
            this.GTypeOfPanel.Values.ToList().Foreach(item => item.Dispose());
            this.GTypeOfPanel.Clear();
            base.Destroy();
        }
    }
}