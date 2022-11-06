using UnityEngine;
using UnityEngine.Events;

namespace Game.Common {
    sealed class LaserEvents : MonoBehaviour, ILaserTarget {
        [SerializeField]
        UnityEvent<GameObject> onReceiveLaser = new();

        public void GetHitBy(GameObject laser) => onReceiveLaser.Invoke(gameObject);

#if UNITY_EDITOR
        [ContextMenu(nameof(ReceiveLaserNow))]
        public void ReceiveLaserNow() => GetHitBy(gameObject);
#endif
    }
}
