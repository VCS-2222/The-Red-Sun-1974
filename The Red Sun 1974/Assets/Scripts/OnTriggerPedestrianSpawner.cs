using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

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
                ejs.GetComponent<PedestrianNPC>().finalDestination = finalLocation[(Random.Range(0, finalLocation.Length))];
                ejs.GetComponent<PedestrianNPC>().TravelToFinalDestination();
            }
        }
    }
}
