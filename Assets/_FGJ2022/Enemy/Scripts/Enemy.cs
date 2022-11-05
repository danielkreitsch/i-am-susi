using UnityEngine;

namespace FGJ2022
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField]
        private EnemyController controller;
        
        [SerializeField]
        private Transform modelTransform;

        private float localY;
        private float _upAngle;

        public EnemyController Controller => this.controller;

        public float LocalY
        {
            get => this.localY;
            set
            {
                this.localY = value;
                var localPosition = this.modelTransform.transform.localPosition;
                localPosition.y = this.localY;
                this.modelTransform.transform.localPosition = localPosition;
            }
        }
        
        public float UpAngle
        {
            get => this._upAngle;
            set
            {
                this._upAngle = value;
                var eulerAngles = this.modelTransform.eulerAngles;
                eulerAngles.y = this._upAngle;
                this.modelTransform.eulerAngles = eulerAngles;
            }
        }
    }
}