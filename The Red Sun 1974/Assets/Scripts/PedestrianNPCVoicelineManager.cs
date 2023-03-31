using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianNPCVoicelineManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] AudioSource NPCVoiceSource;

    [Header("Preset Voicelines")]
    [SerializeField] private AudioClip[] presetLines;

    [Header("Random Interaction Lines")]
    [SerializeField] private AudioClip[] interactionLines;

    public void DoRandomVoiceline()
    {
        if (NPCVoiceSource.isPlaying)
            return;

        int r = Random.Range(0, interactionLines.Length);

        AudioClip v = interactionLines[r];

        NPCVoiceSource.PlayOneShot(v);
    }
}
