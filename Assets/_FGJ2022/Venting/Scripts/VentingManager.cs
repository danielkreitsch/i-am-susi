using System;
using UnityEngine;

namespace Game.Venting {
    public class VentingManager : MonoBehaviour {
        IVent _currentVent;

        //public bool IsVenting { get; set; }

        public Action OnCurrentVentChanged;

        public IVent CurrentVent {
            get => _currentVent;
            set {
                if (_currentVent == value) {
                    return;
                }

                _currentVent = value;
                OnCurrentVentChanged?.Invoke();
            }
        }
    }
}