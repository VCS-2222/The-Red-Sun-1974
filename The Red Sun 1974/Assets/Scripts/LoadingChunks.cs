using UnityEngine;

public class LoadingChunks : MonoBehaviour
{
    [Header("Components")]
    public GameObject ChunkToUnload;
    public GameObject ChunkToLoad;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ChunkToLoad.SetActive(true);
            ChunkToUnload.SetActive(false);
        }
    }
}
