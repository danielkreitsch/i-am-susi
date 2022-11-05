using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class CleanerModel : MonoBehaviour
    {
        [SerializeField]
        private Transform weaponTransform;
        
        private float _upAngle;
        private float _weaponPosition;

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
    }
}