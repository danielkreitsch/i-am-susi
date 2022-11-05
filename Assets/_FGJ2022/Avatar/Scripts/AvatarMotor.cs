using Game.Avatar.SpiderImpl;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarMotor : MonoBehaviour {
        [SerializeField]
        Spider spider;
        [SerializeField]
        Rigidbody attachedRigidbody;
        [SerializeField]
        SphereCollider attachedCollider;
        [SerializeField]
        LayerMask collisionLayers = ~0;

        [Header("Physics")]
        [SerializeField]
        public Vector3 velocity = Vector3.zero;
        [SerializeField]
        public Vector3 dragDirection = Vector3.zero;
        [SerializeField]
        public Vector3 dragVelocity = Vector3.zero;
        [SerializeField]
        public float dragTime = 1;
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

        public Vector3 TranslateMovement(Vector3 movement) {
            return movement;
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
            rotation = targetRotation;

            velocity = Vector3.SmoothDamp(velocity, dragDirection, ref dragVelocity, dragTime);

            velocity += isGrounded
                ? -groundNormal * deltaTime
                : Physics.gravity * deltaTime;

            var direction = velocity.normalized;
            float distance = velocity.magnitude * deltaTime;

            raycastCount = Physics.SphereCastNonAlloc(
                position + center,
                radius,
                direction,
                raycastHits,
                distance,
                collisionLayers,
                QueryTriggerInteraction.Ignore
            );

            var newPosition = position + (direction * distance);

            var normalSum = Vector3.zero;
            int groundCount = 0;

            for (int i = 0; i < raycastCount; i++) {
                var hit = raycastHits[i];
                var collider = hit.collider;

                if (Physics.ComputePenetration(
                    attachedCollider, newPosition, rotation,
                    collider, collider.transform.position, collider.transform.rotation,
                    out var penetrationDirection, out float penetrationDistance)) {

                    normalSum += hit.normal;
                    groundCount++;

                    newPosition += penetrationDirection * penetrationDistance;
                }
            }

            isGrounded = groundCount > 0;
            groundNormal = groundCount > 0
                ? (normalSum / groundCount).normalized
                : Vector3.up;
            groundNormal = Vector3.up;

            position = newPosition;
        }
    }
}
