using System;
using Cinemachine;
using UnityEngine;
using Zenject;
using UnityObject = UnityEngine.Object;

namespace FGJ2022.Venting
{
    public sealed class VentingCamera : MonoBehaviour
    {
        [Inject]
        private VentingManager ventingManager;
        
        [SerializeField]
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        private void Start()
        {
            this.ventingManager.OnCurrentVentChanged += this.OnCurrentVentChanged;
        }

        private void OnCurrentVentChanged()
        {
            var currentVent = this.ventingManager.CurrentVent;
            this.cinemachineVirtualCamera.Follow = currentVent.CameraTransform;
            this.cinemachineVirtualCamera.LookAt = currentVent.Transform;
        }
    }
}