using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace ET
{
    [EnableClass]
    public class EnvironmentStyle : MonoBehaviour
    {
        [Serializable]
        public class StyleElement : Object
        {
            public int Id;
            public string Name;
            public UnityEvent Event;
        }

        [Serializable]
        public class StyleGroup : Object
        {
            public string GroupName;
            public List<StyleElement> Styles = new List<StyleElement>();
        }

        public List<StyleElement> Styles = new List<StyleElement>();

        public void SetStyle(int id)
        {
            this.Styles.Find(style => style.Id == id)?.Event?.Invoke();
        }

        public void SetStyle(string name)
        {
            this.Styles.Find(style => style.Name == name)?.Event?.Invoke();
        }

        public void SetStyleByIndex(int index)
        {
            this.Styles[index].Event?.Invoke();
        }
    }
}