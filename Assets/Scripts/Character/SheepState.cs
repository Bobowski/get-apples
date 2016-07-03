using UnityEngine;
using System.Collections;

public class SheepState : MonoBehaviour
{
    public bool chasing;
    public Transform target;
    public Transform waypoints;

    private Transform current;
    private NavMeshAgent agent;
    // Use this for initialization
    void Start ()
    {
        agent = GetComponent<NavMeshAgent> ();

        int a = Random.Range (0, waypoints.childCount);
        agent.SetDestination (waypoints.GetChild (a).position);
        current = waypoints.GetChild (a);
    }


    void Update ()
    {
        if (chasing) {
            agent.SetDestination (target.position);
        } else {
            if (agent.remainingDistance < 0.05f) {
                int a = Random.Range (0, waypoints.childCount);
                current = waypoints.GetChild (a);
                agent.SetDestination (current.position);
            }
        }

    }

    void OnTriggerEnter (Collider other)
    {
        GameFlowManager.Ins.LoseGame ();
    }
}
