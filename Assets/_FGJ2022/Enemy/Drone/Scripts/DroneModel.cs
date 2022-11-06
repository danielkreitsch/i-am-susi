using Slothsoft.UnityExtensions;
using UnityEngine;

namespace FGJ2022.Drone
{
    public class DroneModel : MonoBehaviour
    {
        [SerializeField]
        private Laser laser;
        
        [SerializeField]
        private Transform weaponTransform;

        [SerializeField]
        private Transform weaponRingTransform;

        [SerializeField]
        private Transform laserRaycastOriginTransform;

        [SerializeField]
        private SpotAvatarTrigger spotAvatarTrigger;
        
        private float _localY;
        private float _upAngle;
        private float _weaponPosition;
        private float _weaponRingAngle;

        public Laser Laser => this.laser;

        public Transform WeaponTransform => this.weaponTransform;

        public Transform LaserRaycastOriginTransform => this.laserRaycastOriginTransform;

        public SpotAvatarTrigger SpotAvatarTrigger => this.spotAvatarTrigger;
        
        public float LocalY
        {
            get => this._localY;
            set
            {
                this._localY = value;
                var localPosition = this.transform.localPosition;
                localPosition.y = this._localY;
                this.transform.localPosition = localPosition;
            }
        }

        public float UpAngle
        {
            get => this._upAngle;
            set
            {
                this._upAngle = value;
                var eulerAngles = this.transform.eulerAngles;
                eulerAngles.y = this._upAngle;
                this.transform.eulerAngles = eulerAngles;
            }
        }

        public float WeaponPosition
        {
            get => this._weaponPosition;
            set
            {
                if (!this.weaponTransform)
                {
                    Debug.LogWarning("Weapon position cannot be changed due to missing transform");
                    return;
                }
                
                this._weaponPosition = value;
                var localPosition = this.weaponTransform.localPosition;
                localPosition.z = this._weaponPosition;
                this.weaponTransform.localPosition = localPosition;
            }
        }

        public float WeaponRingAngle
        {
            get => this._weaponRingAngle;
            set
            {
                if (!this.weaponTransform)
                {
                    Debug.LogWarning("Weapon ring angle cannot be changed due to missing transform");
                    return;
                }
                
                this._weaponRingAngle = value;
                var eulerAngles = this.weaponRingTransform.eulerAngles;
                eulerAngles.z = this._weaponRingAngle;
                this.weaponRingTransform.eulerAngles = eulerAngles;
            }
        }
        
        private void OnDrawGizmosSelected()
        {
            var ray = new Ray(this.LaserRaycastOriginTransform.position, this.LaserRaycastOriginTransform.forward);
            Gizmos.DrawLine(ray.origin, ray.origin + ray.direction * 1000);
        }

        protected void Awake() {
            OnValidate();
        }

        [ContextMenu(nameof(OnValidate))]
        protected void OnValidate() {
            if (!spotAvatarTrigger) {
                transform.TryGetComponentInChildren(out spotAvatarTrigger);
            }
            if (!laser) {
                transform.TryGetComponentInChildren(out laser);
            }
        }
    }
}