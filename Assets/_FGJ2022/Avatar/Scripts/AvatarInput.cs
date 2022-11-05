using Game.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Avatar {
    sealed class AvatarInput : MonoBehaviour {
        [SerializeField]
        AvatarController attachedController;

        [Space]
        [SerializeField, Range(0, 10)]
        float jumpBufferDuration = 0;
        float jumpBufferTimer;
        [SerializeField, Range(0, 10)]
        float dashBufferDuration = 0;
        float dashBufferTimer;

        Controls controls;

        void Awake() {
            OnValidate();
            Cursor.lockState = CursorLockMode.Locked;
        }

        void OnValidate() {
            if (!attachedController) {
                TryGetComponent(out attachedController);
            }
        }

        void OnEnable() {
            controls = new();
            controls.Avatar.Jump.started += OnJump;
            controls.Avatar.Jump.performed += OnJump;
            controls.Avatar.Jump.canceled += OnJump;
            controls.Avatar.Dash.started += OnDash;
            controls.Avatar.Dash.performed += OnDash;
            controls.Avatar.Dash.canceled += OnDash;
            controls.Enable();
        }

        void OnDisable() {
            controls.Disable();
            controls.Dispose();
        }

        void Update() {
            attachedController.movementInput = controls.Avatar.Move.ReadValue<Vector2>();

            if (attachedController.intendsJumpStart) {
                if (jumpBufferTimer >= 0) {
                    jumpBufferTimer -= Time.deltaTime;
                } else {
                    attachedController.intendsJumpStart = false;
                }
            }
        }

        public void OnJump(InputAction.CallbackContext context) {
            if (context.started) {
                attachedController.intendsJumpStart = true;
                jumpBufferTimer = jumpBufferDuration;
            }
            attachedController.intendsJump = context.performed;
            if (context.canceled) {
                attachedController.intendsJumpStart = false;
            }
        }

        public void OnDash(InputAction.CallbackContext context) {
            if (context.started) {
                attachedController.intendsDashStart = true;
                dashBufferTimer = dashBufferDuration;
            }
            attachedController.intendsDash = context.performed;
            if (context.canceled) {
                attachedController.intendsDashStart = false;
            }
        }
    }
}
