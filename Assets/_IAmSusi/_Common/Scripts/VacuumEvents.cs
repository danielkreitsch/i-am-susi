using UnityEngine;
using UnityEngine.Events;

namespace Game.Common {
    sealed class VacuumEvents : MonoBehaviour, IVacuumTarget {
        [SerializeField]
        UnityEvent<GameObject> onGetSuckedInBy = new();

        public void GetSuckedInBy(GameObject vacuum) => onGetSuckedInBy.Invoke(gameObject);

#if UNITY_EDITOR
        [ContextMenu(nameof(GetSuckedInByNow))]
        public void GetSuckedInByNow() => GetSuckedInBy(gameObject);
#endif
    }
}
