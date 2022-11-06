using UnityEngine;

namespace Game.Common {
    sealed class VacuumEvents : MonoBehaviour, IVacuumTarget {
        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength) {
        }
    }
}
