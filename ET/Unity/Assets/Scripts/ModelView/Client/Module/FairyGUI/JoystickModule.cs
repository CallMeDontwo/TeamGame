using FairyGUI;
using UnityEngine;

namespace ET
{
    [EnableClass]
    public sealed class JoystickModule : EventDispatcher
    {
        public bool Fixed { get; set; }
        public int Radius { get; set; }
        public float AxisX { get; private set; }
        public float AxisY { get; private set; }
        public float Degree { get; private set; }
        public float Radian { get; private set; }
        public EventListener OnMove { get; private set; }
        public EventListener OnEnd { get; private set; }
        public bool Moving => this._button.selected;

        private int touchId;
        private float _startStageX;
        private float _startStageY;

        private readonly float _InitX;
        private readonly float _InitY;
        private readonly GObject _main;
        private readonly GObject _center;
        private readonly GButton _button;
        private readonly GObject _touchArea;

        public JoystickModule(GComponent mainView)
        {
            this.OnMove = new EventListener(this, "onMove");
            this.OnEnd = new EventListener(this, "onEnd");

            this._main = mainView;
            this._center = mainView.GetChild("joystick_center");
            this._button = mainView.GetChild("joystick").asButton;
            this._touchArea = mainView.GetChild("joystick_touch");
            this._button.changeStateOnClick = false;

            this.touchId = -1;
            this.Radius = 150;
            this._InitX = this._center.x + this._center.width / 2;
            this._InitY = this._center.y + this._center.height / 2;

            this._touchArea.onTouchBegin.Add(this.OnTouchBegin);
            this._touchArea.onTouchMove.Add(this.OnTouchMove);
            this._touchArea.onTouchEnd.Add(this.OnTouchEnd);
        }

        private void OnTouchBegin(EventContext context)
        {
            if (this.touchId == -1)
            {
                InputEvent evt = (InputEvent)context.data;
                this.touchId = evt.touchId;

                Vector2 pt = this._main.GlobalToLocal(new Vector2(evt.x, evt.y));

                this._startStageX = this.Fixed ? this._InitX : pt.x;
                this._startStageY = this.Fixed ? this._InitY : pt.y;

                this._button.selected = true;
                this._center.SetXY(this._startStageX - this._center.width / 2, this._startStageY - this._center.height / 2);
                this._button.SetXY(this._startStageX - this._button.width / 2, this._startStageY - this._button.height / 2);

                context.CaptureTouch();
            }
        }

        private void OnTouchMove(EventContext context)
        {
            InputEvent evt = (InputEvent)context.data;
            if (this.touchId != -1 && evt.touchId == this.touchId)
            {
                Vector2 pt = this._main.GlobalToLocal(new Vector2(evt.x, evt.y));
                float offsetX = pt.x - this._startStageX;
                float offsetY = this._startStageY - pt.y;

                this.Radian = Mathf.Atan2(offsetY, offsetX);
                this.Degree = this.Radian * 180 / Mathf.PI;
                this.AxisX = Mathf.Cos(this.Radian);
                this.AxisY = Mathf.Sin(this.Radian);

                float maxX = this.Radius * this.AxisX;
                float maxY = this.Radius * this.AxisY;
                if (Mathf.Abs(offsetX) > Mathf.Abs(maxX))
                    offsetX = maxX;
                if (Mathf.Abs(offsetY) > Mathf.Abs(maxY))
                    offsetY = maxY;

                this._button.SetXY(this._startStageX + offsetX - this._button.width / 2, this._startStageY - offsetY - this._button.height / 2);

                this.OnMove.Call(this);
            }
        }

        private void OnTouchEnd(EventContext context)
        {
            InputEvent inputEvt = (InputEvent)context.data;
            if (this.touchId != -1 && inputEvt.touchId == this.touchId)
            {
                this.AxisX = 0;
                this.AxisY = 0;
                this.Degree = 0;
                this.Radian = 0;
                this.touchId = -1;
                this._button.selected = false;
                this._button.SetXY(this._InitX - this._button.width / 2, this._InitY - this._button.height / 2);
                this._center.SetXY(this._InitX - this._center.width / 2, this._InitY - this._center.height / 2);
                this.OnEnd.Call(this);
            }
        }
    }
}