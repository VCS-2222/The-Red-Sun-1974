using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [Header("Referanced Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMoveScript;
    [SerializeField] private PlayerCamera playerCameraScript;

    [Header("Parameters")]
    float xAxis;
    float yAxis;
    [SerializeField] float xParameter;
    [SerializeField] float yParameter;
    public PlayerBindings animatorParameters;

    [SerializeField] private float transforSpeed;
    public float currentMaxParameter;

    [Header("Bools of actions")]
    public bool isWalking;
    public bool isRunning;
    public bool isCrouching;

    public bool walkingForward;
    public bool walkingBackward;
    public bool walkingRight;
    public bool walkingLeft;

    private void Awake()
    {
        animatorParameters = new PlayerBindings();
        animatorParameters.Player.BodyMovement.performed += t => HandleAxis(t.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        animatorParameters.Enable();
    }

    private void OnDisable()
    {
        animatorParameters.Disable();
    }

    void HandleAxis(Vector2 axis)
    {
        yAxis = axis.y;
        xAxis = axis.x;
    }

    private void Update()
    {
        isWalking = playerMoveScript.isWalking;
        isRunning = playerMoveScript.isRunning;
        isCrouching = playerMoveScript.isCrouching;

        if (isRunning)
        {
            currentMaxParameter = 1f;
        }
        else
        {
            currentMaxParameter = 0.5f;
        }

        animator.SetBool("crouched", isCrouching);

        walkingForward = yAxis > 0.2f;
        walkingBackward = yAxis < -0.2f;
        walkingRight = xAxis > 0.2f;
        walkingLeft = xAxis < -0.2f;

        DecreaseAnimatorParametersIfNotUsed();
        UpdateAnimatorParameters();
    }

    void UpdateAnimatorParameters()
    {
        if (walkingForward && yParameter < currentMaxParameter)
        {
            yParameter += Time.deltaTime * transforSpeed;
        }

        if (walkingBackward && yParameter > -currentMaxParameter)
        {
            yParameter -= Time.deltaTime * transforSpeed;
        }

        if (walkingRight && xParameter < currentMaxParameter)
        {
            xParameter += Time.deltaTime * transforSpeed;
        }

        if (walkingLeft && xParameter > -currentMaxParameter)
        {
            xParameter -= Time.deltaTime * transforSpeed;
        }



        if (walkingForward && yParameter > currentMaxParameter)
        {
            yParameter = currentMaxParameter;
        }

        if (walkingBackward && yParameter < -currentMaxParameter)
        {
            yParameter = -currentMaxParameter;
        }

        if (walkingRight && xParameter > currentMaxParameter)
        {
            xParameter = currentMaxParameter;
        }

        if (walkingLeft && xParameter < -currentMaxParameter)
        {
            xParameter = -currentMaxParameter;
        }

        animator.SetFloat("x", xParameter);
        animator.SetFloat("y", yParameter);
    }

    void DecreaseAnimatorParametersIfNotUsed()
    {
        if (!walkingForward && yParameter > 0)
        {
            yParameter -= Time.deltaTime * transforSpeed;
            if(yParameter < 0)
            {
                yParameter = 0;
            }
        }

        if (!walkingBackward && yParameter < 0)
        {
            yParameter += Time.deltaTime * transforSpeed;
            if (yParameter > 0)
            {
                yParameter = 0;
            }
        }

        if (!walkingRight && xParameter > 0)
        {
            xParameter -= Time.deltaTime * transforSpeed;
            if (xParameter < 0)
            {
                xParameter = 0;
            }
        }

        if (!walkingLeft && xParameter < 0)
        {
            xParameter += Time.deltaTime * transforSpeed;
            if (xParameter > 0)
            {
                xParameter = 0;
            }
        }
    }

    public void SittingMechanicOn()
    {
        playerMoveScript.canMove = false;
        animator.SetBool("sitting", true);
        if(playerMoveScript.isCrouching == true)
        {
            playerMoveScript.HandleCrouch();
        }
        playerCameraScript.canRotate = false;
        playerMoveScript.GetComponent<Rigidbody>().isKinematic = true;
        playerMoveScript.GetComponent<CharacterController>().enabled = false;
    }

    public void SittingMechanicOff()
    {
        playerMoveScript.GetComponent<CharacterController>().enabled = true;
        animator.SetBool("sitting", false);
        playerMoveScript.GetComponent<Rigidbody>().isKinematic = false;
        playerCameraScript.canRotate = true;
        playerMoveScript.canMove = true;
    }

    public void UseAnimatorArmLayer()
    {
        int armLayer = animator.GetLayerIndex("Arms");

        animator.SetLayerWeight(armLayer, 1f);
    }

    public void UnuseAnimatorArmLayer()
    {
        int armLayer = animator.GetLayerIndex("Arms");

        animator.SetLayerWeight(armLayer, 0f);
    }
}
