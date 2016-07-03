using UnityEngine;
using System.Collections;

public class GateOpener : Triggerable
{
    public Transform left;
    public Transform right;

    public float speed;

    private bool opening;

    public override void OnEnter (Transform transform)
    {
        StartCoroutine ("Open");
    }

    public override void OnExit (Transform transform)
    {
        StartCoroutine ("Close");
    }

    public override void OnStay (Transform transform)
    {
    }

    private IEnumerator Open ()
    {
        StopCoroutine ("Close");

        while (left.rotation.eulerAngles.y < 120f || left.rotation.eulerAngles.y > 150f) {
            left.Rotate (0, speed * Time.deltaTime, 0);
            right.Rotate (0, -speed * Time.deltaTime, 0);
            yield return 0;
        }
    }

    private IEnumerator Close ()
    {
        StopCoroutine ("Open");

        while (left.rotation.eulerAngles.y > 0f && left.rotation.eulerAngles.y < 150f) {
            left.Rotate (0, -speed * Time.deltaTime, 0);
            right.Rotate (0, speed * Time.deltaTime, 0);
            yield return 0;
        }
    }
}
