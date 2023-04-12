using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformForPlayer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(null);
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.transform.localScale = Vector3.one;
            other.transform.localRotation = new Quaternion(0, other.transform.localRotation.y, 0, 0);
        }
    }
}
