using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    [Header("Referanced Components")]
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMoveScript;

    [Header("Parameters")]
    float xAxis;
    float yAxis;
    [SerializeField] float xParameter;
    [SerializeField] float yParameter;

    [SerializeField] private float transforSpeed;
    public float currentMaxParameter;

    [Header("Bools of actions")]
    public bool isWalking;
    public bool isRunning;

    public bool walkingForward;
    public bool walkingBackward;
    public bool walkingRight;
    public bool walkingLeft;

    private void Update()
    {
        isWalking = playerMoveScript.isWalking;
        isRunning = playerMoveScript.isRunning;

        yAxis = Input.GetAxis("Vertical");
        xAxis = Input.GetAxis("Horizontal");

        if (isRunning)
        {
            currentMaxParameter = 1f;
        }
        else
        {
            currentMaxParameter = 0.5f;
        }

        walkingForward = yAxis > 0;
        walkingBackward = yAxis < 0;
        walkingRight = xAxis > 0;
        walkingLeft = xAxis < 0;

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
