using MyBox;
using UnityEngine;

namespace AssemblyCSharp {
    abstract class EffectBase : ScriptableObject {
        [SerializeField, ReadOnly]
        EffectBase asset = default;

        protected virtual void OnValidate() {
            asset = this;
        }

        public abstract void Invoke(GameObject context);
    }
}
