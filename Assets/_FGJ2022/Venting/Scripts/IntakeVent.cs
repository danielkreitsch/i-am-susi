using UnityEngine;

namespace Game.Venting {
    public sealed class IntakeVent : MonoBehaviour, IVent {
        [SerializeField]
        public OutletVent outVent;

        [SerializeField]
        Transform cameraTransform;

        public Transform Transform => transform;

        public Transform CameraTransform => CameraTransform;

        public Vector3 VentDirection => -transform.forward;
    }
}