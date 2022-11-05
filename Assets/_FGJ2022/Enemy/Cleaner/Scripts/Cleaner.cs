using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class Cleaner : MonoBehaviour
    {
        [SerializeField]
        private CleanerController controller;

        [SerializeField]
        private CleanerModel model;

        public CleanerController Controller => this.controller;

        public CleanerModel Model => this.model;
    }
}