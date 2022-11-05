using Game.Avatar.SpiderImpl;
using MyBox;
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
        float jumpSpeed = 5;
        [SerializeField]
        float dashSpeed = 5;

        [Header("Input")]
        [SerializeField]
        public Vector2 movementInput;
        [SerializeField]
        Vector3 movementAcceleration = Vector3.zero;

        [SerializeField, ReadOnly]
        public bool intendsJump = false;
        [SerializeField, ReadOnly]
        public bool intendsJumpStart = false;
        bool canJump => attachedMotor.isGrounded;
        public bool TryConsumeJumpStart() {
            if (intendsJumpStart) {
                intendsJumpStart = false;
                return true;
            }
            return false;
        }

        [SerializeField, ReadOnly]
        bool canDash = false;
        [SerializeField, ReadOnly]
        public bool intendsDash = false;
        [SerializeField, ReadOnly]
        public bool intendsDashStart = false;
        public bool TryConsumeDashStart() {
            if (intendsDashStart) {
                intendsDashStart = false;
                return true;
            }
            return false;
        }

        Vector3 velocity {
            get => attachedMotor.movement;
            set => attachedMotor.movement = value;
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
        [SerializeField]
        Vector3 cameraInput;
        [SerializeField]
        Vector3 motorInput;

        void OnDrawGizmos() {
            Gizmos.color = Color.red.WithAlpha(0.5f);
            DrawVector3(cameraInput * 10);
            Gizmos.color = Color.green.WithAlpha(0.5f);
            DrawVector3(motorInput * 10);
            Gizmos.color = Color.blue.WithAlpha(0.5f);
            DrawVector3(velocity);
        }
        void DrawVector3(in Vector3 direction) {
            Gizmos.DrawLine(transform.position, transform.position + direction);
        }

        void FixedUpdate() {
            attachedMotor.targetRotation = Quaternion.FromToRotation(Vector3.up, attachedMotor.groundNormal);

            cameraInput = attachedCamera.TranslateInput(movementInput);

            motorInput = attachedMotor.TranslateMovement(cameraInput);

            var movement = motorInput * movementSpeed;

            velocity = Vector3.SmoothDamp(velocity, movement, ref movementAcceleration, movementTime);

            if (attachedMotor.isGrounded) {
                canDash = true;
            }

            if (canJump && TryConsumeJumpStart()) {
                velocity += attachedMotor.groundNormal * jumpSpeed;
            }

            if (canDash && TryConsumeDashStart()) {
                canDash = false;
                velocity += motorInput == Vector3.zero
                    ? spider.transform.forward * dashSpeed
                    : motorInput.normalized * dashSpeed;
            }
        }

        Quaternion TranslateRotation(Vector3 goalForward) {
            return Quaternion.FromToRotation(Vector3.up, goalForward.normalized);
        }

        public void ReceiveLaser(GameObject laser) {
            Debug.Log($"Got hit by laser {laser}!");
        }
        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength) {
            Debug.Log($"Got hit by vacuum {vacuum}!");
        }
    }
}
