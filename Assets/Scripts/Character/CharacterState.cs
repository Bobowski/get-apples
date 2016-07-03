using UnityEngine;


public abstract class CharacterState : MonoBehaviour
{
    protected Character character;
    protected CharacterPhysics physics;
    protected CameraController cam;

    protected abstract Vector3 inputDirection { get; }

    void Awake ()
    {
        character = GetComponent<Character> (); 
        physics = GetComponent<CharacterPhysics> ();
        cam = Camera.main.GetComponent<CameraController> ();
    }

    // Interface for state behavior and transitions
    public abstract void OnBegin ();

    public abstract void Behave ();

    public abstract void Animate ();

    public abstract void OnEnd ();
}
