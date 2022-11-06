using UnityEngine;

namespace FGJ2022.Venting
{
    public interface IVent
    {
        public Transform Transform { get; }
        
        public Transform CameraTransform { get; }
    }
}