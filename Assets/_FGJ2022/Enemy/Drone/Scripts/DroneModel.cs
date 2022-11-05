using UnityEngine;

namespace FGJ2022.Drone
{
    public class DroneModel : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponTransform;

        [SerializeField]
        private Transform weaponRingTransform;

        private float _localY;
        private float _upAngle;
        private float _weaponPosition;
        private float _weaponRingAngle;

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
                this._weaponRingAngle = value;
                var eulerAngles = this.weaponRingTransform.eulerAngles;
                eulerAngles.z = this._weaponRingAngle;
                this.weaponRingTransform.eulerAngles = eulerAngles;
            }
        }
    }
}