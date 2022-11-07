using Game;
using UnityEngine;

namespace Game.Cleaner
{
    public class SpotAvatarTrigger : MonoBehaviour
    {
        [SerializeField]
        private CleanerAgent cleanerAgent;
        
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<IVacuumTarget>(out var vacuumTarget))
            {
                this.cleanerAgent.VacuumTarget = vacuumTarget;
                this.cleanerAgent.StateMachine.ChangeState(CleanerStateId.FocusTarget);
            }
        }
    }
}