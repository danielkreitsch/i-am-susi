using UnityEngine;

namespace FGJ2022.Drone
{
    public class FocusTargetState : DroneState
    {
        public DroneStateId GetId()
        {
            return DroneStateId.FocusTarget;
        }

        public void Enter(DroneAgent agent)
        {
        }

        public void Update(DroneAgent agent)
        {
            var avatarPos = agent.Avatar.transform.position;
            var myPos = agent.Drone.transform.position;
            var horizontalDistance = Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(avatarPos.x, avatarPos.z));
            var moveTarget = avatarPos;//myPos + (myPos - avatarPos);//.normalized * (1 - horizontalDistance);
            agent.Drone.Controller.SetTarget(moveTarget);

            //agent.Enemy.Model.UpAngle = Mathf.Atan2(avatarPos.x - myPos.x, avatarPos.z - myPos.z) * Mathf.Rad2Deg;
            agent.Drone.Model.transform.LookAt(avatarPos);
            
            if (horizontalDistance < agent.OptimalDistanceToShoot)
            {
                agent.StateMachine.ChangeState(DroneStateId.Shoot);
            }
        }

        public void Exit(DroneAgent agent)
        {
        }
    }
}