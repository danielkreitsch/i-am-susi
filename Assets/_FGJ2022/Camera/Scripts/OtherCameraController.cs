using UnityEngine;
using Zenject;

public class OtherCameraController : MonoBehaviour
{
    [Inject]
    private CameraManager cameraManager;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            this.cameraManager.TransitionToState(CameraState.Main);
        }
    }
}
