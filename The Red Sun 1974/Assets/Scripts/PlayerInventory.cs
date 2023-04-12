using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInventory : MonoBehaviour
{
    [Header("Needed Components")]
    [SerializeField] private GameObject itemHolder;
    [SerializeField] PlayerAnimatorController animatorController;
    public PlayerBindings binds;

    [Header("Debug")]
    public bool hasItem;
    [SerializeField] private GameObject currentItemHeld;

    private void Awake()
    {
        binds = new PlayerBindings();
        binds.Player.ItemInteraction.performed += t => CheckIfHasItem();
    }

    private void OnEnable()
    {
        binds.Enable();
    }

    private void OnDisable()
    {
        binds.Disable();
    }

    public void CheckIfHasItem()
    {
        if (hasItem)
        {
            DropItem();
        }
    }

    public void CheckForItem(RaycastHit hit)
    {
        if(itemHolder.transform.childCount == 0)
        {
            hasItem = false;
        }
        else
        {
            hasItem = true;
        }

        if (hasItem)
        {
            DropItem();
            PickUpItem(hit);
        }
        else if (!hasItem)
        {
            PickUpItem(hit);
        }
    }

    public void PickUpItem(RaycastHit hit)
    {
        currentItemHeld = hit.transform.gameObject;
        currentItemHeld.transform.parent = itemHolder.transform;
        currentItemHeld.GetComponent<Rigidbody>().isKinematic = true;
        currentItemHeld.GetComponent<Rigidbody>().useGravity = false;
        currentItemHeld.GetComponent<Collider>().isTrigger = true;
        currentItemHeld.transform.localScale = Vector3.one;
        currentItemHeld.transform.localPosition = Vector3.zero;
        currentItemHeld.transform.localRotation = Quaternion.identity;
        animatorController.UseAnimatorArmLayer();
        hasItem = true;
    }

    public void DropItem()
    {
        currentItemHeld.transform.parent = null;
        currentItemHeld.GetComponent<Rigidbody>().isKinematic = false;
        currentItemHeld.GetComponent<Rigidbody>().useGravity = true;
        currentItemHeld.GetComponent<Collider>().isTrigger = false;
        currentItemHeld = null;
        animatorController.UnuseAnimatorArmLayer();
        hasItem = false;
    }
}
