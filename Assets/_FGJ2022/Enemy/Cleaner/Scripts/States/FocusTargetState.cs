using Game.Common;

namespace FGJ2022.Cleaner {
    public class FocusTargetState : CleanerState {
        public CleanerStateId GetId() {
            return CleanerStateId.FocusTarget;
        }

        public void Enter(CleanerAgent agent) {
        }

        public void Update(CleanerAgent agent) {
            if (!agent.VacuumTarget.IsValid()) {
                agent.StateMachine.ChangeState(CleanerStateId.CleanRoom);
                return;
            }

            var avatarPos = agent.VacuumTarget.transform.position;
            var myPos = agent.Cleaner.transform.position;
            var moveTarget = avatarPos;//myPos + (myPos - avatarPos);//.normalized * (1 - horizontalDistance);
            agent.Cleaner.Controller.SetMoveTarget(moveTarget);

            //agent.Cleaner.Model.UpAngle = Mathf.Atan2(avatarPos.x - myPos.x, avatarPos.z - myPos.z) * Mathf.Rad2Deg;
        }

        public void Exit(CleanerAgent agent) {
        }
    }
}