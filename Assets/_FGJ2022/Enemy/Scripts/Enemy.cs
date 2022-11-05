using UnityEngine;

namespace FGJ2022
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyController controller;

        [SerializeField]
        private EnemyModel model;

        public EnemyController Controller => this.controller;

        public EnemyModel Model => this.model;
    }
}