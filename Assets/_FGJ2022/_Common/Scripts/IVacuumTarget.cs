using UnityEngine;

namespace Game {
    public interface IVacuumTarget {
        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength);
    }
}
