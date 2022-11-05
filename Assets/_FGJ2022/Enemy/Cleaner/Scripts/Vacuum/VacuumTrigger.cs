using System;
using AssemblyCSharp;
using UnityConstants;
using UnityEngine;

namespace FGJ2022.Cleaner
{
    public class VacuumTrigger : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var vacuumTarget = other.GetComponent<IVacuumTarget>();
            
            if (vacuumTarget == null)
            {
                return;
            }

            var pullDirection = (this.transform.position - other.transform.position).normalized;
            var strength = 1;
            vacuumTarget.Apply(this.gameObject, pullDirection, strength);
            Debug.Log("VACUUMING!");
        }
    }
}