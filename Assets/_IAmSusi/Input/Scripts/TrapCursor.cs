using UnityEngine;

namespace Game.Input {
    sealed class TrapCursor : MonoBehaviour {
        void Start() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnDestroy() {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
