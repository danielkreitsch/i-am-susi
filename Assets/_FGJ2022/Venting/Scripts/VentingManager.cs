using System;
using UnityEngine;

namespace FGJ2022.Venting
{
    public class VentingManager : MonoBehaviour
    {
        private IVent _currentVent;

        //public bool IsVenting { get; set; }

        public Action OnCurrentVentChanged;

        public IVent CurrentVent
        {
            get => this._currentVent;
            set
            {
                if (this._currentVent == value)
                {
                    return;
                }

                this._currentVent = value;
                this.OnCurrentVentChanged?.Invoke();
            }
        }
    }
}