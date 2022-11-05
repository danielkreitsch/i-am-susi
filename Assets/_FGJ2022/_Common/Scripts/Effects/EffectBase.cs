using System.Collections;
using MyBox;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Effects {
    abstract class EffectBase : ScriptableObject {
        sealed class CoroutineInvoker : MonoBehaviour {
        }
        [SerializeField, ReadOnly]
        EffectBase asset = default;

        [Space]
        [SerializeField]
        bool waitUntilInvoke = false;
        [SerializeField, ConditionalField(nameof(waitUntilInvoke))]
        float waitTimer = 1;

        protected virtual void OnValidate() {
            asset = this;
        }

        public void Invoke(GameObject context) {
            if (waitUntilInvoke) {
                var invoker = context.AddComponent<CoroutineInvoker>();
                invoker.StartCoroutine(Invoke_Co(context));
            } else {
                InvokeNow(context);
            }
        }

        IEnumerator Invoke_Co(GameObject context) {
            yield return Wait.forSeconds[waitTimer];
            InvokeNow(context);
        }

        protected abstract void InvokeNow(GameObject context);
    }
}
