using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneStarter : MonoBehaviour
{
    [Header("Essential Components")]
    public GameObject playerCharacter;
    public float secondsOfCutscene;

    public void OnTriggerEnter(Collider other)
    {
        StartCoroutine(afterCut());
    }

    IEnumerator afterCut()
    {
        this.transform.GetComponent<Collider>().enabled = false;
        playerCharacter.GetComponent<PlayerMovement>().canMove = false;

        yield return new WaitForSeconds(secondsOfCutscene);

        playerCharacter.GetComponent<PlayerMovement>().canMove = true;
    }
}