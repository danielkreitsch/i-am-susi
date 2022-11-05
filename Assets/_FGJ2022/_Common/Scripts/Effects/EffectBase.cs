using MyBox;
using UnityEngine;

namespace Game.Effects {
    abstract class EffectBase : ScriptableObject {
        [SerializeField, ReadOnly]
        EffectBase asset = default;

        protected virtual void OnValidate() {
            asset = this;
        }

        public abstract void Invoke(GameObject context);
    }
}
