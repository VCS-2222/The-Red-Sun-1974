using UnityEngine;

public class TrainBrakePoint : MonoBehaviour
{
    [Header("Track Brain")]
    public TrainTrackSystem trackSystem;

    private void OnTriggerEnter(Collider other)
    {
        print("Stopping Train");
        trackSystem.trainIsStopping = true;
    }
}
