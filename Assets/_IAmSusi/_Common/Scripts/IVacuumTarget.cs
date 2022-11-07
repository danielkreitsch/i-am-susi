using UnityEngine;

namespace Game {
    public interface IVacuumTarget {
        public Transform transform { get; }
        public void GetSuckedInBy(GameObject vacuum);
    }
}
