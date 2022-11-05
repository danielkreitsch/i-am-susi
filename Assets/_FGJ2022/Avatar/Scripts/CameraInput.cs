using Game.Avatar.SpiderImpl;
using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Avatar {
    sealed class CameraInput : MonoBehaviour {
        [SerializeField]
        CameraAbstract attachedCamera;
        [SerializeField]
        float cameraVelocity = 1;

        Controls controls;

        void Awake() {
            OnValidate();
        }

        void OnValidate() {
            if (!attachedCamera) {
                TryGetComponent(out attachedCamera);
            }
        }

        void OnEnable() {
            controls = new();
            controls.Avatar.CameraPosition.performed += OnCameraPosition;
            controls.Enable();
        }

        void OnDisable() {
            controls.Disable();
            controls.Dispose();
        }

        void Update() {
            attachedCamera.axisInput += controls.Avatar.CameraVelocity.ReadValue<Vector2>() * cameraVelocity;
        }

        void OnCameraPosition(InputAction.CallbackContext context) {
            attachedCamera.axisInput += context.ReadValue<Vector2>();
        }
    }
}
