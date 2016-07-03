using UnityEngine;


namespace CharacterStates
{
    public class BasicState : CharacterState
    {
        public SpeedInfo speed;
        public JumpInfo jump;


        [System.Serializable]
        public struct SpeedInfo
        {
            // Statistics
            public float grounded;
            public float airbourne;
            public float acceleration;
            public float jump;
            public float rotation;
            // Current state
            [HideInInspector] public float currentGrounded;
            [HideInInspector] public float currentAirbourne;
        }

        [System.Serializable]
        public struct JumpInfo
        {
            // Statistics
            public int multiple;
            public float rechargeTime;
            // Current state
            [HideInInspector] public float lastJump;
            [HideInInspector] public int left;
            [HideInInspector] public Vector3 preVelocity;
        }


        // Direction for character to follow
        protected override Vector3 inputDirection {
            get {
                // Get user input
                float horizontal = Input.GetAxisRaw ("Horizontal");
                float vertical = Input.GetAxisRaw ("Vertical");
                
                // Transform input to camera relative direction
                Vector3 direction = new Vector3 (horizontal, 0, vertical);
                // Transform input relative to camera view (natural for player)
                direction = cam.transform.TransformDirection (direction);
                // Player moves in x,z directions only
                direction.y = 0;
                direction.Normalize ();
                return direction;
            }
        }

        // Implementing interface of CharacterState

        public override void OnBegin ()
        {
            Debug.Log ("I'm starting DefaultState: " + character);
        }

        public override void OnEnd ()
        {
            Debug.Log ("I'm ending DefaultState: " + character);
        }

        public override void Animate ()
        {
            // Nothing atm
        }

        public override void Behave ()
        {
            Move ();
        }

        private void Move ()
        {
            if (physics.gravity.isGrounded)
                HandleGroundMove ();
            else
                HandleAirMove ();
        }

        private void HandleGroundMove ()
        {
            Vector3 direction = inputDirection;
            bool idle = direction == Vector3.zero;
            bool jumping = Input.GetButtonDown ("Jump");

            // Don't apply rotation if user is idle
            if (!idle) {
                // Rotate
                direction = Vector3.RotateTowards (transform.forward, direction, speed.rotation * Time.deltaTime, 0f);
                transform.rotation = Quaternion.LookRotation (direction);
            }

            // Apply speed
            float targetSpeed = idle ? 0f : speed.grounded;
            speed.currentGrounded = Mathf.MoveTowards (speed.currentGrounded, targetSpeed, speed.acceleration * Time.deltaTime);
            direction *= speed.currentGrounded;

            // Apply gravity or jump, depending on player's choice
            direction.y = jumping ? speed.jump : physics.velocity.y;

            // Pass velocity to physics
            physics.velocity = direction;
        }

        private void HandleAirMove ()
        {
            // Player just lost contact with ground
            if (physics.gravity.wasGrounded)
                OnAirbourne ();

            Vector3 direction = inputDirection;
            bool idle = direction == Vector3.zero;
            bool jumping = Input.GetButtonDown ("Jump");


            // Apply speed
            float targetSpeed = idle ? 0f : speed.airbourne;
            speed.currentAirbourne = Mathf.MoveTowards (speed.currentAirbourne, targetSpeed, speed.acceleration * Time.deltaTime);
            // Airborne movement is additive to ground movement
            direction = jump.preVelocity + direction * speed.currentAirbourne;
            direction.y = physics.velocity.y;

            // Multiple jump handling
            if (jumping && jump.left > 0)
            if (Time.time - jump.lastJump > jump.rechargeTime) {
                jump.left -= 1;
                jump.lastJump = Time.time;

                direction.y = speed.jump;
            }

            // Pass velocity to physics
            physics.velocity = direction;
        }

        private void OnAirbourne ()
        {
            jump.lastJump = Time.time;
            jump.left = jump.multiple;
            jump.preVelocity = physics.velocity;
        }
    }
}