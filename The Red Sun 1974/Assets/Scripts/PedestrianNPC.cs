using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianNPC : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("NavMeshAgent Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float destinationFix;

    [Header("Action bools")]
    [SerializeField] private bool canWander;
    [SerializeField] private bool canStopAtPoint;
    [SerializeField] private bool hasPresetPath;
    [SerializeField] private bool hasSetDestination;
    [SerializeField] bool hasRandomSpeed;

    [Header("Travel")]
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] public Transform finalDestination;
    [SerializeField] int currentPathPoint;
    [SerializeField] float roamReach;

    private void Start()
    {
        if (hasRandomSpeed)
        {
            agent.speed = Random.Range(0.7f, 1.3f);
        }
        else
        {
            agent.speed = speed;
        }
        agent.angularSpeed = turnSpeed;
        agent.stoppingDistance = stoppingDistance;
        animator.speed = speed;
    }

    private void Update()
    {
        if(agent.velocity != Vector3.zero)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if (hasPresetPath)
        {
            canWander = false;
            DoPresetPath();
        }

        if (canWander)
        {
            hasPresetPath = false;
            DoWonder();
        }

        if (canStopAtPoint)
        {

        }

        if (hasSetDestination)
        {
            TravelToFinalDestination();
        }
    }

    private void DoPresetPath()
    {
        if (agent.remainingDistance < destinationFix)
        {
            currentPathPoint++;
            for (int i = 0; i < currentPathPoint; i++)
            {
                if (currentPathPoint > pathWaypoints.Length)
                {
                    currentPathPoint = 0;
                    agent.SetDestination(pathWaypoints[i].position);
                }
                else
                {
                    agent.SetDestination(pathWaypoints[i].position);
                }
            }
        }
        else
        {

        }
    }

    void DoWonder()
    {
        if (agent.remainingDistance < destinationFix)
        {
            Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * roamReach;
            Debug.DrawRay(randomPoint, Vector3.up, Color.red);

            agent.SetDestination(randomPoint);
        }
        else
        {

        }
    }

    public void TravelToFinalDestination()
    {
        canWander = false;
        hasPresetPath = false;
        agent.destination = finalDestination.position;
    }
}
