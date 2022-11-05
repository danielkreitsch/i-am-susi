using Glowdragon.VariableDisplay;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace FGJ2022.Drone
{
    public class DroneController : MonoBehaviour
    {
        [Inject]
        private VariableDisplay variableDisplay;
        
        [SerializeField]
        private NavMeshAgent navAgent;

        [SerializeField]
        private Drone drone;

        [SerializeField]
        private float offsetY;

        [SerializeField]
        private float minY;
        
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
            this.navAgent.SetDestination(target);
            this.targetY = Mathf.Max(target.y + this.offsetY, this.minY);
        }

        private void Update()
        {
            this.drone.Model.LocalY = Mathf.SmoothDamp(this.drone.Model.LocalY, this.targetY, ref this.verticalVelocity, this.verticalSmoothTime);
        }
    }
}