using System.Collections;
using Game.Venting;
using Slothsoft.UnityExtensions;
using UnityEngine;

namespace Game.Avatar {
    sealed class AvatarVenting : MonoBehaviour, IVentable {
        [SerializeField]
        AvatarMotor motor = default;

        [Space]
        [SerializeField]
        float inForce = 100;
        [SerializeField]
        float ventInTime = 1;

        [Space]
        [SerializeField]
        float outSpeed = 100;
        [SerializeField]
        float ventOutTime = 1;


        void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        void OnValidate() {
            if (!motor) {
                transform.TryGetComponentInChildren(out motor);
            }
        }

        public void StartVenting(IVent inVent, IVent outVent) {
            StartCoroutine(Vent_Co(inVent, outVent));
        }

        IEnumerator Vent_Co(IVent inVent, IVent outVent) {
            motor.attachedRigidbody.velocity = motor.velocity;
            motor.attachedRigidbody.isKinematic = false;
            motor.attachedRigidbody.detectCollisions = false;

            for (float timer = 0; timer < ventInTime; timer += Time.deltaTime) {
                var direction = (inVent.Transform.position - motor.transform.position).normalized;
                motor.attachedRigidbody.AddForce(direction * inForce, ForceMode.Acceleration);
                motor.attachedRigidbody.AddForce(inVent.VentDirection * inForce, ForceMode.Acceleration);
                yield return null;
            }

            motor.attachedRigidbody.velocity = Vector3.zero;
            motor.attachedRigidbody.position = outVent.Transform.position;

            motor.attachedRigidbody.AddForce(outVent.VentDirection * outSpeed, ForceMode.VelocityChange);
            yield return Wait.forSeconds[ventOutTime];

            motor.velocity = motor.attachedRigidbody.velocity;
            motor.attachedRigidbody.isKinematic = true;
            motor.attachedRigidbody.detectCollisions = true;
        }
    }
}
