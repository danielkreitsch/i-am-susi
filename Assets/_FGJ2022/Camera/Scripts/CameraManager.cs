using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    [NotNull(IgnorePrefab = true)]
    private new Camera camera;
    
    [SerializeField]
    private Animator animator;

    public Camera Camera => this.camera;

    public void TransitionToState(CameraState state)
    {
        this.animator.Play(state.ToString());
    }
}
