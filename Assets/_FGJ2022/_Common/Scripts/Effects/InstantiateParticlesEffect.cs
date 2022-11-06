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
        bool parentToContext = false;

        [Space]
        [SerializeField]
        bool overrideColor = false;
        [SerializeField, ConditionalField(nameof(overrideColor))]
        ParticleSystem.MinMaxGradient colorGradient = new();

        [Space]
        [SerializeField]
        bool useMeshShape = false;
        [SerializeField, ConditionalField(nameof(useMeshShape))]
        ParticleSystemMeshShapeType meshShapeType = ParticleSystemMeshShapeType.Triangle;

        protected override void InvokeNow(GameObject context) {
            var particlesInstance = parentToContext
                ? Instantiate(particlesPrefab, context.transform.position, particlesPrefab.transform.rotation, context.transform)
                : Instantiate(particlesPrefab, context.transform.position, particlesPrefab.transform.rotation);

            if (overrideColor) {
                var main = particlesInstance.main;
                main.startColor = colorGradient;
            }

            if (useMeshShape && TryGetMesh(context, out var mesh)) {
                var shape = particlesInstance.shape;
                shape.mesh = mesh;
                shape.shapeType = ParticleSystemShapeType.Mesh;
                shape.meshShapeType = ParticleSystemMeshShapeType.Triangle;
                shape.scale = context.transform.lossyScale;
                shape.rotation = context.transform.eulerAngles;
            }
        }

        bool TryGetMesh(GameObject context, out Mesh mesh) {
            if (context.TryGetComponent<MeshFilter>(out var filter)) {
                mesh = filter.sharedMesh;
                return mesh;
            }
            if (context.TryGetComponent<MeshCollider>(out var collider)) {
                mesh = collider.sharedMesh;
                return mesh;
            }
            mesh = default;
            return false;
        }

    }
}
