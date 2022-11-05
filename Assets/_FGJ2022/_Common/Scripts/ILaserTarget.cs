using UnityEngine;

namespace AssemblyCSharp {
    interface ILaserTarget {
        public void ReceiveLaser(GameObject laser);
    }
}
