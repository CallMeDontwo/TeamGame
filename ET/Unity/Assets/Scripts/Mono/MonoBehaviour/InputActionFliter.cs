using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace ET
{
    [EnableClass]
    [Serializable]
    public class ActionFliter
    {
        public string ActionName;
        public InputActionPhase Phase;
        public UnityEvent<InputAction.CallbackContext> CallbackContext;
    }

    [EnableClass]
    public sealed class InputActionFliter : MonoBehaviour, ISerializationCallbackReceiver
    {
        public PlayerInput PlayerInput;
        public List<ActionFliter> Actions;
        [NonSerialized]
        private readonly MultiDictionary<string, InputActionPhase, ActionFliter> actionMaps = new MultiDictionary<string, InputActionPhase, ActionFliter>();

        private void Reset()
        {
            this.PlayerInput = this.GetComponent<PlayerInput>();
        }

        private void Awake()
        {
            if (this.PlayerInput)
            {
                switch (this.PlayerInput.notificationBehavior)
                {
                    case PlayerNotifications.SendMessages:
                        break;
                    case PlayerNotifications.BroadcastMessages:
                        break;
                    case PlayerNotifications.InvokeUnityEvents:
                        foreach (PlayerInput.ActionEvent item in this.PlayerInput.actionEvents)
                        {
                            item.RemoveListener(this.Invoke);
                            item.AddListener(this.Invoke);
                        }
                        break;
                    case PlayerNotifications.InvokeCSharpEvents:
                        break;
                }
            }
        }

        public void Invoke(InputAction.CallbackContext context)
        {
            if (this.actionMaps.TryGetValue(context.action.name, context.phase, out ActionFliter action))
            {
                action.CallbackContext?.Invoke(context);
            }
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            this.actionMaps.Clear();
            this.Actions?.ForEach(item => this.actionMaps.Add(item.ActionName, item.Phase, item));
        }
    }
}