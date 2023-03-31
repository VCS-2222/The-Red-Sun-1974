using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera playerCamera;

    [Header("Settings")]
    [SerializeField] private float reach;

    [Header("Needed Components")]
    [SerializeField] private PlayerInventory playerInventory;

    public RaycastHit hit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, reach);

            if (hit.collider == null)
                return;

            CheckForHitInfo();
        }
    }

    void CheckForHitInfo()
    {
        if(hit.collider.tag == "Item")
        {
            playerInventory.CheckForItem(hit);
        }
        else
        {

        }
    }
}
