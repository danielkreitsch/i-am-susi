using UnityEngine;

namespace Game.Venting {
    public sealed class OutletVent : MonoBehaviour, IVent {
        [SerializeField]
        Transform cameraTransform;

        public Transform Transform => transform;

        public Transform CameraTransform => CameraTransform;

        public Vector3 VentDirection => transform.forward;
    }
}