using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerPedestrianSpawner : MonoBehaviour
{
    [Serializable]
    public struct Pref
    {
        public GameObject npcPrefab;
        public Transform npcSpawn;
        public bool isChasingPlayer;
    }

    public Pref[] pedestrianPrefects;
    public Transform[] finalLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SpawnNow();
        }
    }

    public void SpawnNow()
    {
        for (int i = 0; i < pedestrianPrefects.Length; i++)
        {
            GameObject ejs = Instantiate(pedestrianPrefects[i].npcPrefab, pedestrianPrefects[i].npcSpawn.position, pedestrianPrefects[i].npcSpawn.rotation);
            if (pedestrianPrefects[i].isChasingPlayer)
            {
                for(int l = 0; l < finalLocation.Length; l++)
                {
                    ejs.GetComponent<PedestrianNPC>().finalDestination = finalLocation[l];
                    ejs.GetComponent<PedestrianNPC>().TravelToFinalDestination();
                }
            }
        }
    }
}
