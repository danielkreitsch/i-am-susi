using Cinemachine;
using Slothsoft.UnityExtensions;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Avatar {
    sealed class AvatarCamera : MonoBehaviour, AxisState.IInputAxisProvider {
        [SerializeField]
        CinemachineVirtualCameraBase attachedCamera;
        [SerializeField]
        CinemachineBrain attachedBrain;
        [SerializeField]
        public Vector2 axisInput;
        [SerializeField]
        UnityEvent<GameObject> onTargetGone = new();

        public Vector3 right => attachedBrain.transform.right;
        public Vector3 up => attachedBrain.transform.up;
        public Vector3 forward => attachedBrain.transform.forward;

        public Transform cameraTarget {
            get => attachedCamera.Follow;
            set {
                if (attachedCamera) {
                    attachedCamera.Follow = value;
                    attachedCamera.LookAt = value;

                    if (!value) {
                        onTargetGone.Invoke(gameObject);
                    }
                }
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
            var right = attachedBrain.transform.right * input.x;
            var up = attachedBrain.transform.up * input.y;
            var forward = attachedBrain.transform.forward * input.z;
            return (right + up + forward).normalized * input.magnitude;
        }
    }
}
