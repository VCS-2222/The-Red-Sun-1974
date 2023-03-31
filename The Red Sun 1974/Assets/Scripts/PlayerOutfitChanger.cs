using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOutfitChanger : MonoBehaviour
{
    [Header("Materials")]
    [SerializeField] SkinnedMeshRenderer playerOutfit;
    //[SerializeField] Material prisonOutfit;
    //[SerializeField] Material casualOutfit;
    //[SerializeField] Material constructionWorkerOutfit;
    //[SerializeField] Material rebelOutfit;
    //[SerializeField] Material maintananceWorkerOutfit;
    [SerializeField] Material fancySuitOutfit;
    [SerializeField] Material fancyCasualOutfit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeOutfit("fancySuitOutfit");
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ChangeOutfit("fancyCasualOutfit");
        }
    }

    public void ChangeOutfit(string name)
    {
        //if(name == "prisonOutfit")
        //{
        //    playerOutfit.material = prisonOutfit;
        //}

        //if (name == "casualOutfit")
        //{
        //    playerOutfit.material = casualOutfit;
        //}

        //if (name == "constructionWorkerOutfit")
        //{
        //    playerOutfit.material = constructionWorkerOutfit;
        //}

        //if (name == "rebelOutfit")
        //{
        //    playerOutfit.material = rebelOutfit;
        //}

        //if (name == "maintananceWorkerOutfit")
        //{
        //    playerOutfit.material = maintananceWorkerOutfit;
        //}

        if (name == "fancySuitOutfit")
        {
            playerOutfit.material = fancySuitOutfit;
        }

        if (name == "fancyCasualOutfit")
        {
            playerOutfit.material = fancyCasualOutfit;
        }
    }
}
