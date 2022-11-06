using UnityEngine;

namespace FGJ2022.Venting
{
    public sealed class OutletVent : MonoBehaviour, IVent
    {
        [SerializeField]
        private Transform cameraTransform;
        
        public Transform Transform => this.transform;
        
        public Transform CameraTransform => this.CameraTransform;
    }
}