using UnityEngine;


[System.Serializable]
public struct Health
{
    [SerializeField] private int max;
    [SerializeField] private int current;


    public int Max {
        get { return max; } 
        set { max = Mathf.Max (value, 0); }
    }

    public int Current {
        get { return current; }
        set { current = Mathf.Clamp (value, 0, max); }
    }

    public bool Alive {
        get { return current > 0; }
    }
}