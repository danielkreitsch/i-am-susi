using UnityEngine;
using UnityEngine.Events;

namespace Game.Common {
    sealed class LaserEvents : MonoBehaviour, ILaserTarget {
        [SerializeField]
        UnityEvent<GameObject> onReceiveLaser = new();

        public void ReceiveLaser(GameObject laser) => onReceiveLaser.Invoke(gameObject);
    }
}
