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

    [Header("Travel")]
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] int currentPathPoint;
    [SerializeField] float roamReach;

    private void Start()
    {
        agent.speed = speed;
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
}