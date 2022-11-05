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

        [SerializeField]
        private float verticalSmoothTime;

        private float targetY;
        private float verticalVelocity;

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

            this.targetY = target.y;
            this.drone.Model.LocalY = Mathf.SmoothDamp(this.drone.Model.LocalY, this.targetY, ref this.verticalVelocity, this.verticalSmoothTime);
        }
    }
}