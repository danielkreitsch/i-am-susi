using Game.Avatar.SpiderImpl;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarController : MonoBehaviour, ILaserTarget, IVacuumTarget {
        [SerializeField]
        Spider spider;
        [SerializeField]
        AvatarMotor attachedMotor;
        [SerializeField]
        AvatarCamera attachedCamera;

        [Header("Settings")]
        [SerializeField]
        float movementSpeed = 5;
        [SerializeField]
        float movementTime = 1;
        [SerializeField]
        float turnSpeed = 5;

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
                attachedCamera = FindObjectOfType<AvatarCamera>();
            }
        }
        void FixedUpdate() {
            var cameraInput = attachedCamera.TranslateInput(movementInput);
            var motorInput = attachedMotor.TranslateMovement(cameraInput);
            var movement = motorInput * movementSpeed;

            velocity = Vector3.SmoothDamp(velocity, movement, ref movementAcceleration, movementTime);

            if (cameraInput != Vector3.zero) {
                attachedMotor.targetRotation = TranslateRotation(cameraInput);
            }
        }

        Quaternion TranslateRotation(Vector3 goalForward) {
            goalForward = Vector3.ProjectOnPlane(goalForward, attachedMotor.groundNormal).normalized;

            return Quaternion.RotateTowards(attachedMotor.targetRotation, Quaternion.LookRotation(goalForward, attachedMotor.up), turnSpeed);
        }

        public void ReceiveLaser(GameObject laser) {
            Debug.Log($"Got hit by laser {laser}!");
        }
        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength) {
            Debug.Log($"Got hit by vacuum {vacuum}!");
        }
    }
}
