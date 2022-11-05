using Game;
using UnityEngine;

namespace FGJ2022.Cleaner {
    sealed class VacuumTrigger : MonoBehaviour {
        [SerializeField]
        AnimationCurve strengthOverDistance = AnimationCurve.Linear(0, 1, 0, 0);

        void OnTriggerStay(Collider other) {
            if (other.TryGetComponent<IVacuumTarget>(out var target)) {
                var pullDirection = (transform.position - other.transform.position).normalized;
                float pullDistance = Vector3.Distance(transform.position, other.transform.position);

                target.Apply(gameObject, pullDirection, strengthOverDistance.Evaluate(pullDistance));
            }
        }
    }
}