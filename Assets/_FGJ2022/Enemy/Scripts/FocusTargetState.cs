using UnityEngine;

namespace FGJ2022
{
    public class FocusTargetState : EnemyState
    {
        public EnemyStateId GetId()
        {
            return EnemyStateId.FocusTarget;
        }

        public void Enter(EnemyAgent agent)
        {
        }

        public void Update(EnemyAgent agent)
        {
            var avatarPos = agent.Avatar.transform.position;
            var myPos = agent.Enemy.transform.position;
            var horizontalDistance = Vector2.Distance(new Vector2(myPos.x, myPos.z), new Vector2(avatarPos.x, avatarPos.z));
            var moveTarget = avatarPos;//myPos + (myPos - avatarPos);//.normalized * (1 - horizontalDistance);
            agent.Enemy.Controller.SetTarget(moveTarget);

            //agent.Enemy.Model.UpAngle = Mathf.Atan2(avatarPos.x - myPos.x, avatarPos.z - myPos.z) * Mathf.Rad2Deg;
            agent.Enemy.Model.transform.LookAt(avatarPos);
            
            if (horizontalDistance < agent.OptimalDistanceToShoot)
            {
                agent.StateMachine.ChangeState(EnemyStateId.Shoot);
            }
        }

        public void Exit(EnemyAgent agent)
        {
        }
    }
}