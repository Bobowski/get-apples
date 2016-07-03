using UnityEngine;


public class CharacterPhysics : MonoBehaviour
{
    public bool apply;
    public Vector3 velocity;
    public GravityInfo gravity;

    private CharacterController controller;

    [System.Serializable]
    public struct GravityInfo
    {
        public bool apply;
        public bool isGrounded;
        public bool wasGrounded;
        public float acceleration;
        public float groundRadius;
        public LayerMask groundMask;
    }


    void Awake ()
    {
        controller = GetComponent<CharacterController> ();
    }

    void Update ()
    {
        // If velocity is not applied, gravity isn't aswell
        if (gravity.apply)
            ApplyGravity ();

        if (apply)
            ApplyPhysics ();
    }

    private void ApplyGravity ()
    {	
        gravity.wasGrounded = gravity.isGrounded;

        gravity.isGrounded = Physics.CheckSphere (transform.position, gravity.groundRadius, gravity.groundMask);
        gravity.isGrounded = gravity.isGrounded || controller.isGrounded;

        if (controller.isGrounded && velocity.y < 0.01f)
            velocity.y = 0f;
		
        velocity.y -= gravity.acceleration * Time.deltaTime;
    }

    private void ApplyPhysics ()
    {
        controller.Move (velocity * Time.deltaTime);
    }
}
