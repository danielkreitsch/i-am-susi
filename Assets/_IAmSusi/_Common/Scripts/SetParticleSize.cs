using System;
using System.Linq;
using MyBox;
using UnityEngine;

namespace Game.Common {
    sealed class SetParticleSize : MonoBehaviour {
        [SerializeField]
        ParticleSystem particles;
        [SerializeField]
        Transform root;
        [SerializeField, ReadOnly]
        MeshRenderer[] renderers = Array.Empty<MeshRenderer>();
        [SerializeField, ReadOnly]
        Bounds bounds = new();
        [SerializeField]
        float particleDensity = 0.001f;
        [SerializeField, ReadOnly]
        int maxParticles = 0;

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!particles) {
                TryGetComponent(out particles);
            }
            if (root) {
                renderers = root.GetComponentsInChildren<MeshRenderer>()
                    .Where(r => r.gameObject.isStatic)
                    .ToArray();

                if (renderers.Length > 0) {
                    bounds = renderers[0].bounds;
                    foreach (var renderer in renderers) {
                        bounds.Encapsulate(renderer.bounds);
                    }
                    maxParticles = Mathf.RoundToInt(particleDensity * bounds.size.x * bounds.size.y * bounds.size.z);
                }
            }

            if (particles) {
                var main = particles.main;
                main.maxParticles = maxParticles;

                var emission = particles.emission;
                emission.rateOverTime = maxParticles / main.duration;

                var shape = particles.shape;
                shape.shapeType = ParticleSystemShapeType.Box;
                shape.position = bounds.center;
                shape.rotation = Vector3.zero;
                shape.scale = bounds.size;
            }
        }
    }
}
