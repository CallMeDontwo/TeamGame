using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [EnableClass]
    [Serializable]
    public sealed class ObjectIdItem
    {
        public int Id;
        public GameObject Object;
    }

    [EnableClass]
    public sealed class ObjectIdMap : MonoBehaviour, IEnumerable<KeyValuePair<int, GameObject>>, ISerializationCallbackReceiver
    {
        public List<ObjectIdItem> Items;
        public Dictionary<int, GameObject>.KeyCollection Keys => this.map.Keys;
        public Dictionary<int, GameObject>.ValueCollection Values => this.map.Values;

        private readonly Dictionary<int, GameObject> map = new Dictionary<int, GameObject>();

        public GameObject Get(int id)
        {
            return this.map.TryGetValue(id, out GameObject obj) ? obj : null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.map.GetEnumerator();
        }

        IEnumerator<KeyValuePair<int, GameObject>> IEnumerable<KeyValuePair<int, GameObject>>.GetEnumerator()
        {
            return this.map.GetEnumerator();
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            this.map.Clear();
            this.Items?.ForEach(item => this.map.Add(item.Id, item.Object));
        }
    }
}