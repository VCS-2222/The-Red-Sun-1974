using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutomatedLift : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(startLift());
        }
    }

    IEnumerator startLift()
    {
        yield return new WaitForSeconds(2f);

        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("start lift");
    }
}
