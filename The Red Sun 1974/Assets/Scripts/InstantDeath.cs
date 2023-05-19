using UnityEngine;

public class InstantDeath : MonoBehaviour
{
    [SerializeField] string deathMessage;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerLoseState>().EnableLoseState(deathMessage);
        }
    }
}
