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

        public void SetTarget(Vector3 target)
        {
            //target.y = 0;
            this.navAgent.SetDestination(target);

            this.enemy.LocalY = 0.3f;
        }
    }
}