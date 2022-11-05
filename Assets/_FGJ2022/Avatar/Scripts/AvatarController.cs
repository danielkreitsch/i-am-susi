using Game.Avatar.SpiderImpl;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarController : MonoBehaviour {
        [SerializeField]
        Spider spider;
        [SerializeField]
        AvatarMotor attachedMotor;
        [SerializeField]
        CameraAbstract attachedCamera;

        [Header("Settings")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float movementTime = 1;

        [Header("Input")]
        [SerializeField]
        public Vector2 movementInput;
        [SerializeField]
        Vector3 movementAcceleration = Vector3.zero;

        Vector3 velocity {
            get => attachedMotor.velocity;
            set => attachedMotor.velocity = value;
        }

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!spider) {
                transform.TryGetComponentInParent(out spider);
            }
            if (!attachedMotor) {
                transform.TryGetComponentInParent(out attachedMotor);
            }
            if (!attachedCamera) {
                transform.TryGetComponentInParent(out attachedCamera);
            }
        }

        void FixedUpdate() {

            var movement = TranslateGroundInput(TranslateCameraInput(movementInput));

            movement *= movementSpeed;

            velocity = Vector3.SmoothDamp(velocity, movement, ref movementAcceleration, movementTime);
        }

        Vector2 TranslateCameraInput(Vector2 input) {
            var right = attachedCamera.transform.right * input.x;
            var forward = attachedCamera.transform.forward * input.y;
            return new Vector2(right.x + forward.x, right.z + forward.z).normalized * input.magnitude;
        }

        Vector3 TranslateGroundInput(Vector2 input) {
            return input.SwizzleXZ();
        }
    }
}
