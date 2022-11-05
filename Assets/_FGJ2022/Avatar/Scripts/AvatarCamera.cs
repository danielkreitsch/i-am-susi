using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarCamera : MonoBehaviour, AxisState.IInputAxisProvider {
        [SerializeField]
        CinemachineVirtualCameraBase attachedCamera;
        [SerializeField]
        CinemachineBrain attachedBrain;
        [SerializeField]
        public Vector2 axisInput;

        public Vector3 right => attachedCamera.transform.right;
        public Vector3 up => attachedCamera.transform.up;
        public Vector3 forward => attachedCamera.transform.forward;

        public Transform cameraTarget {
            get => attachedCamera.Follow;
            set {
                attachedCamera.Follow = value;
                attachedCamera.LookAt = value;
            }
        }

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!attachedCamera) {
                TryGetComponent(out attachedCamera);
            }
            if (!attachedBrain) {
                attachedBrain = FindObjectOfType<CinemachineBrain>();
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

        public Vector3 TranslateInput(in Vector2 input) {
            return TranslateInput(input.SwizzleXZ());
        }

        public Vector3 TranslateInput(in Vector3 input) {
            var right = attachedCamera.transform.right * input.x;
            var up = attachedCamera.transform.up * input.y;
            var forward = attachedCamera.transform.forward * input.z;
            return (right + up + forward).normalized * input.magnitude;
        }
    }
}
