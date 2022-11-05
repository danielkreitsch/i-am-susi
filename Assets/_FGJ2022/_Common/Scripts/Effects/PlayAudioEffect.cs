using FMODUnity;
using UnityEngine;

namespace Game.Effects {
    [CreateAssetMenu]
    sealed class PlayAudioEffect : EffectBase {
        [SerializeField]
        EventReference soundEvent = new();

        public override void Invoke(GameObject context) {
            if (soundEvent.IsNull) {
                return;
            }

            var eventDescription = RuntimeManager.GetEventDescription(soundEvent);

            if (!eventDescription.isValid()) {
                return;
            }

            eventDescription.createInstance(out var instance);
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(context));
            RuntimeManager.AttachInstanceToGameObject(instance, context.transform);
            instance.start();
        }
    }
}
