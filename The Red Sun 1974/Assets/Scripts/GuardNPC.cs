using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardNPC : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] Animator animator;

    [Header("NavMeshAgent Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float turnSpeed;
    [SerializeField] private float stoppingDistance;
    [SerializeField] private float destinationFix;

    [Header("Action bools")]
    [SerializeField] private bool canWander;
    [SerializeField] private bool canStopAtPoint;
    [SerializeField] private bool hasPresetPath;
    [SerializeField] private bool hasSetDestination;
    [SerializeField] bool hasRandomSpeed;

    [Header("Guard Factors")]
    [SerializeField] private bool hasGun;
    [SerializeField] private bool chasingPlayer;
    [SerializeField] float sight;

    [Header("Travel")]
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private Transform finalDestination;
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
            speed = walkSpeed;
            agent.speed = speed;
        }

        agent.angularSpeed = turnSpeed;
        agent.stoppingDistance = stoppingDistance;
        animator.speed = speed;
    }

    private void Update()
    {
        Sight();

        if (agent.velocity != Vector3.zero)
        {
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
        }

        if(agent.velocity != Vector3.zero && chasingPlayer)
        {
            animator.SetBool("run", true);
            speed = runSpeed;
            agent.speed = speed;
        }
        else
        {
            animator.SetBool("run", false);
            speed = walkSpeed;
            agent.speed = speed;
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

        if (hasSetDestination || chasingPlayer)
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

    void TravelToFinalDestination()
    {
        canWander = false;
        hasPresetPath = false;
        agent.destination = finalDestination.position;
    }

    void Sight()
    {
        RaycastHit hit;
        Physics.BoxCast(this.transform.position, this.transform.localScale, this.transform.forward, out hit, this.transform.rotation, sight);

        if(hit.collider.enabled == true && hit.collider.tag == "Player")
        {
            finalDestination = hit.collider.transform;
            chasingPlayer = true;
        }
        else
        {

        }
    }
}
