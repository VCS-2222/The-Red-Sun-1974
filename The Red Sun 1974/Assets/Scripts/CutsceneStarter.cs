using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStarter : MonoBehaviour
{
    [Header("Essential Components")]
    public PlayableDirector cutscene;

    public void OnTriggerEnter(Collider other)
    {
        cutscene.Play();
    }
}