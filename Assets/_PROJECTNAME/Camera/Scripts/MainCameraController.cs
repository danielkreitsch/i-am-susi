using UnityEngine;
using Zenject;

public class MainCameraController : MonoBehaviour
{
    [Inject]
    private CameraManager cameraManager;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.cameraManager.TransitionToState(CameraState.Other);
        }
    }
}
