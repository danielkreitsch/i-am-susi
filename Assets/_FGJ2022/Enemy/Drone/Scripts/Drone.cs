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
        private LayerMask avatarLayer;

        [SerializeField]
        private LayerMask solidLayer;

        [SerializeField]
        private float shootCooldown;

        [SerializeField]
        [NotNull]
        private Path idlePath;

        private float lastShoot;

        public DroneAgent Agent => this.agent;

        public DroneController Controller => this.controller;

        public DroneModel Model => this.model;

        public LayerMask AvatarLayer => this.avatarLayer;

        public LayerMask SolidLayer => this.solidLayer;

        public bool ShootIsOnCooldown => this.lastShoot + this.shootCooldown > Time.realtimeSinceStartup;
        
        public Path IdlePath => this.idlePath;
        
        public void ResetShootCooldown()
        {
            this.lastShoot = Time.realtimeSinceStartup;
        }

        private void Start()
        {
            this.onStart.Invoke(this.gameObject);
        }

        [SerializeField]
        public UnityEvent<GameObject> onStart = new();

        [SerializeField]
        public UnityEvent<GameObject> onShoot = new();
    }
}