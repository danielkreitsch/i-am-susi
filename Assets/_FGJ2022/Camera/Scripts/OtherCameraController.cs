using UnityEngine;
using Zenject;

public class OtherCameraController : MonoBehaviour
{
    [Inject]
    private CameraManager cameraManager;
    
    private void Update()
    {
       
    }
}
