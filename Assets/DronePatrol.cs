using UnityEngine;
using UnityEngine.AI;

public class DronePatrol : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;

    private NavMeshAgent agent;
    private Transform target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = pointA;
    }

    void Update()
    {
        agent.SetDestination(target.position);

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance < 0.5f)
        {
            if (target == pointA)
                target = pointB;
            else
                target = pointA;
        }
    }
}