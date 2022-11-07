using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Game.Common {
    sealed class PlayContinuousAudio : MonoBehaviour {
        [SerializeField]
        EventReference soundEvent = new();

        EventInstance instance;

        void OnEnable() {
            if (soundEvent.IsNull) {
                return;
            }

            var eventDescription = RuntimeManager.GetEventDescription(soundEvent);

            if (!eventDescription.isValid()) {
                return;
            }

            eventDescription.createInstance(out var instance);

            if (!instance.isValid()) {
                return;
            }

            instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject));
            RuntimeManager.AttachInstanceToGameObject(instance, gameObject.transform);
            instance.start();
        }

        void OnDisable() {
            if (instance.isValid()) {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }
        }
    }
}
