using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerBody;
    [SerializeField] private Camera actualPlayerCamera;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private float leftAndRightCameraAxis;
    [SerializeField] private float upAndDownCameraAxis;
    float clamper;

    public void UseMouseCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void DontUseMouseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void Start()
    {
        UseMouseCursor();
    }

    private void LateUpdate()
    {
        upAndDownCameraAxis = Input.GetAxis("Mouse Y") * mouseSensitivity;
        leftAndRightCameraAxis = Input.GetAxis("Mouse X") * mouseSensitivity;

        clamper -= upAndDownCameraAxis;
        clamper = Mathf.Clamp(clamper, -80f, 80f);

        transform.localRotation = Quaternion.Euler(clamper, 0, 0);

        playerBody.transform.Rotate(Vector3.up, leftAndRightCameraAxis);
    }
}
