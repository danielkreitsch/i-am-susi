using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Avatar {
    sealed class AvatarEvents : MonoBehaviour, ILaserTarget, IVacuumTarget {
        [SerializeField]
        AvatarMotor attachedMotor;

        [SerializeField]
        UnityEvent<GameObject> onReceiveLaser = new();

        [SerializeField]
        float pullStrength = 1;

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!attachedMotor) {
                transform.TryGetComponentInChildren(out attachedMotor);
            }
        }

        public void ReceiveLaser(GameObject laser) {
            onReceiveLaser.Invoke(gameObject);
        }

        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength) {
            attachedMotor.dragVelocity += pullStrength * strength * pullDirection.normalized;
        }
    }
}
