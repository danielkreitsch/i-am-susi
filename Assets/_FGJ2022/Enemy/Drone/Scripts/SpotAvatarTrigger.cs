using Game;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class SpotAvatarTrigger : MonoBehaviour
    {
        [SerializeField]
        private DroneAgent droneAgent;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ILaserTarget>(out var laserTarget))
            {
                this.droneAgent.LaserTarget = laserTarget;
                this.droneAgent.StateMachine.ChangeState(DroneStateId.FocusTarget);
            }
        }
    }
}