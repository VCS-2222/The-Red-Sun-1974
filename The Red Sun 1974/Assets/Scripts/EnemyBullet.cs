using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other != null)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<PlayerLoseState>().EnableLoseState("Shot by Guard");
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject, 10f);
            }
        }
    }
}
