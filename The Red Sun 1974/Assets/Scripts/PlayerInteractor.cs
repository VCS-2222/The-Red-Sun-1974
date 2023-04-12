using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float reach;

    [Header("Needed Components")]
    [SerializeField] private PlayerInventory playerInventory;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject playerReferance;
    PlayerBindings binds;

    public RaycastHit hit;

    private void Awake()
    {
        binds = new PlayerBindings();
        binds.Player.ItemInteraction.performed += t => RaycastForItems();
        binds.Player.GeneralInteraction.performed += t => RaycastForGeneral();
    }

    private void OnEnable()
    {
        binds.Enable();
    }

    private void OnDisable()
    {
        binds.Disable();
    }

    public void RaycastForItems()
    {
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, reach);

        if (hit.collider == null)
            return;

        CheckForHitInfoItems();
    }

    public void RaycastForGeneral()
    {
        Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, reach);

        if (hit.collider == null)
            return;

        CheckForHitInfo();
    }

    void CheckForHitInfoItems()
    {
        Debug.Log(hit.transform.name);

        if(hit.collider.tag == "Item")
        {
            playerInventory.CheckForItem(hit);
        }
    }
    void CheckForHitInfo()
    {
        Debug.Log(hit.transform.name);

        if (hit.collider.tag == "Pedestrian")
        {
            hit.collider.GetComponentInChildren<PedestrianNPCVoicelineManager>().DoRandomVoiceline();
        }

        if (hit.collider.tag == "Car")
        {
            hit.transform.GetComponent<CarController>().UseCar(playerReferance);
        }
    }
}
