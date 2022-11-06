using Game.Common;
using Game.Utility;
using Glowdragon.VariableDisplay;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace FGJ2022.Drone {
    public class DroneController : MonoBehaviour {
        [Inject]
        VariableDisplay variableDisplay;

        [SerializeField]
        [NotNull]
        NavMeshAgent navAgent;

        [SerializeField]
        [NotNull]
        Drone drone;

        [SerializeField]
        float offsetY;

        [SerializeField]
        float minY;

        [SerializeField]
        float verticalSmoothTime;

        [SerializeField]
        float rotationSmoothTime;

        [SerializeField]
        float rotationSmoothTimePointing;

        float targetY;
        float verticalVelocity;

        /*private Vector3 lookPosition;
        private Vector3 targetLookPosition;*/
        Vector3 modelRotationVelocity;

        Vector3 previousPosition;

        public bool IsStopped {
            get => navAgent.isStopped;
            set => navAgent.isStopped = value;
        }

        public void SetMoveTarget(Vector3 target) {
            navAgent.SetDestination(target);
            targetY = Mathf.Max(target.y + offsetY, minY);
        }

        void Update() {
            var myPosition = drone.Model.transform.position;
            var deltaPosition = myPosition - previousPosition;

            var currentState = drone.Agent.StateMachine.CurrentState;

            if (currentState == DroneStateId.Shoot && drone.Model.Laser.IsDeadly) {
            } else {
                drone.Model.LocalY = Mathf.SmoothDamp(drone.Model.LocalY, targetY, ref verticalVelocity, verticalSmoothTime);

                var lookDirection = Vector3.zero;

                if (drone.Agent.LaserTarget.IsValid()) {
                    var chaseTargetPos = drone.Agent.LaserTarget.transform.position + 0.75f * Vector3.up;
                    lookDirection = chaseTargetPos - myPosition;
                } else if (!Mathf.Approximately(deltaPosition.magnitude, 0)) {
                    lookDirection = deltaPosition;
                }

                drone.Model.transform.rotation = QuaternionUtility.SmoothDampQuaternion(
                    drone.Model.transform.rotation,
                    Quaternion.LookRotation(lookDirection),
                    ref modelRotationVelocity,
                    currentState == DroneStateId.Shoot ? rotationSmoothTimePointing : rotationSmoothTime
                );
            }

            previousPosition = myPosition;
        }
    }
}