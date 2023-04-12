using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaphonePlayerLogic : MonoBehaviour
{
    [Header("Audio Variables")]
    [SerializeField] AudioClip[] voicelines;
    [SerializeField] AudioSource megaphoneSource;

    public void PlayLineFromArray(int num)
    {
        if (megaphoneSource.isPlaying)
            return;

        megaphoneSource.PlayOneShot(voicelines[num]);
    }
}
