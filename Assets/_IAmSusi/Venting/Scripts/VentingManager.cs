using System;
using UnityEngine;

namespace Game.Venting {
    public class VentingManager : MonoBehaviour {
        IVent _currentIntakeVent;
        IVent _currentOutletVent;
        
        public Action OnCurrentIntakeVentChanged;
        
        public Action OnCurrentOutletVentChanged;

        public IVent CurrentIntakeVent {
            get => this._currentIntakeVent;
            set {
                if (this._currentIntakeVent == value) {
                    return;
                }

                this._currentIntakeVent = value;
                this.OnCurrentIntakeVentChanged?.Invoke();
            }
        }
        
        public IVent CurrentOutletVent {
            get => this._currentOutletVent;
            set {
                if (this._currentOutletVent == value) {
                    return;
                }

                this._currentOutletVent = value;
                this.OnCurrentOutletVentChanged?.Invoke();
            }
        }
    }
}