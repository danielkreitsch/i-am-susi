using Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Venting {
    public sealed class VentingCamera : MonoBehaviour {
        [Inject]
        VentingManager ventingManager;

        [SerializeField]
        CinemachineVirtualCamera cinemachineVirtualCamera;

        void Start() {
            ventingManager.OnCurrentVentChanged += OnCurrentVentChanged;
        }

        void OnCurrentVentChanged() {
            var currentVent = ventingManager.CurrentVent;
            cinemachineVirtualCamera.Follow = currentVent.CameraTransform;
            cinemachineVirtualCamera.LookAt = currentVent.Transform;
        }
    }
}