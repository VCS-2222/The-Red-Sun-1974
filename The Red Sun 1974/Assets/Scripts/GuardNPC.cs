using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardNPC : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] Animator animator;
    [SerializeField] Transform sightPoint;
    [SerializeField] BoxCollider deathTriggerCollider;

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
    [SerializeField] public bool chasingPlayer;
    [SerializeField] float sight;

    [Header("Travel")]
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] public Transform finalDestination;
    [SerializeField] int currentPathPoint;
    [SerializeField] float roamReach;

    [Header("Audio")]
    [SerializeField] AudioSource screamSource;
    [SerializeField] AudioClip[] screams;

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

    private void FixedUpdate()
    {
        Sight();
    }

    private void Update()
    {
        SetStateOfDeathTirgger();
        Scream();

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
        if(Physics.Raycast(sightPoint.position, sightPoint.forward, out hit, sight))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.DrawLine(sightPoint.position, hit.point, Color.red);
                finalDestination = hit.collider.transform;
                chasingPlayer = true;
            }
            else
            {
                return;
            }
        }
        else
        {
            return;
        }
    }

    public void Scream()
    {
        if (chasingPlayer)
        {
            if (screamSource.isPlaying)
                return;

            StartCoroutine(screamAtPlayer());
        }
    }

    IEnumerator screamAtPlayer()
    {
        yield return new WaitForSeconds(5f);

        screamSource.PlayOneShot(screams[Random.Range(0, screams.Length)]);
    }

    void SetStateOfDeathTirgger()
    {
        if (chasingPlayer)
        {
            deathTriggerCollider.enabled = true;
        }
        else
        {
            deathTriggerCollider.enabled = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerLoseState>().EnableLoseState("Caught by Guard");
        }
    }
}
