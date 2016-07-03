using UnityEngine;


public abstract class CameraState : MonoBehaviour
{
    protected CameraController controller;

    void Awake ()
    {
        controller = GetComponent <CameraController> ();
    }

    // Interface for state behavior and transitions
    public abstract void OnBegin ();

    public abstract void Behave ();

    public abstract void OnEnd ();
}
