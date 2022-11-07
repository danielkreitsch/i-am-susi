using Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Venting {
    public sealed class VentingInCamera : MonoBehaviour {
        [Inject]
        VentingManager ventingManager;

        [SerializeField]
        CinemachineVirtualCamera cinemachineVirtualCamera;

        void Start() {
            ventingManager.OnCurrentIntakeVentChanged += OnCurrentVentChanged;
        }

        void OnCurrentVentChanged() {
            var currentVent = ventingManager.CurrentIntakeVent;
            cinemachineVirtualCamera.Follow = currentVent.CameraTransform;
            cinemachineVirtualCamera.LookAt = currentVent.Transform;
        }
    }
}