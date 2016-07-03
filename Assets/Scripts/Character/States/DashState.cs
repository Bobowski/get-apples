using UnityEngine;
using System.Collections;

namespace CharacterStates
{
    public class DashState : BasicState
    {
        public DashInfo dash;

        [System.Serializable]
        public class DashInfo
        {
            public float distance;
            public float time;
            public float rechargeTime;
            public int multiple;

            [HideInInspector] public bool isDashing;
            [HideInInspector] public float lastDashTime;
            [HideInInspector] public Vector3 preVelocity;
            [HideInInspector] public int left;
        }

        public override void OnBegin ()
        {
            Debug.Log ("I'm starting DashState: " + character);
        }

        public override void OnEnd ()
        {
            // Cleanup
            StopDash (force: true);

            Debug.Log ("I'm ending DashState: " + character);
        }

        public override void Animate ()
        {
            // Nothing atm
        }

        public override void Behave ()
        {
            if (!dash.isDashing)
                base.Behave ();

            HandleDash ();
        }

        private void HandleDash ()
        {
            // Get user input
            bool idle = inputDirection == Vector3.zero;
            bool dashing = Input.GetButtonDown ("Fire1");

            if (physics.gravity.wasGrounded)
                OnAirbourne ();

            if (dashing && !idle && (physics.gravity.isGrounded || dash.left > 0))
            if (Time.time - dash.lastDashTime > dash.rechargeTime)
                StartCoroutine ("DashCoroutine");
            
        }

        private IEnumerator DashCoroutine ()
        {
            StartDash ();
            yield return new WaitForSeconds (dash.time);
            StopDash ();
        }

        private void StartDash ()
        {
            // Calculate velocity of movement while dashing
            float velocity = dash.distance / dash.time;

            // Adjust rotation
            transform.rotation = Quaternion.LookRotation (inputDirection);

            dash.preVelocity = physics.velocity;
            dash.left -= 1;
            physics.velocity = inputDirection * velocity;
            physics.gravity.apply = false;
            dash.isDashing = true;
        }

        private void StopDash (bool force = false)
        {
            if (force)
                StopCoroutine ("DashCoroutine");

            dash.isDashing = false;
            physics.gravity.apply = true;
            physics.velocity = dash.preVelocity;
            dash.lastDashTime = Time.time;
        }

        private void OnAirbourne ()
        {
            dash.left = dash.multiple;
        }
    }
}