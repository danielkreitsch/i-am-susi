using UnityEngine;
using UnityEngine.AI;

namespace Game.Cleaner
{
    public class CleanerController : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navAgent;

        [SerializeField]
        private Cleaner cleaner;
        
        public bool IsStopped
        {
            get => this.navAgent.isStopped;
            set => this.navAgent.isStopped = value;
        }
        
        public void SetMoveTarget(Vector3 target)
        {
            this.navAgent.SetDestination(target);
        }
    }
}