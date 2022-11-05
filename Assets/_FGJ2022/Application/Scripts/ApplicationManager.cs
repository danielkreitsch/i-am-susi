using System;
using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour
{
    [Inject]
    private VariableDisplay variableDisplay;

    [SerializeField]
    private float globalShootCooldown;

    private float globalShootCooldownTimer;

    public bool ShootIsOnGlobalCooldown => this.globalShootCooldownTimer < this.globalShootCooldown;
    
    private void Awake()
    {
    }
    
    public void ResetShootCooldown()
    {
        this.globalShootCooldownTimer = 0;
    }

    private void Update()
    {
        if (this.globalShootCooldownTimer < this.globalShootCooldown)
        {
            this.globalShootCooldownTimer += Time.deltaTime;
            this.variableDisplay.Set("Global", "Shoot CD", Mathf.Max(0, this.globalShootCooldown - this.globalShootCooldownTimer));
        }
    }
}
