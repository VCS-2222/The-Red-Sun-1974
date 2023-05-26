using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public static PlayerCamera instance;

    [Header("Components")]
    [SerializeField] private GameObject playerBody;
    [SerializeField] private Camera actualPlayerCamera;
    [SerializeField] public PlayerBindings cameraControl;

    [Header("Settings and Debug")]
    [SerializeField] public float mouseSensitivity;
    [SerializeField] private float leftAndRightCameraAxis;
    [SerializeField] private float upAndDownCameraAxis;
    public bool ifHasControllerConnected;
    float clamper;

    [Header("Bools")]
    public bool canRotate;

    private void Awake()
    {
        cameraControl = new PlayerBindings();
        cameraControl.Player.MouseMovement.performed += t => MouseMover(t.ReadValue<Vector2>());

        if (PlayerPrefs.HasKey("cameraSensitivity"))
        {
            mouseSensitivity = PlayerPrefs.GetFloat("cameraSensitivity");
        }
    }

    private void OnEnable()
    {
        cameraControl.Enable();
    }

    private void OnDisable()
    {
        cameraControl.Disable();
    }

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
        if (PlayerPrefs.HasKey("controllerConnected"))
        {
            ifHasControllerConnected = true;
        }
        else
        {
            ifHasControllerConnected = false;
        }

        UseMouseCursor();
    }

    private void MouseMover(Vector2 axis)
    {
        mouseSensitivity = PlayerPrefs.GetFloat("cameraSensitivity");

        if (ifHasControllerConnected)
        {
            upAndDownCameraAxis = (axis.y * Time.deltaTime * mouseSensitivity) * 6;
            leftAndRightCameraAxis = (axis.x * Time.deltaTime * mouseSensitivity) * 6;
        }
        else
        {
            upAndDownCameraAxis = axis.y * Time.deltaTime * mouseSensitivity;
            leftAndRightCameraAxis = axis.x * Time.deltaTime * mouseSensitivity;
        }

        clamper -= upAndDownCameraAxis;
        clamper = Mathf.Clamp(clamper, -70f, 70f);

        transform.localRotation = Quaternion.Euler(clamper, 0, 0);

        if (!canRotate) return;

        playerBody.transform.Rotate(Vector3.up, leftAndRightCameraAxis);
    }
}
