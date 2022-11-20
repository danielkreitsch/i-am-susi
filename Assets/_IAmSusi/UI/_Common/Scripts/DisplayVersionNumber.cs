using TMPro;
using UnityEngine;

namespace Game.Avatar {
    [ExecuteAlways]
    sealed class DisplayVersionNumber : MonoBehaviour {
        [SerializeField]
        TextMeshProUGUI text;

        void Update() {
            if (!text) {
                TryGetComponent(out text);
            }
            if (text) {
                text.text = Application.version;
            }
        }
    }
}
