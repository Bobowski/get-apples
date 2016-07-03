using UnityEngine;
using System.Collections;

public abstract class Triggerable : MonoBehaviour
{
    public string triggerTag;

    public abstract void OnEnter (Transform transform);

    public abstract void OnExit (Transform transform);

    public abstract void OnStay (Transform transform);


    void OnTriggerEnter (Collider col)
    {
        if (col.CompareTag (triggerTag))
            OnEnter (col.transform);
    }

    void OnTriggerExit (Collider col)
    {
        if (col.CompareTag (triggerTag))
            OnExit (col.transform);
    }

    void OnTriggerStay (Collider col)
    {
        if (col.CompareTag (triggerTag))
            OnStay (col.transform);
    }
}
