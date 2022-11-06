using Game.Utility;
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
        [NotNull]
        private NavMeshAgent navAgent;

        [SerializeField]
        [NotNull]
        private Drone drone;

        [SerializeField]
        private float offsetY;

        [SerializeField]
        private float minY;

        [SerializeField]
        private float verticalSmoothTime;

        [SerializeField]
        private float rotationSmoothTime;

        [SerializeField]
        private float rotationSmoothTimePointing;

        private float targetY;
        private float verticalVelocity;

        /*private Vector3 lookPosition;
        private Vector3 targetLookPosition;*/
        private Vector3 modelRotationVelocity;

        public bool IsStopped
        {
            get => this.navAgent.isStopped;
            set => this.navAgent.isStopped = value;
        }

        public void SetMoveTarget(Vector3 target)
        {
            this.navAgent.SetDestination(target);
            this.targetY = Mathf.Max(target.y + this.offsetY, this.minY);
        }

        private void Update()
        {
            var currentState = this.drone.Agent.StateMachine.CurrentState;

            if (currentState == DroneStateId.Shoot && this.drone.Model.Laser.IsDeadly)
            {
            }
            else
            {
                this.drone.Model.LocalY = Mathf.SmoothDamp(this.drone.Model.LocalY, this.targetY, ref this.verticalVelocity, this.verticalSmoothTime);

                var myPos = this.drone.Model.transform.position;
                var avatarPos = this.drone.Agent.Avatar.transform.position + 0.75f * Vector3.up;
                this.drone.Model.transform.rotation = QuaternionUtility.SmoothDampQuaternion(
                    this.drone.Model.transform.rotation,
                    Quaternion.LookRotation(avatarPos - myPos),
                    ref this.modelRotationVelocity,
                    currentState == DroneStateId.Shoot ? this.rotationSmoothTimePointing : this.rotationSmoothTime
                );
            }
           
        }
    }
}