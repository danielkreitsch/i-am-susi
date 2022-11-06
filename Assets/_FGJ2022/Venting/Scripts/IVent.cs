using UnityEngine;

namespace Game.Venting {
    public interface IVent {
        public Transform Transform { get; }

        public Transform CameraTransform { get; }
    }
}