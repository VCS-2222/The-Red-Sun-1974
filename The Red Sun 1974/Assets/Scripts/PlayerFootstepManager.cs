using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepManager : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] PlayerMovement playerMovement;
    public Camera playerCamera;

    [Header("Audio")]
    [SerializeField] AudioClip[] concrete;
    [SerializeField] AudioClip[] tile;
    [SerializeField] AudioClip[] brick;
    [SerializeField] AudioClip[] metal;
    [SerializeField] AudioClip[] wood;
    [SerializeField] AudioClip[] carpet;

    [SerializeField] private AudioSource feetAudioSource;

    public void UseFeet()
    {
        if (playerMovement.movementDirection.magnitude > 0.2f)
        {
            if (Physics.Raycast(playerCamera.transform.position, Vector3.down, out RaycastHit hit, 2f))
            {
                switch (hit.collider.tag)
                {
                    case "Concrete":
                        feetAudioSource.PlayOneShot(concrete[Random.Range(0, concrete.Length - 1)]);
                        break;
                    case "Tile":
                        feetAudioSource.PlayOneShot(tile[Random.Range(0, tile.Length - 1)]);
                        break;
                    case "Brick":
                        feetAudioSource.PlayOneShot(brick[Random.Range(0, brick.Length - 1)]);
                        break;
                    case "Metal":
                        feetAudioSource.PlayOneShot(metal[Random.Range(0, metal.Length - 1)]);
                        break;
                    case "Wood":
                        feetAudioSource.PlayOneShot(wood[Random.Range(0, wood.Length - 1)]);
                        break;
                    case "Carpet":
                        feetAudioSource.PlayOneShot(carpet[Random.Range(0, carpet.Length - 1)]);
                        break;
                    default:
                        feetAudioSource.PlayOneShot(concrete[Random.Range(0, concrete.Length - 1)]);
                        break;
                }
            }
        }
    }
}
