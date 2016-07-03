using UnityEngine;


namespace CameraStates
{
    public class DefaultState : CameraState
    {
        // Distance, speed & clamp angle
        public float ySpeed;
        public float xzSpeed;
        public float clampAngle;
        
        // Last update variables
        private Vector3 lastPosition;

        public override void OnBegin ()
        {            
            Debug.Log ("I'm starting DefaultState: " + this);
            
            // Assign reference to player transform
            controller.pivot = GameObject.FindGameObjectWithTag ("Player").transform;
            controller.pivot = controller.pivot.FindChild ("Pivot");
            
            SaveLastPosition ();
            
            //Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnEnd ()
        {
            Debug.Log ("I'm ending DefaultState: " + this.ToString ());
            throw new System.NotImplementedException ();
        }

        public override void Behave ()
        {
            LoadLastPosition ();
            AdjustRotation ();
            RotateY ();
            RotateXZ ();
            SaveLastPosition ();
        }

        private void LoadLastPosition ()
        {
            transform.position = controller.pivot.position + lastPosition;
        }

        private void SaveLastPosition ()
        {
            lastPosition = transform.position - controller.pivot.position; 
            // Clamp distance bewteen camera and pivot to distance variable
            lastPosition.Normalize ();
            lastPosition *= controller.distance;
        }

        private void AdjustRotation ()
        {
            transform.LookAt (controller.pivot.position);
        }

        private void RotateY ()
        {
            // User input
            float direction = Input.GetAxisRaw ("Mouse X");
            
            // Rotate around Y axis
            transform.RotateAround (controller.pivot.position, Vector3.up, direction * ySpeed * Time.deltaTime);
        }

        private void RotateXZ ()
        {
            // User input
            float direction = Input.GetAxisRaw ("Mouse Y");
            
            // Relative camera-to-pivot vector & 2D direction
            Vector3 relPosition = controller.pivot.position - transform.position;
            Vector3 relDirection = relPosition;
            relDirection.y = 0f;
            
            // Delta angle that will be added on rotation
            float delta = direction * xzSpeed * Time.deltaTime;
            // Current rotation angle between camera and y=0 axis
            float angle = Vector3.Angle (relPosition, relDirection);
            // Is camera currently above or below pivot (y-axis)
            float isUp = (relPosition.y > 0) ? 1.0f : -1.0f;
            // Axis to rotate around
            Vector3 rotateAxis = Vector3.Cross (relPosition, Vector3.up);
            // Check if new angle will exceed clampAngle
            if (angle + delta * isUp < clampAngle)
                transform.RotateAround (controller.pivot.position, rotateAxis, delta);        
        }
    }
}