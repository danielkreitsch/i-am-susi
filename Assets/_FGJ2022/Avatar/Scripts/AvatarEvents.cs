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
        UnityEvent<GameObject> onReceiveVacuum = new();

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!attachedMotor) {
                transform.TryGetComponentInChildren(out attachedMotor);
            }
        }

        public void GetHitBy(GameObject laser) {
            onReceiveLaser.Invoke(gameObject);
        }

        public void GetSuckedInBy(GameObject vacuum) {
            onReceiveVacuum.Invoke(gameObject);
        }
    }
}
