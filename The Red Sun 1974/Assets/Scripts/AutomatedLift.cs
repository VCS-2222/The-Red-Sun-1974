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
        this.transform.GetComponentInChildren<AudioSource>().Play();

        yield return new WaitForSeconds(2f);

        Animator anim = GetComponent<Animator>();
        anim.SetTrigger("start lift");

        yield return new WaitForSeconds(35f);

        this.transform.GetComponentInChildren<AudioSource>().Stop();
    }
}
