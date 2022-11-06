using Game.Common;
using UnityConstants;
using UnityEngine;

namespace FGJ2022.Drone {
    public class FocusTargetState : DroneState {
        DroneModel model;

        public DroneStateId GetId() {
            return DroneStateId.FocusTarget;
        }

        public void Enter(DroneAgent agent) {
            model = agent.Drone.Model;
        }

        public void Update(DroneAgent agent) {
            if (!agent.LaserTarget.IsValid()) {
                agent.StateMachine.ChangeState(DroneStateId.Idle);
                return;
            }

            var avatarPos = agent.LaserTarget.transform.position;
            var myPos = model.WeaponTransform.position;
            float horizontalDistance = Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(avatarPos.x, avatarPos.z));
            var moveTarget = myPos + (myPos - avatarPos).normalized * (10 - horizontalDistance);
            agent.Drone.Controller.SetMoveTarget(moveTarget);

            bool wasHit = Physics.Raycast(myPos, avatarPos - myPos, out var solidHit, Vector3.Distance(avatarPos, myPos), agent.Drone.SolidLayer);
            bool opaqueObstacleInTheWay = wasHit && !solidHit.collider.gameObject.CompareTag(Tags.Transparent);

            bool aimsAtAvatar = Physics.Raycast(myPos, avatarPos - myPos, agent.OptimalDistanceToShoot * 2, agent.Drone.AvatarLayer);
            bool isOnCooldown = agent.Drone.ShootIsOnCooldown || agent.ApplicationManager.ShootIsOnGlobalCooldown;
            bool isCloseEnough = horizontalDistance < agent.OptimalDistanceToShoot;

            if (!isOnCooldown && isCloseEnough && aimsAtAvatar && !opaqueObstacleInTheWay) {
                agent.Drone.ResetShootCooldown();
                agent.ApplicationManager.ResetShootCooldown();
                agent.StateMachine.ChangeState(DroneStateId.Shoot);
            }
        }

        public void Exit(DroneAgent agent) {
        }
    }
}