using System;
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

        private float shootCooldownTimer;

        public DroneAgent Agent => this.agent;

        public DroneController Controller => this.controller;

        public DroneModel Model => this.model;

        public LayerMask SolidLayer => this.solidLayer;

        public bool ShootIsOnCooldown => this.shootCooldownTimer < this.shootCooldown;

        public void ResetShootCooldown()
        {
            this.shootCooldownTimer = 0;
        }

        private void Start() {
            onStart.Invoke(gameObject);
        }

        private void Update()
        {
            if (this.shootCooldownTimer < this.shootCooldown)
            {
                this.shootCooldownTimer += Time.deltaTime;
                this.variableDisplay.Set(this.gameObject.name, "Shoot CD", Mathf.Max(0, this.shootCooldown - this.shootCooldownTimer));
            }
        }

        [SerializeField]
        public UnityEvent<GameObject> onStart = new();

        [SerializeField]
        public UnityEvent<GameObject> onShoot = new();
    }
}