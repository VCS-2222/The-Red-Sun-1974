using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuLogic : MonoBehaviour
{
    [Header("Time")]
    [SerializeField] float timeWhenPaused;
    [SerializeField] float normalTime;

    [Header("Components")]
    [SerializeField] GameObject pauseMenuPanel;
    public PlayerBindings binds;
    [SerializeField] GameObject loadingScreen;
    [SerializeField] PlayerCamera playerCamera;
    public Button firstSelectedButtonOnOpen;

    private void Awake()
    {
        binds = new PlayerBindings();
        binds.UI.PullUpPauseMenu.performed += t => PullUpPausePanel(true);
    }

    private void OnEnable()
    {
        binds.Enable();
    }

    private void OnDisable()
    {
        binds.Disable();
    }

    public void PullUpPausePanel(bool pull)
    {
        if (pull)
        {
            PrimaryButtonOnSelection(firstSelectedButtonOnOpen);
            Time.timeScale = timeWhenPaused;
            pauseMenuPanel.SetActive(true);
        }
        else
        {
            playerCamera.UseMouseCursor();
            Time.timeScale = normalTime;
            pauseMenuPanel.SetActive(false);
        }
    }

    public void PrimaryButtonOnSelection(Button toBeSelectedButton)
    {
        toBeSelectedButton.Select();
    }

    public void GoBackToMainMenu()
    {
        StartCoroutine(InitializeLevelLeave());
    }

    IEnumerator InitializeLevelLeave()
    {
        loadingScreen.SetActive(true);
        Time.timeScale = normalTime;

        yield return new WaitForSecondsRealtime(1f);

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        StartCoroutine(InitializeLeaveGame());
    }

    IEnumerator InitializeLeaveGame()
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(1f);

        Application.Quit();
    }
}
