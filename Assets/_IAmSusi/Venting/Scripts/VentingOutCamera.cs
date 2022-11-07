using Cinemachine;
using UnityEngine;
using Zenject;

namespace Game.Venting {
    public sealed class VentingOutCamera : MonoBehaviour {
        [Inject]
        VentingManager ventingManager;

        [SerializeField]
        CinemachineVirtualCamera cinemachineVirtualCamera;

        void Start() {
            ventingManager.OnCurrentOutletVentChanged += OnCurrentVentChanged;
        }

        void OnCurrentVentChanged() {
            var currentVent = ventingManager.CurrentOutletVent;
            cinemachineVirtualCamera.Follow = currentVent.CameraTransform;
            cinemachineVirtualCamera.LookAt = currentVent.Transform;
        }
    }
}