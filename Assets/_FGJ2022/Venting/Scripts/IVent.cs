using UnityEngine;

namespace Game.Venting {
    public interface IVent {
        public Transform Transform { get; }

        public Transform CameraTransform { get; }

        /// <summary>
        /// Which way does this vent pull?
        /// </summary>
        public Vector3 VentDirection { get; }
    }
}