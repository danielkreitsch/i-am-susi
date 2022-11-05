using UnityEngine;

namespace AssemblyCSharp {
    interface IVacuumTarget {
        public void Apply(GameObject vacuum, Vector3 pullDirection, float strength);
    }
}
