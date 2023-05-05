using UnityEngine;

public class UniversalFanSpinner : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
