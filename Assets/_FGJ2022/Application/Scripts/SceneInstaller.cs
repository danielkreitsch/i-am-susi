using FGJ2022.Venting;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private CameraManager cameraManager;

    [SerializeField]
    private VentingManager ventingManager;
    
    public override void InstallBindings()
    {
        this.Container.BindInstances(
            this.cameraManager,
            this.ventingManager
        );
    }
}