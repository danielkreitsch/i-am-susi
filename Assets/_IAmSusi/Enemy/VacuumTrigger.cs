using Game.Common;
using UnityEngine;

namespace Game.Enemy {
    sealed class VacuumTrigger : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<IVacuumTarget>(out var target) && target.IsValid()) {
                target.GetSuckedInBy(gameObject);
            }
        }
    }
}