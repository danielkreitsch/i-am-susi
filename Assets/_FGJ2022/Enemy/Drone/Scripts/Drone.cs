using UnityEngine;

namespace FGJ2022.Drone
{
    public class Drone : MonoBehaviour
    {
        [SerializeField]
        private DroneController controller;

        [SerializeField]
        private DroneModel model;

        public DroneController Controller => this.controller;

        public DroneModel Model => this.model;
    }
}