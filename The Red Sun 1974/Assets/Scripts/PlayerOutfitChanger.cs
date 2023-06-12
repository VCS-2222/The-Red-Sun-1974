using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitChanger : MonoBehaviour
{
    [Header("Materials")]
    public SkinnedMeshRenderer playerOutfit;
    [SerializeField] Material prisonOutfit;
    //[SerializeField] Material casualOutfit;
    [SerializeField] Material constructionWorkerOutfit;
    [SerializeField] Material rebelOutfit;
    [SerializeField] Material maintananceWorkerOutfit;
    [SerializeField] Material fancySuitOutfit;
    [SerializeField] Material fancyCasualOutfit;

    public void ChangeOutfit(string name)
    {
        if (name == "PrisonOutfit")
        {
            playerOutfit.material = prisonOutfit;
        }

        if (name == "ConstructionWorkerOutfit")
        {
            playerOutfit.material = constructionWorkerOutfit;
        }

        if (name == "RebelOutfit")
        {
            playerOutfit.material = rebelOutfit;
        }

        if (name == "MaintananceWorkerOutfit")
        {
            playerOutfit.material = maintananceWorkerOutfit;
        }

        if (name == "FancySuitOutfit")
        {
            playerOutfit.material = fancySuitOutfit;
        }

        if (name == "FancyCasualOutfit")
        {
            playerOutfit.material = fancyCasualOutfit;
        }
    }
}
