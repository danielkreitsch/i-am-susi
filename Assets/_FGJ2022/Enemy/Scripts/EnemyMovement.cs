using UnityEngine;
using UnityEngine.AI;

namespace FGJ2022
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField]
        private NavMeshAgent navAgent;

        public void SetTarget(Vector3 target)
        {
            target.y = 0;
            this.navAgent.SetDestination(target);
        }
    }
}