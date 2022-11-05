using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Avatar {
    sealed class AvatarEvents : MonoBehaviour, ILaserTarget, IVacuumTarget {
        [SerializeField]
        AvatarMotor attachedMotor;

        [SerializeField]
        UnityEvent<GameObject> onReceiveLaser = new();

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
            Debug.Log($"Got hit by laser {laser}!");
            onReceiveLaser.Invoke(gameObject);
        }

        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength) {
            Debug.Log($"Got hit by vacuum {vacuum}!");
            attachedMotor.dragDirection += pullDirection.normalized * strength;
        }
    }
}
