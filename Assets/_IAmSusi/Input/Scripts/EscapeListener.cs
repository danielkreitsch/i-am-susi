using UnityEngine;
using UnityEngine.Events;

namespace Game.Input {
    sealed class EscapeListener : MonoBehaviour {
        [SerializeField]
        UnityEvent onEscape = new();

        Controls controls;

        void OnEnable() {
            controls = new();
            controls.Avatar.Escape.started += _ => onEscape.Invoke();
            controls.Enable();
        }

        void OnDisable() {
            controls.Disable();
            controls.Dispose();
        }
    }
}
