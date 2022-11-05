using UnityEngine;
using UnityEngine.AI;

namespace FGJ2022.Cleaner
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
        
        public void SetTarget(Vector3 target)
        {
            //target.y = 0;
            this.navAgent.SetDestination(target);
            //Debug.Log("Destination: " + target);
        }
    }
}