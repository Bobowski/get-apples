using UnityEngine;


public class Character : MonoBehaviour
{
    [Header ("Properties")]
    [SerializeField] private Health health;

    [Header ("States")]
    [SerializeField] private CharacterState state;
    [SerializeField] private StatesInfo states;

    [System.Serializable]
    public struct StatesInfo
    {
        public CharacterState basic;
        public CharacterState hurt;
        public CharacterState dead;
    }

    public StatesInfo States {
        get { return states; }
    }

    public CharacterState State {
        get { return state; }
        set {
            state.OnEnd ();
            state = value;
            state.OnBegin ();
        }
    }


    // Passing actions onto proper objects
    void Awake ()
    {
    }

    void Start ()
    {
        state.OnBegin ();
    }

    void Update ()
    {
        state.Behave ();
        state.Animate ();
    }


    // Character public functions
    public bool IsAlive {
        get { return health.Alive; }
    }

    public void ObtainDamage (Character dealer, int damage)
    {
        Debug.Log ("Obtaining damage " + damage + " from " + dealer);

        if (IsAlive) {
            health.Current -= damage;

            if (!IsAlive)
                Die ();
        }
    }

    private void Die ()
    {
        Debug.Log ("I'm dead: " + this);
    }

    public void Respawn ()
    {
        Debug.Log ("I'm alive again: " + this);
    }
}
