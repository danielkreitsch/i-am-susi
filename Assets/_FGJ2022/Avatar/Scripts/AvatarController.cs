using Game.Avatar.SpiderImpl;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Avatar {
    sealed class AvatarController : MonoBehaviour {
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
        [SerializeField]
        private float dashCooldown = 1;

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
        [SerializeField, ReadOnly]
        public float lastDashTime;
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

        bool dashIsOnCooldown => lastDashTime + dashCooldown > Time.realtimeSinceStartup;

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
            if (spider && attachedCamera) {
                attachedCamera.cameraTarget = spider.transform;
            }
        }
        void OnEnable() {
            attachedCamera.cameraTarget = spider.transform;
        }
        void OnDisable() {
            attachedCamera.cameraTarget = null;
        }

        [SerializeField]
        Vector3 cameraInput;
        [SerializeField]
        Vector3 motorInput;

        [Header("Events")]
        [SerializeField]
        UnityEvent<GameObject> onJump = new();
        [SerializeField]
        UnityEvent<GameObject> onDash = new();

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

            if (attachedMotor.isGrounded && !dashIsOnCooldown) {
                canDash = true;
            }

            if (canJump && TryConsumeJumpStart()) {
                velocity += attachedMotor.groundNormal * jumpSpeed;
                onJump.Invoke(gameObject);
            }

            if (canDash && TryConsumeDashStart()) {
                canDash = false;
                lastDashTime = Time.realtimeSinceStartup;
                velocity = motorInput == Vector3.zero
                    ? spider.transform.forward * dashSpeed
                    : motorInput.normalized * dashSpeed;
                onDash.Invoke(gameObject);
            }
        }

        Quaternion TranslateRotation(Vector3 goalForward) {
            return Quaternion.FromToRotation(Vector3.up, goalForward.normalized);
        }
    }
}
