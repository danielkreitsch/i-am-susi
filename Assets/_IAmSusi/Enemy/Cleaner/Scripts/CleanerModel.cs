using UnityEngine;

namespace Game.Cleaner
{
    public class CleanerModel : MonoBehaviour
    {
        [SerializeField]
        private SpotAvatarTrigger spotAvatarTrigger;
        
        private float _upAngle;
        private float _weaponPosition;

        public SpotAvatarTrigger SpotAvatarTrigger => this.spotAvatarTrigger;
        
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
    }
}