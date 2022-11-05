using Game.Avatar.SpiderImpl;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarMotor : MonoBehaviour {
        [Header("Settings")]
        [SerializeField]
        Spider spider;
        [SerializeField]
        Rigidbody attachedRigidbody;
        [SerializeField]
        SphereCollider attachedCollider;
        [SerializeField]
        LayerMask collisionLayers = ~0;

        [Space]
        [SerializeField]
        public float dragTime = 1;
        [SerializeField]
        public float turnSpeed = 1;

        [Header("Physics")]
        [SerializeField]
        public Vector3 movement = Vector3.zero;
        [SerializeField]
        public Vector3 velocity = Vector3.zero;
        [SerializeField]
        public Vector3 dragDirection = Vector3.zero;
        [SerializeField]
        public Vector3 dragVelocity = Vector3.zero;
        [SerializeField]
        public Quaternion targetRotation = Quaternion.identity;

        public bool isGrounded {
            get => spider.groundInfo.isGrounded;
            set => spider.groundInfo.isGrounded = value;
        }
        public Vector3 groundNormal {
            get => spider.groundInfo.groundNormal;
            set => spider.groundInfo.groundNormal = value;
        }
        Vector3 position {
            get => attachedRigidbody.position;
            set => attachedRigidbody.position = value;
        }
        public Quaternion rotation {
            get => attachedRigidbody.rotation;
            set => attachedRigidbody.rotation = value;
        }
        Vector3 center => attachedCollider.center;
        float radius => attachedCollider.radius;

        public Vector3 right => spider.transform.right;
        public Vector3 up => spider.transform.up;
        public Vector3 forward => spider.transform.forward;

        public Vector3 TranslateMovement(in Vector3 movement) {
            return targetRotation * movement;
        }

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!spider) {
                transform.TryGetComponentInParent(out spider);
            }
            if (!attachedRigidbody) {
                transform.TryGetComponentInParent(out attachedRigidbody);
            }
            if (!attachedCollider) {
                transform.TryGetComponentInParent(out attachedCollider);
            }
        }

        void OnEnable() {
            spider.onMove += Move;
        }
        void OnDisable() {
            spider.onMove += Move;
        }

        readonly RaycastHit[] raycastHits = new RaycastHit[128];
        int raycastCount;

        void Move(float deltaTime) {
            velocity += isGrounded
                ? Physics.gravity.magnitude * deltaTime * -groundNormal
                : Physics.gravity * deltaTime;

            velocity = Vector3.SmoothDamp(velocity, dragDirection, ref dragVelocity, dragTime);

            var totalVelocity = velocity + movement;
            var direction = totalVelocity.normalized;
            float distance = totalVelocity.magnitude * deltaTime;

            raycastCount = Physics.SphereCastNonAlloc(
                position + center - direction,
                radius,
                direction,
                raycastHits,
                distance + radius + 1,
                collisionLayers,
                QueryTriggerInteraction.Ignore
            );

            var newPosition = position + (direction * distance);

            var normalSum = Vector3.zero;
            int groundCount = 0;

            for (int i = 0; i < raycastCount; i++) {
                var hit = raycastHits[i];
                var collider = hit.collider;

                if (collider.Raycast(new Ray(hit.point + hit.normal, -hit.normal), out var info, float.PositiveInfinity)) {
                    normalSum += info.normal;
                    groundCount++;
                }

                if (Physics.ComputePenetration(
                    attachedCollider, newPosition, Quaternion.identity,
                    collider, collider.transform.position, collider.transform.rotation,
                    out var penetrationDirection, out float penetrationDistance)) {

                    newPosition += penetrationDirection * penetrationDistance;
                }
            }

            if (groundCount > 0) {
                isGrounded = true;
                groundNormal = (normalSum / groundCount).normalized;
                velocity = Vector3.zero;
            } else {
                isGrounded = false;
                groundNormal = Vector3.up;
            }

            position = newPosition;
            rotation = Quaternion.RotateTowards(rotation, targetRotation, turnSpeed);
        }
    }
}
