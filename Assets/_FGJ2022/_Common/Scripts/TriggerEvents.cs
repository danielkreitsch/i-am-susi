using UnityEngine;
using UnityEngine.Events;

namespace Game.Common {
    sealed class TriggerEvents : MonoBehaviour {
        [SerializeField]
        UnityEvent<GameObject> onTriggerEnter = new();
        [SerializeField]
        UnityEvent<GameObject> onTriggerStay = new();
        [SerializeField]
        UnityEvent<GameObject> onTriggerExit = new();

        void OnTriggerEnter(Collider other) {
            onTriggerEnter.Invoke(other.gameObject);
        }
        void OnTriggerStay(Collider other) {
            onTriggerStay.Invoke(other.gameObject);
        }
        void OnTriggerExit(Collider other) {
            onTriggerExit.Invoke(other.gameObject);
        }
    }
}
