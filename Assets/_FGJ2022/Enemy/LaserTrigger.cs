using Game.Common;
using UnityEngine;

namespace Game.Enemy {
    sealed class LaserTrigger : MonoBehaviour {
        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<ILaserTarget>(out var target) && target.IsValid()) {
                target.GetHitBy(gameObject);
            }
        }
    }
}