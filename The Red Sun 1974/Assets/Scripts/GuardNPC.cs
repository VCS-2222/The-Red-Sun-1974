using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardNPC : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] float debugRemainingDistance;

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
    public bool isYelling;
    [SerializeField] AudioSource gunSource;
    [SerializeField] AudioClip shootGun;
    [SerializeField] AudioClip reloadGun;

    [Header("Gun Factors")]
    [SerializeField] int currentBulletsInMag;
    [SerializeField] int maxBulletsInMag;
    [SerializeField] float reloadTime;
    [SerializeField] bool isAimingGun;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform shootPoint;
    [SerializeField] bool isReloading;

    private void Start()
    {
        currentPathPoint = 0;
        currentBulletsInMag = maxBulletsInMag;
        if (hasRandomSpeed)
        {
            agent.speed = Random.Range(0.7f, 1.3f);
        }
        else
        {
            speed = walkSpeed;
            agent.speed = speed;
        }

        if (hasGun)
        {
            animator.SetBool("hasGun", true);
        }

        agent.angularSpeed = turnSpeed;
        agent.stoppingDistance = stoppingDistance;
        animator.speed = speed;
    }

    private void FixedUpdate()
    {
        Sight();

        if (hasPresetPath)
        {
            canWander = false;
            DoPresetPath();
        }

        Scream();

        if (chasingPlayer && hasGun)
        {
            ChasePlayerWhileArmed();
        }
    }

    private void Update()
    {
        debugRemainingDistance = agent.remainingDistance;
        SetStateOfDeathTirgger();

        if (agent.velocity != Vector3.zero && !isAimingGun)
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

        if (canWander)
        {
            hasPresetPath = false;
            DoWonder();
        }

        if (hasSetDestination || chasingPlayer && !hasGun)
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

    void ChasePlayerWhileArmed()
    {
        float remDis = Vector3.Distance(this.transform.position, finalDestination.position);
        if (remDis < sight)
        {
            agent.Stop();
            if(currentBulletsInMag > 0 && !isAimingGun)
            {
                isAimingGun = true;
                StartCoroutine(AimAndShootTarget());
            }
            
            if(currentBulletsInMag <= 0 && isAimingGun && !isReloading)
            {
                StartCoroutine(ReloadGun());
            }
        }

        if (remDis >= sight)
        {
            agent.Resume();
            TravelToFinalDestination();
        }
    }

    IEnumerator AimAndShootTarget()
    {
        transform.LookAt(finalDestination);
        animator.SetBool("isAiming", true);

        yield return new WaitForSeconds(.5f);

        GameObject bf = Instantiate(bulletPrefab, shootPoint);
        gunSource.PlayOneShot(shootGun);
        currentBulletsInMag--;
        bf.GetComponent<Rigidbody>().AddForce(transform.forward * 800);

        yield return new WaitForSeconds(.5f);

        animator.SetBool("isAiming", false);
        isAimingGun = false;
    }

    IEnumerator ReloadGun()
    {
        isReloading = true;
        print("Reloading gun");

        gunSource.PlayOneShot(reloadGun);
        yield return new WaitForSeconds(reloadTime);

        currentBulletsInMag = maxBulletsInMag;
        isReloading = false;
        print("Reload Done");
    }

    void Sight()
    {
        RaycastHit hit;
        if(Physics.Raycast(sightPoint.position, sightPoint.forward, out hit, sight))
        {
            if (hit.collider.tag == "Player")
            {
                
                string name = hit.transform.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().materials[0].name;

                Debug.Log("Detected object " + name);

                if ( name == "PrisonOutfit (Instance)")
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
        else
        {
            return;
        }
    }

    public void Scream()
    {
        if (chasingPlayer && !isYelling)
        {
            if (screamSource.isPlaying)
            {
                StopCoroutine(screamAtPlayer());
                return;
            }

            StartCoroutine(screamAtPlayer());
        }
        else
        {
            StopCoroutine(screamAtPlayer());
        }
    }

    IEnumerator screamAtPlayer()
    {
        isYelling = true;
        yield return new WaitForSeconds(5f);

        screamSource.PlayOneShot(screams[Random.Range(0, screams.Length)]);
        isYelling = false;
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
