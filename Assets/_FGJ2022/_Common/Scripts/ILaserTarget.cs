using UnityEngine;

namespace Game {
    public interface ILaserTarget {
        public void ReceiveLaser(GameObject laser);
    }
}
