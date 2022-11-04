using Glowdragon.VariableDisplay;
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]
    private ApplicationManager applicationManager;

    [SerializeField]
    private VariableDisplay variableDisplay;
    
    public override void InstallBindings()
    {
        this.Container.BindInstances(
            this.applicationManager,
            this.variableDisplay
        );
    }
}