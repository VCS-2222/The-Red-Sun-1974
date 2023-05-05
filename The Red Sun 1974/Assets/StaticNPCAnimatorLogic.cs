using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticNPCAnimatorLogic : MonoBehaviour
{
    Animator animator;
    [SerializeField] bool isSitting;
    [SerializeField] bool isComfortable;
    [SerializeField] bool isStressed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (isSitting)
        {
            animator.SetBool("sitting", true);

            if (isComfortable)
            {
                animator.SetBool("comfy", true);
            }
            else if (isStressed)
            {
                animator.SetBool("stress", true);
            }
            else
            {
                animator.SetBool("normal", true);
            }
        }
        else
        {
            animator.SetBool("standing", true);

            if (isComfortable)
            {
                animator.SetBool("comfy", true);
            }
            else if (isStressed)
            {
                animator.SetBool("stress", true);
            }
            else
            {
                animator.SetBool("normal", true);
            }
        }
    }
}
