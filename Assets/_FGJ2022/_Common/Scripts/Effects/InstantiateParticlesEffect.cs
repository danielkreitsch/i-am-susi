using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Effects {
    [CreateAssetMenu]
    sealed class InstantiateParticlesEffect : EffectBase {
        [SerializeField, Expandable]
        ParticleSystem particlesPrefab;

        [Space]
        [SerializeField]
        bool overrideColor = false;
        [SerializeField, ConditionalField(nameof(overrideColor))]
        ParticleSystem.MinMaxGradient colorGradient = new();

        protected override void InvokeNow(GameObject context) {
            var particlesInstance = Instantiate(particlesPrefab, context.transform.position, particlesPrefab.transform.rotation);

            if (overrideColor) {
                var main = particlesInstance.main;
                main.startColor = colorGradient;
            }
        }
    }
}
