using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoulderLogic : MonoBehaviour
{
    [Header("Variables")]
    public PlayerBindings playerControls;
    public bool isUsing;
    public Light torchLight;
    public AudioSource torchSound;

    private void Awake()
    {
        isUsing = false;
        playerControls = new PlayerBindings();
        playerControls.Player.ShoulderTorch.performed += t => UseTorch();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public void UseTorch()
    {
        isUsing = !isUsing;
        torchSound.Play();

        if (isUsing)
        {
            torchLight.enabled = true;
        }
        else
        {
            torchLight.enabled = false;
        }
    }
}
