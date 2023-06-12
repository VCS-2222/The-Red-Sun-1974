using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivationPlayerOutfitChanger : MonoBehaviour
{
    public string outfitName;
    public AudioSource changeSound;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            changeSound.Play();
            other.GetComponent<PlayerOutfitChanger>().ChangeOutfit(outfitName);
            Destroy(gameObject, 2f);
        }
    }
}
