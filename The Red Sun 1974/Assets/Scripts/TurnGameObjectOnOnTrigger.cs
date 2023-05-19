using UnityEngine;

public class TurnGameObjectOnOnTrigger : MonoBehaviour
{
    public GameObject[] objectToTurnOn;

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < objectToTurnOn.Length; i++)
        {
            objectToTurnOn[i].SetActive(true);
        }
    }
}
