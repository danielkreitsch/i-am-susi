using Game;
using Game.Common;
using UnityEngine;

namespace FGJ2022.Drone {
    public class SpotAvatarTrigger : MonoBehaviour {
        [SerializeField]
        DroneAgent droneAgent;

        void OnTriggerEnter(Collider other) {
            if (other.TryGetComponent<ILaserTarget>(out var target) && target.IsValid()) {
                droneAgent.LaserTarget = target;
                droneAgent.StateMachine.ChangeState(DroneStateId.FocusTarget);
            }
        }
    }
}