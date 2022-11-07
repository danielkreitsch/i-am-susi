using Game;
using Game.Common;
using UnityEngine;

namespace Game.Drone {
    public sealed class SpotAvatarTrigger : MonoBehaviour {
        [SerializeField]
        DroneAgent droneAgent;

        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<ILaserTarget>(out var target) && target.IsValid() && target.isAvatar) {
                droneAgent.LaserTarget = target;
                droneAgent.StateMachine.ChangeState(DroneStateId.FocusTarget);
            }
        }
    }
}