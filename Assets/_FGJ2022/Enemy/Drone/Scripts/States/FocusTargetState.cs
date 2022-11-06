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
            
            var aimsAtAvatar = Physics.Raycast(myPos, avatarPos, agent.OptimalDistanceToShoot * 2, Layers.Player);
            var isOnCooldown = agent.Drone.ShootIsOnCooldown;
            var isCloseEnough = horizontalDistance < agent.OptimalDistanceToShoot;
            if (!isOnCooldown && isCloseEnough && aimsAtAvatar)
            {
                agent.StateMachine.ChangeState(DroneStateId.Shoot);
            }
        }

        public void Exit(DroneAgent agent)
        {
        }
    }
}