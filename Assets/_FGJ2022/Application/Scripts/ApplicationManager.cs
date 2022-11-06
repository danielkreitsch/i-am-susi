using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour
{
    [Inject]
    private VariableDisplay variableDisplay;

    [SerializeField]
    private float globalShootCooldown;

    private float lastShoot;

    public bool ShootIsOnGlobalCooldown => this.lastShoot + this.globalShootCooldown > Time.realtimeSinceStartup;

    public void ResetShootCooldown()
    {
        this.lastShoot = Time.realtimeSinceStartup;
    }
}
