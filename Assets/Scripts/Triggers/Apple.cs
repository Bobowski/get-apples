using UnityEngine;
using System.Collections;

public class Apple : Triggerable
{
    public override void OnEnter (Transform transform)
    {
        AppleManager.Ins.CollectedApples += 1;
        gameObject.SetActive (false);
    }

    public override void OnExit (Transform transform)
    {
    }

    public override void OnStay (Transform transform)
    {
    }
}
