using Game;
using Game.Common;
using UnityEngine;

public sealed class Laser : MonoBehaviour {
    float thickness;
    float length;

    bool _isDeadly;

    public bool IsDeadly {
        get => _isDeadly;
        set {
            if (TryGetComponent<BoxCollider>(out var boxCollider)) {
                _isDeadly = value;
                GetComponent<BoxCollider>().enabled = _isDeadly;
            } else {
                Debug.LogWarning("Variable \"IsDeadly\" cannot be changed due to missing component");
            }
        }
    }

    public float Thickness {
        get => thickness;
        set {
            if (transform == null) {
                Debug.LogWarning("Laser thickness cannot be changed due to missing transform");
                return;
            }

            thickness = value;
            var localScale = transform.localScale;
            localScale.y = thickness;
            localScale.z = thickness;
            transform.localScale = localScale;
        }
    }

    public float Length {
        get => length;
        set {
            if (transform == null) {
                Debug.LogWarning("Laser length cannot be changed due to missing transform");
                return;
            }

            length = value;

            var localScale = transform.localScale;
            localScale.x = length / 12;
            transform.localScale = localScale;

            var localPosition = transform.localPosition;
            localPosition.z = length / 12 / 2;
            transform.localPosition = localPosition;
        }
    }

    void Start() {
        IsDeadly = false;
    }

    void OnTriggerEnter(Collider other) {
        if (IsDeadly && other.TryGetComponent<ILaserTarget>(out var target) && target.IsValid()) {
            target.GetHitBy(gameObject);
        }
    }
}
