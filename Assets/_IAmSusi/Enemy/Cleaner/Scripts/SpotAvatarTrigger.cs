using Game;
using UnityEngine;

namespace FGJ2022.Cleaner
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