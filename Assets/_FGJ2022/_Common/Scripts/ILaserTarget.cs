using UnityEngine;

namespace Game {
    public interface ILaserTarget
    {
        public Transform transform { get; }
        
        public void ReceiveLaser(GameObject laser);
    }
}
