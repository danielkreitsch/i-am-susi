using System.Linq;
using Game.Avatar.SpiderImpl;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    [ExecuteAlways]
    sealed class SpiderBodyRenderer : MonoBehaviour {
        [SerializeField]
        Spider spider;
        [SerializeField]
        string bodyName = "UpperBody";
        [SerializeField]
        GameObject bodyObj;
        [SerializeField]
        Vector3 bodyOffset;

        [Space]
        [SerializeField, ReadOnly]
        GameObject bodyReference;

        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!spider) {
                transform.TryGetComponentInParent(out spider);
            }

            if (spider) {
                bodyReference = spider.transform.GetComponentsInChildren<Transform>()
                    .Select(t => t.gameObject)
                    .FirstOrDefault(t => t.name == bodyName);

                UpdateBody();
            }
        }

        void LateUpdate() {
            UpdateBody();
        }

        void UpdateBody() {
            if (bodyObj && bodyReference) {
                bodyObj.transform.SetPositionAndRotation(
                    bodyReference.transform.position + (spider.transform.rotation * bodyOffset),
                    bodyReference.transform.rotation
                );
            }
        }
    }
}
