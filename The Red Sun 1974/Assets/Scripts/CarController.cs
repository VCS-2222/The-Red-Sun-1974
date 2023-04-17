using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Wheel Colliders and Meshes")]
    [SerializeField] private WheelCollider[] frontWheelColliders;
    [SerializeField] private WheelCollider[] rearWheelColliders;
    [SerializeField] private Transform[] wheelRenderers;

    [Header("Engine Settings")]
    [SerializeField] private float torqueSpeed;
    [SerializeField] private float brakeForce;
    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float turnSpeed;
    [SerializeField] private int motorPower;
    [SerializeField] private float maxKpH;

    [Header("Lights")]
    [SerializeField] GameObject[] headLights;
    [SerializeField] GameObject[] brakeLights;

    [Header("Audio")]
    [SerializeField] float pitch;
    [SerializeField] float volume;
    [SerializeField] AudioSource engineAudioSource;
    [SerializeField] AudioSource brakeAudioSkidSource;

    [Header("Components and Debug")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] bool isUsing;
    [SerializeField] private Transform seat;
    [SerializeField] private Transform exitPoint;
    GameObject playerReferance;
    public PlayerBindings carControls;
    [SerializeField] bool headlightsUsed;

    float verticalInput;
    float horizontalInput;

    private void Awake()
    {
        carControls = new PlayerBindings();
    }

    private void OnEnable()
    {
        carControls.Enable();
    }

    private void OnDisable()
    {
        carControls.Disable();
    }

    private void Update()
    {
        HandleAudio();

        if (!isUsing)
        {
            ApplyBrakes();
        }
        
        if(isUsing)
        {
            carControls.Car.CarMovement.performed += t => HandleAxis(t.ReadValue<Vector2>());

            carControls.Car.Headlights.performed += t => UseHeadlights();

            if (carControls.Car.Brakes.IsPressed())
            {
                ApplyBrakes();
                UseBrakeLights(true);
            }
            else
            {
                UseBrakeLights(false);
                foreach (WheelCollider rer in rearWheelColliders)
                {
                    rer.brakeTorque = 0;
                }
            }
        }

        if(rb.velocity.magnitude > maxKpH)
        {
            rb.velocity = rb.velocity.normalized * maxKpH;
        }
    }

    private void FixedUpdate()
    {
        if (!isUsing) return;
        FrontWheelControl();
        RearWheelControl();
    }

    private void LateUpdate()
    {
        //frontwheels
        WheelSync(frontWheelColliders[0], wheelRenderers[0]);
        WheelSync(frontWheelColliders[1], wheelRenderers[1]);

        //rearwheels
        WheelSync(rearWheelColliders[0], wheelRenderers[2]);
        WheelSync(rearWheelColliders[1], wheelRenderers[3]);
    }

    void HandleAudio()
    {
        float speedVar = rb.velocity.magnitude;

        if (!isUsing)
        {
            engineAudioSource.volume = 0f;
        }
        else
        {
            if (speedVar > 1f)
            {
                engineAudioSource.volume = volume;
                engineAudioSource.pitch = pitch;
            }
            else
            {
                engineAudioSource.volume = 0.1f;
                engineAudioSource.pitch = 0.5f;
            }
        }
    }

    #region wheelLogic
    void ApplyBrakes()
    {
        verticalInput = 0;
        foreach (WheelCollider rer in rearWheelColliders)
        {
            rer.brakeTorque = brakeForce * motorPower;
            rer.motorTorque = 0;
        }
    }

    void FrontWheelControl()
    {
        foreach (WheelCollider fnt in frontWheelColliders)
        {
            var steerMaxAngle = horizontalInput * turnSpeed * maxSteerAngle;
            fnt.steerAngle = Mathf.Lerp(fnt.steerAngle, steerMaxAngle, turnSpeed);
        }
    }

    void RearWheelControl()
    {
        foreach (WheelCollider rer in rearWheelColliders)
        {
            rer.motorTorque = (torqueSpeed * motorPower) * verticalInput;
        }
    }

    void WheelSync(WheelCollider col, Transform mesh)
    {
        Quaternion rots;
        Vector3 vecs;
        col.GetWorldPose(out vecs, out rots);
        mesh.position = vecs;
        mesh.rotation = rots;
    }
    #endregion

    #region playerMechanics
    void HandleAxis(Vector2 axis)
    {
        verticalInput = axis.y;
        horizontalInput = axis.x;
    }

    void UseBrakeLights(bool isOn)
    {
        if (isOn)
        {
            foreach (GameObject bl in brakeLights)
            {
                bl.gameObject.SetActive(true);
            }

            if(rb.velocity.magnitude > 1f)
            {
                if (brakeAudioSkidSource.isPlaying)
                {
                    return;
                }
                else
                {
                    brakeAudioSkidSource.Play();
                }
            }
        }
        else
        {
            brakeAudioSkidSource.Stop();
            foreach (GameObject bl in brakeLights)
            {
                bl.gameObject.SetActive(false);
            }
        }
    }

    void UseHeadlights()
    {
        if (!isUsing) return;

        headlightsUsed = !headlightsUsed;

        if (headlightsUsed)
        {
            foreach (GameObject hl in headLights)
            {
                hl.gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject hl in headLights)
            {
                hl.gameObject.SetActive(false);
            }
        }
    }

    public void UseCar(GameObject player)
    {
        if(playerReferance != null)
        {
            UnuseCar();
        }
        else
        {
            verticalInput = 0;
            playerReferance = player;
            isUsing = true;
            player.transform.SetParent(seat.transform);
            if (player.GetComponentInChildren<PlayerInventory>().hasItem)
            {
                player.GetComponentInChildren<PlayerInventory>().DropItem();
            }
            player.transform.position = seat.transform.position;
            player.transform.rotation = seat.transform.rotation;
            player.GetComponentInChildren<PlayerAnimatorController>().SittingMechanicOn();
        }
    }

    public void UnuseCar()
    {
        if (playerReferance != null)
        {
            verticalInput = 0;
            isUsing = false;
            playerReferance.transform.position = exitPoint.position;
            playerReferance.GetComponent<CharacterController>().enabled = true;
            playerReferance.GetComponent<PlayerMovement>().canMove = true;
            playerReferance.transform.parent = null;
            playerReferance.transform.rotation = new Quaternion(0, 0, 0, 0);
            playerReferance.transform.localScale = Vector3.one;
            playerReferance.GetComponentInChildren<PlayerAnimatorController>().SittingMechanicOff();
            playerReferance = null;
        }
    }
    #endregion
}
