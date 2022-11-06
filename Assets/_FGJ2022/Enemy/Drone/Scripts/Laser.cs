using System;
using Game;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private float thickness;
    private float length;

    private bool _isDeadly;
    
    public bool IsDeadly
    {
        get => this._isDeadly;
        set
        {
            if (this.TryGetComponent<BoxCollider>(out var boxCollider))
            {
                this._isDeadly = value;
                this.GetComponent<BoxCollider>().enabled = this._isDeadly;
            }
            else
            {
                Debug.LogWarning("Variable \"IsDeadly\" cannot be changed due to missing component");
            }
        }
    }

    public float Thickness
    {
        get => this.thickness;
        set
        {
            if (this.transform == null)
            {
                Debug.LogWarning("Laser thickness cannot be changed due to missing transform");
                return;
            }
            
            this.thickness = value;
            var localScale = this.transform.localScale;
            localScale.y = this.thickness;
            localScale.z = this.thickness;
            this.transform.localScale = localScale;
        }
    }
        
    public float Length
    {
        get => this.length;
        set
        {
            if (this.transform == null)
            {
                Debug.LogWarning("Laser length cannot be changed due to missing transform");
                return;
            }
            
            this.length = value;
                
            var localScale = this.transform.localScale;
            localScale.x = this.length / 12;
            this.transform.localScale = localScale;
                
            var localPosition = this.transform.localPosition;
            localPosition.z = this.length / 12 / 2;
            this.transform.localPosition = localPosition;
        }
    }

    private void Start()
    {
        this.IsDeadly = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<ILaserTarget>(out var target))
        {
            target.ReceiveLaser(this.gameObject);
        }
    }
}
