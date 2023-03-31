using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianSkinRandomizer : MonoBehaviour
{
    [Header("Skin and Skins")]
    [SerializeField] SkinnedMeshRenderer npcSkin;
    [SerializeField] Material[] outfits;

    private void Start()
    {
        int o = Random.Range(0, outfits.Length);

        Material ns = outfits[o];

        npcSkin.material = ns;
    }
}
