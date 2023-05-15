using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenuLogic : MonoBehaviour
{
    [Header("Saves")]
    [SerializeField] private bool hasSaveFile;
    [SerializeField] private bool hasFileOfPresetSettings;

    [Header("Load")]
    public GameObject loadPoster;

    [Header("Needed Components")]
    [SerializeField] Button firstButtonToSelect;
    private PlayerBindings binds;

    [Header("Audio")]
    [SerializeField] AudioMixer voicelines;
    [SerializeField] AudioMixer footsteps;
    [SerializeField] AudioMixer vehicles;
    [SerializeField] AudioMixer megaphones;
    [SerializeField] AudioMixer music;
    [SerializeField] AudioMixer sfx;

    [SerializeField] Slider voicelinesSlider;
    [SerializeField] Slider footstepsSlider;
    [SerializeField] Slider vehiclesSlider;
    [SerializeField] Slider megaphoneSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider soundEffectsSlider;

    private void Awake()
    {
        binds = new PlayerBindings();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        firstButtonToSelect.Select();

        loadPoster.SetActive(false);
        if (PlayerPrefs.HasKey("TRS_1974_SaveFile"))
        {
            hasSaveFile = true;
        }
        else
        {
            hasSaveFile = false;
        }
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("TRS_1974_SaveFile"))
        {
            hasSaveFile = true;
        }
        else
        {
            hasSaveFile = false;
        }

        SetMixerThoughtSlider(voicelinesSlider, voicelines);
        SetMixerThoughtSlider(footstepsSlider, footsteps);
        SetMixerThoughtSlider(vehiclesSlider, vehicles);
        SetMixerThoughtSlider(musicSlider, music);
        SetMixerThoughtSlider(soundEffectsSlider, sfx);
    }

    public void StartNewGame()
    {
        if (hasSaveFile)
        {
            PlayerPrefs.DeleteKey("TRS_1974_SaveFile");
            StartCoroutine(InitializeNewLevel());
        }
        else
        {
            PlayerPrefs.SetInt("TRS_1974_SaveFile", 1);
            StartCoroutine(InitializeNewLevel());
        }
    }

    IEnumerator InitializeNewLevel()
    {
        loadPoster.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);
    }

    IEnumerator InitializeLoadLevel(int lvl)
    {
        loadPoster.SetActive(true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(lvl);
    }

    public void LoadGame()
    {
        if(!hasSaveFile)
            return;

        int levelToLoad = PlayerPrefs.GetInt("TRS_1974_SaveFile");
        StartCoroutine(InitializeLoadLevel(levelToLoad));
    }

    public void DeleteSaveFile()
    {
        PlayerPrefs.DeleteKey("TRS_1974_SaveFile");
    }

    public void SetMixerThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
    }

    public void QuitGame()
    {
        loadPoster.SetActive(true);
        Application.Quit();
    }
}
