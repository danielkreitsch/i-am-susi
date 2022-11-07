using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Venting {
    sealed class VentZone : MonoBehaviour {
        [SerializeField]
        IntakeVent vent;

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!vent) {
                transform.TryGetComponentInParent(out vent);
            }
        }

        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IVentable>(out var ventable)) {
                ventable.StartVenting(vent, vent.outVent);
            }
        }
    }
}
