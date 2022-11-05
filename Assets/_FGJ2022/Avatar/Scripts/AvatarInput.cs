using Game.Input;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarInput : MonoBehaviour {
        [SerializeField]
        AvatarController attachedController;

        Controls controls;

        void Awake() {
            OnValidate();
        }

        void OnValidate() {
            if (!attachedController) {
                TryGetComponent(out attachedController);
            }
        }

        void OnEnable() {
            controls = new();
            controls.Enable();
        }

        void OnDisable() {
            controls.Disable();
            controls.Dispose();
        }

        void Update() {
            attachedController.movementInput = controls.Avatar.Move.ReadValue<Vector2>();
            attachedController.intendsToJump = controls.Avatar.Jump.IsPressed();
        }
    }
}
