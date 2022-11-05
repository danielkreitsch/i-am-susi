using Game.Avatar.SpiderImpl;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class SpiderMotor : MonoBehaviour {
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
        public Vector3 velocity = Vector3.zero;
        [SerializeField]
        public Vector3 drag = Vector3.zero;

        public bool isGrounded {
            get => spider.groundInfo.isGrounded;
            set => spider.groundInfo.isGrounded = value;
        }
        public Vector3 groundNormal {
            get => spider.groundInfo.groundNormal;
            set => spider.groundInfo.groundNormal = value;
        }
        Vector3 position3D {
            get => attachedRigidbody.position;
            set => attachedRigidbody.position = value;
        }
        Quaternion rotation3D {
            get => attachedRigidbody.rotation;
            set => attachedRigidbody.rotation = value;
        }
        Vector3 center => attachedCollider.center;
        float radius => attachedCollider.radius;

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
            velocity -= groundNormal * deltaTime;

            var direction = velocity.normalized;
            float distance = velocity.magnitude * deltaTime;

            raycastCount = Physics.SphereCastNonAlloc(
                position3D + center,
                radius,
                direction,
                raycastHits,
                distance,
                collisionLayers,
                QueryTriggerInteraction.Ignore
            );

            var newPosition = position3D + (direction * distance);

            var normalSum = Vector3.zero;
            int groundCount = 0;

            for (int i = 0; i < raycastCount; i++) {
                var hit = raycastHits[i];
                var collider = hit.collider;

                if (Physics.ComputePenetration(
                    attachedCollider, newPosition, rotation3D,
                    collider, collider.transform.position, collider.transform.rotation,
                    out var penetrationDirection, out float penetrationDistance)) {

                    normalSum += hit.normal;
                    groundCount++;

                    newPosition += penetrationDirection * penetrationDistance;
                }
            }

            isGrounded = groundCount > 0;
            groundNormal = groundCount > 0
                ? normalSum / groundCount
                : Vector3.up;

            position3D = newPosition;
        }
    }
}
