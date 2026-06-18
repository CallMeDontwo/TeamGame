using UnityEngine;

namespace ET
{
    [EnableClass]
    public class ObjectSpawner : MonoBehaviour
    {
        public GameObject GameObject;

        public GameObject Instantiate(bool setAsChild)
        {
            GameObject clone = this.GameObject.Instantiate();
            if (setAsChild)
            {
                clone.transform.SetParent(this.transform);
            }
            return clone;
        }

        public void SetObject(GameObject obj)
        {
            this.GameObject = obj;
        }
    }
}