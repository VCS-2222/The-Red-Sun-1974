using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Needed Compponents")]
    [SerializeField] private CharacterController characterController;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private Transform groundCheck;

    [Header("Booleans of/for actions")]
    [SerializeField] public bool canMove;
    [SerializeField] private bool canRun;
    [SerializeField] private bool isGrounded;

    [Header("Player Settings")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float walkingSpeed;
    [SerializeField] private float runningSpeed;
    [SerializeField] private float crouchingSpeed;
    [SerializeField] private float gravity;

    Vector3 velocity;
    [HideInInspector]
    public Vector3 movementDirection;

    public movementStates states;
    public enum movementStates
    {
        idle,
        walking,
        running,
        crouching
    }
    [Header("Debug")]
    [SerializeField] float velocityMagnitudeDebugger;
    [SerializeField] float groundCheckSphereSize;

    [SerializeField] private float leftAndRightAxis;
    [SerializeField] private float backAndForthAxis;
    [SerializeField] public bool isRunning;
    [SerializeField] public bool isWalking;
    [SerializeField] public bool isCrouching;
    [SerializeField] private LayerMask walkableLayer;

    private void Start()
    {
        currentSpeed = walkingSpeed;
    }

    private void Update()
    {
        velocityMagnitudeDebugger = movementDirection.magnitude;

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckSphereSize, walkableLayer);

        if (!canMove)
            return;

        InputOfMovement();

        HandleDebugStates();
    }

    private void FixedUpdate()
    {
        velocity.y -= gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

        if (!canMove)
            return;

        movementDirection = transform.forward * backAndForthAxis + transform.right * leftAndRightAxis;

        characterController.Move(movementDirection.normalized * currentSpeed * Time.deltaTime);
        if (isGrounded)
        {
            movementDirection = transform.forward * backAndForthAxis + transform.right * leftAndRightAxis;

            characterController.Move(movementDirection.normalized * currentSpeed * Time.deltaTime);
        }

        if (movementDirection.magnitude > currentSpeed)
        {
            rigidBody.velocity = movementDirection.normalized * currentSpeed;
        }
    }

    void InputOfMovement()
    {
        leftAndRightAxis = Input.GetAxis("Horizontal");
        backAndForthAxis = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            currentSpeed = runningSpeed;
        }
        else if(isCrouching)
        {
            currentSpeed = crouchingSpeed;
        }
        else
        {
            currentSpeed = walkingSpeed;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            isCrouching = !isCrouching;

            HandleCrouch();
        }
    }

    void HandleCrouch()
    {
        if (isCrouching)
        {
            canRun = false;
            states = movementStates.crouching;
            currentSpeed = crouchingSpeed;
            characterController.height = 1.6f;
            characterController.center = new Vector3(0,0.1f,0);
        }
        else
        {
            canRun = true;
            characterController.height = 1.8f;
            characterController.center = new Vector3(0, 0.15f, 0);
        }
    }

    void HandleDebugStates()
    {
        if (movementDirection.magnitude > 0.5f && Input.GetKey(KeyCode.LeftShift) && canRun)
        {
            if (isCrouching)
            {
                canRun = false;
            }
            states = movementStates.running;
            isRunning = true;
            isWalking = false;
        }
        else if (movementDirection.magnitude > 0.5f)
        {
            states = movementStates.walking;
            isWalking = true;
            isRunning = false;
        }
        else if (movementDirection.magnitude < 0.2f)
        {
            states = movementStates.idle;
            isRunning = false;
            isWalking = false;
        }
        else if (movementDirection.magnitude > 0.5f && isCrouching)
        {
            states = movementStates.crouching;
            currentSpeed = crouchingSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(groundCheck.position, groundCheckSphereSize);
    }
}