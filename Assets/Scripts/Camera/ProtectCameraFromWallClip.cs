using UnityEngine;
using System.Collections;
using System;


public class ProtectCameraFromWallClip : MonoBehaviour
{
    public float clipMoveTime = 0.08f;
    public float returnTime = 0.4f;
    public float sphereCastRadius = 0.3f;
    public float closestDistance = 1.2f;
    public string dontClipTag = "Player";

    public bool protecting { get; private set; }


    private CameraController controller;
    private float moveVelocity;
    private float currentDistance;
    private RayHitComparer rayHitComparer;


    void Awake ()
    {
        // Assign variables
        controller = GetComponent <CameraController> ();
        rayHitComparer = new RayHitComparer ();
        
        // Begin with desired distance
        currentDistance = controller.distance;
    }

    void LateUpdate ()
    {
        AdjustCameraPosition ();    
    }

    private void AdjustCameraPosition ()
    {
        // Initially set the target distance
        float targetDistance = controller.distance;
        
        // Set ray for shpere check
        Ray ray = new Ray (controller.pivot.position, -transform.forward);
        
        // Initially nothing is hit
        bool hitSomething = false;

        // Raycast sphere out of it
        RaycastHit[] hits = Physics.SphereCastAll (ray, sphereCastRadius, controller.distance + sphereCastRadius);
        
        // Sort the collisions by distance
        Array.Sort (hits, rayHitComparer);
        
        // Set the variable used for storing the closest to be as far as possible
        float nearest = Mathf.Infinity;
        foreach (var hit in hits) {
            // Only deal with the collision if it was closer than the previous one, not a trigger, and not attached to a rigidbody tagged with the dontClipTag
            if (hit.distance < nearest && !hit.collider.isTrigger && !hit.collider.CompareTag (dontClipTag)) {
                nearest = hit.distance;
                targetDistance = hit.distance;
                hitSomething = true;
            }
        }
       
        // Debug 
        // if (hitSomething)
        // {
        //     Debug.DrawLine (transform.position, transform.forward * targetDistance, Color.red, 1f);
        // }
        
        protecting = hitSomething;
        currentDistance = Mathf.SmoothDamp (currentDistance, targetDistance, ref moveVelocity, currentDistance > targetDistance ? clipMoveTime : returnTime);
        currentDistance = Mathf.Clamp (currentDistance, closestDistance, controller.distance);
        transform.position = controller.pivot.position - transform.forward * currentDistance;
    }
    
    // Comparer for check distances in ray cast hits
    public class RayHitComparer : IComparer
    {
        public int Compare (object x, object y)
        {
            return ((RaycastHit)x).distance.CompareTo (((RaycastHit)y).distance);
        }
    }
}
