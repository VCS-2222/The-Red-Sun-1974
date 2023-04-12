using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FreeRoamCam : MonoBehaviour
{
    public float xRotation;
    public float speed;
    public float mouseMulti;
    public Transform parent;
    public PlayerBindings cameraControl;

    float mouseX;
    float mouseY;

    private void Awake()
    {
        cameraControl = new PlayerBindings();
        cameraControl.FreeRoamCamera.LiftUp.performed += t => Lift();
        cameraControl.FreeRoamCamera.PushDown.performed += t => Push();
        cameraControl.FreeRoamCamera.MouseMovement.performed += t => CameraMouse(t.ReadValue<Vector2>());
        cameraControl.FreeRoamCamera.SpacialMovement.performed += t => MoveCamera(t.ReadValue<Vector2>());
    }

    private void OnEnable()
    {
        cameraControl.Enable();
    }

    private void OnDisable()
    {
        cameraControl.Disable();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        parent.Rotate(Vector3.up * mouseX);
    }

    private void MoveCamera(Vector2 axis)
    {
        float forwardBackward = axis.y;
        float leftRight = axis.x;

        parent.Translate(Vector3.forward * forwardBackward * speed);
        parent.Translate(Vector3.right * leftRight * speed);
    }

    void Push()
    {
        parent.Translate(-Vector3.up * speed);
    }

    void Lift()
    {
        parent.Translate(Vector3.up * speed);
    }

    void CameraMouse(Vector2 axis)
    {
        mouseX = axis.x * mouseMulti * Time.timeScale;
        mouseY = axis.y * mouseMulti * Time.timeScale;

        xRotation -= mouseY;
    }
}