using System;
using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour
{
    [Inject]
    private VariableDisplay variableDisplay;

    [SerializeField]
    private float timeScale = 1;
    
    [SerializeField]
    private float globalShootCooldown;

    private float lastShoot;

    public bool ShootIsOnGlobalCooldown => this.lastShoot + this.globalShootCooldown > Time.realtimeSinceStartup;

    public void ResetShootCooldown()
    {
        this.lastShoot = Time.realtimeSinceStartup;
    }

    private void Awake()
    {
        Time.timeScale = this.timeScale;
        
        if (!Mathf.Approximately(Time.timeScale, 1))
        {
            Debug.LogWarning("Time scale is " + Time.timeScale);
        }
    }
}
