using UnityEngine;
using System.Collections;

public class MakeTarget : MonoBehaviour
{

    public SheepState sheep;


    void OnTriggerEnter (Collider col)
    {

        if (col.CompareTag ("Player")) {
            sheep.target = col.transform;
            sheep.chasing = true;
        }
    }

    void OnTriggerExit (Collider col)
    {
        if (col.CompareTag ("Player")) {
            sheep.target = col.transform;
            sheep.chasing = false;
        }
    }
}
