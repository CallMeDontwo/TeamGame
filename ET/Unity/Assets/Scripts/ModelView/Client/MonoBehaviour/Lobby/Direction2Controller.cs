using UnityEngine;
using UnityEngine.Events;

namespace ET
{
    [EnableClass]
    public class Direction2Controller : MonoBehaviour
    {
        public UnityEvent Left;
        public UnityEvent Right;

        private int dir;

        public bool IsLeft
        {
            get => this.dir == 0;
            set
            {
                if (value)
                {
                    if (this.dir != 0)
                    {
                        this.dir = 0;
                        this.Left?.Invoke();
                    }
                }
                else
                {
                    this.dir = 1;
                    this.Right?.Invoke();
                }
            }
        }

        public bool IsRight
        {
            get => !this.IsLeft;
            set => this.IsLeft = !value;
        }

        public int Dir
        {
            get => this.dir;
            set => this.IsLeft = value == 0;
        }
    }
}