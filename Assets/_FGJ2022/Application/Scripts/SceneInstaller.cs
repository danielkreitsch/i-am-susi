using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField]
    private CameraManager cameraManager;
    
    public override void InstallBindings()
    {
        this.Container.BindInstances(
            this.cameraManager
        );
    }
}