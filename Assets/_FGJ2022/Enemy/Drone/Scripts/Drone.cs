using Glowdragon.VariableDisplay;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

namespace FGJ2022.Drone
{
    public class Drone : MonoBehaviour
    {
        [Inject]
        private VariableDisplay variableDisplay;

        [SerializeField]
        private DroneAgent agent;
        
        [SerializeField]
        private DroneController controller;

        [SerializeField]
        private DroneModel model;

        [SerializeField]
        private LayerMask solidLayer;
        
        [SerializeField]
        private float shootCooldown;

        private float lastShoot;

        public DroneAgent Agent => this.agent;

        public DroneController Controller => this.controller;

        public DroneModel Model => this.model;

        public LayerMask SolidLayer => this.solidLayer;

        public bool ShootIsOnCooldown => this.lastShoot + this.shootCooldown > Time.realtimeSinceStartup;

        public void ResetShootCooldown()
        {
            this.lastShoot = Time.realtimeSinceStartup;
        }

        private void Start() {
            onStart.Invoke(gameObject);
        }

        [SerializeField]
        public UnityEvent<GameObject> onStart = new();

        [SerializeField]
        public UnityEvent<GameObject> onShoot = new();
    }
}