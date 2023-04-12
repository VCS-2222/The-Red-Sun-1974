using UnityEngine;

public class MegaphoneVoicelineTrigger : MonoBehaviour
{
    public int voicelineToBePlayed;
    public MegaphonePlayerLogic[] megaphones;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            foreach(var mg in megaphones)
            {
                mg.PlayLineFromArray(voicelineToBePlayed);
            }
            Destroy(gameObject);
        }
    }
}
