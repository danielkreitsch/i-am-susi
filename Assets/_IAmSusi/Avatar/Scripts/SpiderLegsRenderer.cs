using System;
using System.Linq;
using Game.Avatar.SpiderImpl;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    [ExecuteAlways]
    sealed class SpiderLegsRenderer : MonoBehaviour {
        [Serializable]
        struct Leg : IDisposable {
            public Transform transform;
            public IKChain chain;
            public LineRenderer renderer;

            public Leg(IKChain chain, Material material) {
                this.chain = chain;
                transform = chain.transform;
                if (!chain.TryGetComponent(out renderer)) {
                    renderer = chain.gameObject.AddComponent<LineRenderer>();
                }
                renderer.sharedMaterial = material;
            }

            public void Dispose() {
                if (renderer) {
                    if (Application.isPlaying) {
                        Destroy(renderer);
                    } else {
                        DestroyImmediate(renderer);
                    }
                    renderer = null;
                }
            }
        }
        [SerializeField]
        Spider spider;
        [SerializeField]
        Material legMaterial;
        [SerializeField]
        float legWidth = 1;
        [SerializeField]
        int legRounding = 0;
        [SerializeField]
        LineAlignment legAlignment = LineAlignment.TransformZ;

        void Awake() {
            OnValidate();
        }
        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!spider) {
                transform.TryGetComponentInParent(out spider);
            }
        }

        [SerializeField, ReadOnly]
        Leg[] legs = Array.Empty<Leg>();


        void OnEnable() {
            if (spider) {
                legs = spider
                    .GetComponentsInChildren<IKChain>()
                    .Select(leg => new Leg(leg, legMaterial))
                    .ToArray();

                UpdateLegs();
            }
        }

        void OnDisable() {
            foreach (var leg in legs) {
                leg.Dispose();
            }
            legs = Array.Empty<Leg>();
        }

        void LateUpdate() {
            UpdateLegs();
        }

        void UpdateLegs() {
            foreach (var leg in legs) {
                leg.renderer.alignment = legAlignment;
                leg.renderer.useWorldSpace = true;
                leg.renderer.widthMultiplier = legWidth;
                leg.renderer.numCornerVertices = legRounding;
                var positions = leg.chain
                    .joints
                    .Select(joint => joint.transform.position)
                    //.Prepend(spider.transform.position)
                    .ToArray();
                leg.renderer.positionCount = positions.Length;
                leg.renderer.SetPositions(positions);
            }
        }
    }
}
