using UnityEngine;

namespace ET
{
    [EnableClass]
    public class GameObjectUnitComponent : MonoBehaviour
    {
        private EntityWeakRef<Unit> unit;

        public Unit Unit
        {
            get => this.unit;
            set => this.unit = value;
        }
    }
}