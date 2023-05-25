using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WorkingNPC : MonoBehaviour
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
    [SerializeField] private bool hasPresetPath;
    [SerializeField] private bool hasSetDestination;
    [SerializeField] bool hasRandomSpeed;

    [Header("Things Being Carried")]
    [SerializeField] GameObject box;
    [SerializeField] GameObject longObject;
    [SerializeField] GameObject oneHanded;

    [Header("Animator bools")]
    [SerializeField] bool isCarrying;
    [SerializeField] bool carryingHeavy;

    [Header("Travel")]
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private Transform finalDestination;
    [SerializeField] int currentPathPoint;

    private void Start()
    {
        if (isCarrying)
        {
            animator.SetBool("carry", true);

            if (carryingHeavy)
            {
                box.SetActive(true);
                animator.SetBool("box", true);
            }
            else
            {
                longObject.SetActive(true);
                animator.SetBool("long", true);
            }
        }
        else
        {
            animator.SetBool("carry", false);

            if (carryingHeavy)
            {

            }
            else
            {
                oneHanded.SetActive(true);
                animator.SetBool("long", true);
            }
        }

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
        if (hasPresetPath)
        {
            DoPresetPath();
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

    void TravelToFinalDestination()
    {
        hasPresetPath = false;
        agent.destination = finalDestination.position;
    }
}
