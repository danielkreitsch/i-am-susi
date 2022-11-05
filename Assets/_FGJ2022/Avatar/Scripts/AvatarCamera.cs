using Cinemachine;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarCamera : MonoBehaviour, AxisState.IInputAxisProvider {
        [SerializeField]
        CinemachineBrain attachedCamera;
        [SerializeField]
        public Vector2 axisInput;

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!attachedCamera) {
                attachedCamera = FindObjectOfType<CinemachineBrain>();
            }
        }

        public float GetAxisValue(int axis) {
            if (axis == 0) {
                float value = axisInput.x;
                axisInput.x = 0;
                return value;
            } else {
                float value = axisInput.y;
                axisInput.y = 0;
                return value;
            }
        }

        public Vector3 TranslateInput(Vector2 input) {
            var right = attachedCamera.transform.right * input.x;
            var forward = attachedCamera.transform.forward * input.y;
            return (right + forward).normalized * input.magnitude;
        }
    }
}
