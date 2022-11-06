using Game;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public sealed class VacuumDeathTrigger : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<ILaserTarget>(out var laserTarget))
            {
                laserTarget.ReceiveLaser(this.gameObject);
            }
        }
    }
}