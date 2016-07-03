using UnityEngine;
using System.Collections;


namespace CharacterStates
{
    public class HurtState : CharacterState
    {
        public override void OnBegin ()
        {
            Debug.Log ("I'm starting HurtState: " + character);

            
        }

        public override void OnEnd ()
        {
            Debug.Log ("I'm ending HurtState: " + character);
        }

        public override void Animate ()
        {
            // Nothing atm
        }

        public override void Behave ()
        {
            // TODO
        }

        protected override Vector3 inputDirection {
            get { return Vector3.zero; }
        }
    }
}