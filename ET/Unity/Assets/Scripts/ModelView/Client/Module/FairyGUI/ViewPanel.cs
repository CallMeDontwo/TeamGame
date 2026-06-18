using System;
using FairyGUI;

namespace ET
{
    public interface IViewWindow
    {
    }

    public abstract class ViewPanel : Object
    {
        public GComponent GComponent { get; internal set; }
        public ViewController Controller { get; internal set; }
        public Scene Scene => this.Controller.RootScene;
        public virtual bool Visable => this.GComponent.visible;
        public abstract Type GType { get; }
        public abstract void Init();
        public abstract ETTask InitAsync();

        public virtual void Reload()
        {
        }

        public virtual void Show()
        {
            this.GComponent.visible = true;
        }

        public virtual void Show(object args)
        {
            this.GComponent.visible = true;
        }

        public virtual void Hide()
        {
            this.GComponent.visible = false;
        }

        public virtual void Close()
        {
            this.GComponent.visible = false;
        }

        public virtual void Dispose()
        {
            this.GComponent.Dispose();
        }
    }

    [ViewMethod]
    public abstract class ViewPanel<T> : ViewPanel where T : GComponent
    {
        public T SelfUI => this.GComponent as T;
        public override Type GType => typeof(T);
        public override async ETTask InitAsync() => await ETTask.CompletedTask;
    }

    public abstract class ViewWindow<TWindow, TGComponent> : ViewPanel<TGComponent>, IViewWindow where TWindow : ViewPanel where TGComponent : GComponent
    {
        [StaticField]
        public static TWindow Instance => ViewController.Instance.GetByGType<TGComponent>() as TWindow;

        public Window Window { get; private set; }
        public override bool Visable => this.Window.isShowing;

        public override void Init()
        {
            Window window = new Window();
            window.contentPane = this.SelfUI;
            window.modal = true;
            window.sortingOrder = 100;
            this.Window = window;
        }

        public override void Show()
        {
            this.Window.Show();
        }

        public override void Hide()
        {
            this.Window.Hide();
        }

        public override void Close()
        {
            this.Window.Hide();
        }

        public override void Dispose()
        {
            base.Dispose();
            this.Window.Dispose();
        }
    }
}