using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

public class ApplicationManager : MonoBehaviour
{
    [Inject]
    private VariableDisplay variableDisplay;
    
    private void Awake()
    {
        Debug.Log("ApplicationManager just woke up");
        this.variableDisplay.Set("Application", "Status", "Healthy");
    }
}
