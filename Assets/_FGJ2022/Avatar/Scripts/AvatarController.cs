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
        float jumpSpeed = 5;

        [Header("Input")]
        [SerializeField]
        public Vector2 movementInput;
        [SerializeField]
        Vector3 movementAcceleration = Vector3.zero;

        public bool intendsToJump {
            get => m_intendsToJump;
            set {
                if (value) {
                    m_intendsToJump = true;
                    intendsToJumpStart = true;
                } else {
                    m_intendsToJump = false;
                    intendsToJumpStart = false;
                }
            }
        }
        [SerializeField]
        bool m_intendsToJump;

        public bool intendsToJumpStart {
            get => m_intendsToJumpStart;
            set => m_intendsToJumpStart = value;
        }
        [SerializeField]
        bool m_intendsToJumpStart;

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

            if (intendsToJumpStart) {
                intendsToJumpStart = false;
                if (attachedMotor.isGrounded) {
                    velocity += attachedMotor.groundNormal * jumpSpeed;
                }
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
