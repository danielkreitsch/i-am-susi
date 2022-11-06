using System;
using UnityEngine;
using UnityObject = UnityEngine.Object;

namespace FGJ2022.Venting
{
    public sealed class IntakeVent : MonoBehaviour, IVent
    {
        [SerializeField]
        private Transform cameraTransform;
        
        public Transform Transform => this.transform;
        
        public Transform CameraTransform => this.CameraTransform;
    }
}