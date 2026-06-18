using UnityEngine;

namespace ET
{
    [EnableClass]
    public abstract class SceneMonoBehaviour : MonoBehaviour
    {
        public Scene MainScene { get; set; }
        public Scene CurtScene { get; set; }
    }
}