using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerEnemySpawner : MonoBehaviour
{
    [Serializable]
    public struct Pref
    {
        public GameObject enemyPrefab;
        public Transform enemySpawn;
        public bool isChasingPlayer;
    }

    public Pref[] enemyPrefects;
    public Transform playerLocation;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            playerLocation = other.gameObject.transform;
            SpawnNow();
        }
    }

    public void SpawnNow()
    {
        for(int i = 0; i < enemyPrefects.Length; i++)
        {
            GameObject ejs = Instantiate(enemyPrefects[i].enemyPrefab, enemyPrefects[i].enemySpawn.position, enemyPrefects[i].enemySpawn.rotation);
            if (enemyPrefects[i].isChasingPlayer)
            {
                ejs.GetComponent<GuardNPC>().finalDestination = playerLocation;
                ejs.GetComponent<GuardNPC>().chasingPlayer = true;
            }
        }
    }
}
