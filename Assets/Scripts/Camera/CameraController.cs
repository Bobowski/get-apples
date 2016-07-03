using UnityEngine;


public class CameraController : MonoBehaviour
{
    // Camera should always look at something that acts as pivot
    // Distance should be the desired distance between camera & pivot
    public Transform pivot;
    public float distance;
    
    // Private
    [SerializeField] private CameraState state;

    public CameraState State {
        get { return state; }
        set {
            state.OnEnd ();
            state = value;
            state.OnBegin ();
        }
    }

    void Start ()
    {
        state.OnBegin ();
    }

    void Update ()
    {
        state.Behave ();
    }
}
