using UnityEngine;

namespace Game.Utility {
    public static class VectorExtensions {
        public static Vector2 xz(this Vector3 vv) {
            return new Vector2(vv.x, vv.z);
        }

        public static float FlatDistanceTo(this Vector3 from, Vector3 unto) {
            var a = from.xz();
            var b = unto.xz();
            return Vector2.Distance(a, b);
        }
    }
}