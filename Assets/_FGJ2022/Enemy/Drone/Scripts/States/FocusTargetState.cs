using Game.Utility;
using UnityConstants;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class FocusTargetState : DroneState
    {
        private DroneModel model;

        private Vector3 modelRotationVelocity;

        public DroneStateId GetId()
        {
            return DroneStateId.FocusTarget;
        }

        public void Enter(DroneAgent agent)
        {
            this.model = agent.Drone.Model;
        }

        public void Update(DroneAgent agent)
        {
            var avatarPos = agent.Avatar.transform.position;
            var myPos = this.model.WeaponTransform.position;
            var horizontalDistance = Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(avatarPos.x, avatarPos.z));
            var moveTarget = avatarPos; //myPos + (myPos - avatarPos);//.normalized * (1 - horizontalDistance);
            agent.Drone.Controller.SetMoveTarget(moveTarget);
            
            RaycastHit solidHit;
            Physics.Raycast(myPos, avatarPos - myPos, out solidHit, Vector3.Distance(avatarPos, myPos), agent.Drone.SolidLayer);
            var obstacleInTheWay = solidHit.collider != null && !solidHit.collider.gameObject.CompareTag(Tags.Cover);

            var aimsAtAvatar = Physics.Raycast(myPos, avatarPos - myPos, agent.OptimalDistanceToShoot * 2, agent.Drone.AvatarLayer);
            var isOnCooldown = agent.Drone.ShootIsOnCooldown || agent.ApplicationManager.ShootIsOnGlobalCooldown;
            var isCloseEnough = horizontalDistance < agent.OptimalDistanceToShoot;
            
            if (!isOnCooldown && isCloseEnough && aimsAtAvatar && !obstacleInTheWay)
            {
                agent.Drone.ResetShootCooldown();
                agent.ApplicationManager.ResetShootCooldown();
                agent.StateMachine.ChangeState(DroneStateId.Shoot);
            }
        }

        public void Exit(DroneAgent agent)
        {
        }
    }
}