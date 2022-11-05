using UnityEngine;
using UnityEngine.AI;

namespace FGJ2022
{
    public class EnemyController : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navAgent;

        [SerializeField]
        private Enemy enemy;
        
        public bool IsStopped
        {
            get => this.navAgent.isStopped;
            set => this.navAgent.isStopped = value;
        }

        public void SetTarget(Vector3 target)
        {
            //target.y = 0;
            this.navAgent.SetDestination(target);
            Debug.Log("Destination: " + target);

            this.enemy.Model.LocalY = 0.25f;
        }
    }
}