using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivationPlayerOutfitChanger : MonoBehaviour
{
    public string outfitName;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<PlayerOutfitChanger>().ChangeOutfit(outfitName);
        }
    }
}
