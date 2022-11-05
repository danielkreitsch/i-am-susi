using UnityEngine;
using UnityEngine.AI;

namespace FGJ2022.Drone
{
    public class DroneController : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navAgent;

        [SerializeField]
        private Drone drone;
        
        public bool IsStopped
        {
            get => this.navAgent.isStopped;
            set => this.navAgent.isStopped = value;
        }

        public void SetTarget(Vector3 target)
        {
            //target.y = 0;
            this.navAgent.SetDestination(target);
            Debug.Log("Destination: " + target);

            this.drone.Model.LocalY = 0.25f;
        }
    }
}