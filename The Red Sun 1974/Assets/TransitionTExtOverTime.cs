using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TransitionTExtOverTime : MonoBehaviour
{
    [Tooltip("There can only be 15 chat boxes in here, that's the limit")]
    public string[] thingsToSay;
    public int currentTextBoxValue;
    public TextMeshProUGUI contextBox;
    public float timeToWait;
    public AudioSource writingAction;

    [SerializeField] Button firstButtonToSelect;
    private PlayerBindings binds;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        binds = new PlayerBindings();
    }

    private void OnEnable()
    {
        binds.Enable();
    }

    private void OnDisable()
    {
        binds.Disable();
    }

    private void Start()
    {
        StartCoroutine(roll());
        firstButtonToSelect.Select();
    }

    IEnumerator roll()
    {
        contextBox.text += thingsToSay[currentTextBoxValue];
        writingAction.Play();
        currentTextBoxValue++;

        yield return new WaitForSeconds(timeToWait);

        if(currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
            writingAction.Play();
        {
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }

        yield return new WaitForSeconds(timeToWait);

        if (currentTextBoxValue >= thingsToSay.Length)
        {
            yield return null;
        }
        else
        {
            writingAction.Play();
            contextBox.text += thingsToSay[currentTextBoxValue];
            currentTextBoxValue++;
        }
    }

    public void LoadNextScene(int sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
