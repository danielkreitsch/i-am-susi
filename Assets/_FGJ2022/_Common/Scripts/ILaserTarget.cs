using UnityEngine;

namespace Game {
    public interface ILaserTarget {
        public Transform transform { get; }

        public void GetHitBy(GameObject laser);
    }
}
