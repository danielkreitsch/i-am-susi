using UnityEngine;

namespace Game.Common {
    public static class TargetExtensions {
        public static bool IsValid(this ILaserTarget target) {
            return target is MonoBehaviour component && component;
        }
        public static bool IsValid(this IVacuumTarget target) {
            return target is MonoBehaviour component && component;
        }
    }
}
