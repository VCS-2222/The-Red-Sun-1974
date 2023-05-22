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
    [SerializeField] bool hasSaveFile;
    [SerializeField] bool hasFileOfPresetSettings;
    [SerializeField] public bool isUsingController;
    [SerializeField] bool isInFullScreen;

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

    [Header("Controller")]
    [SerializeField] TMP_Dropdown checkControllerDropdown;

    [Header("Graphics and Resolution")]
    [SerializeField] TMP_Dropdown fullScreenDropdown;
    [SerializeField] TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    private void Awake()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> res = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string resolution = resolutions[i].width + " by " + resolutions[i].height;
            res.Add(resolution);
        }

        resolutionDropdown.AddOptions(res);

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
        IfHasController();
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

        if (PlayerPrefs.HasKey("Full_Screen"))
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        if (PlayerPrefs.HasKey("hasPresetSettings"))
        {
            hasFileOfPresetSettings = true;
            LoadOptions();
        }
        else
        {
            hasFileOfPresetSettings = false;
        }
    }

    public void IfHasController()
    {
        if(PlayerPrefs.HasKey("ControllerConnected"))
        {
            isUsingController = true;
        }
        else
        {
            if (checkControllerDropdown.value == 0)
            {
                PlayerPrefs.DeleteKey("ControllerConnected");
                isUsingController = false;
            }
            else if (checkControllerDropdown.value == 1)
            {
                PlayerPrefs.SetString("ControllerConnected", "Yes");
                isUsingController = true;
            }
        }
    }

    public void IfInFullScreen()
    {
        if (fullScreenDropdown.value == 0)
        {
            PlayerPrefs.SetString("Full_Screen", "Yes");
        }
        else if (fullScreenDropdown.value == 1)
        {
            PlayerPrefs.DeleteKey("Full_Screen");
        }
    }

    public void SetResolutionFromDropdown()
    {
        PlayerPrefs.SetInt("Screen_Resolution", resolutionDropdown.value);
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Full_Screen"))
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.fullScreen = false;
        }

        if (PlayerPrefs.HasKey("TRS_1974_SaveFile"))
        {
            hasSaveFile = true;
        }
        else
        {
            hasSaveFile = false;
        }

        if (isUsingController)
        {
            PlayerPrefs.SetString("controllerConnected", "Yes");
        }
        else
        {
            PlayerPrefs.DeleteKey("controllerConnected");
        }

        SetMixerVoiceThoughtSlider(voicelinesSlider, voicelines);
        SetMixerFootThoughtSlider(footstepsSlider, footsteps);
        SetMixerVehicleThoughtSlider(vehiclesSlider, vehicles);
        SetMixerMusicThoughtSlider(musicSlider, music);
        SetMixerSoundThoughtSlider(soundEffectsSlider, sfx);
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
            PlayerPrefs.SetInt("TRS_1974_SaveFile", 2);
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

    public void SetMixerVoiceThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("vc_Audio", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
    }

    public void SetMixerFootThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("ft_Audio", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
    }

    public void SetMixerVehicleThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("vh_Audio", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
    }

    public void SetMixerMusicThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("mc_Audio", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
    }

    public void SetMixerSoundThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("sfx_Audio", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
    }

    public void LoadOptions()
    {
        voicelines.SetFloat("volume", PlayerPrefs.GetFloat("vc_Audio"));
        voicelinesSlider.value = PlayerPrefs.GetFloat("vc_Audio");

        footsteps.SetFloat("volume", PlayerPrefs.GetFloat("ft_Audio"));
        footstepsSlider.value = PlayerPrefs.GetFloat("ft_Audio");

        vehicles.SetFloat("volume", PlayerPrefs.GetFloat("vh_Audio"));
        vehiclesSlider.value = PlayerPrefs.GetFloat("vh_Audio");

        music.SetFloat("volume", PlayerPrefs.GetFloat("mc_Audio"));
        musicSlider.value = PlayerPrefs.GetFloat("mc_Audio");

        sfx.SetFloat("volume", PlayerPrefs.GetFloat("sfx_Audio"));
        soundEffectsSlider.value = PlayerPrefs.GetFloat("sfx_Audio");

        if (PlayerPrefs.HasKey("Screen_Resolution"))
        {
            resolutionDropdown.value = PlayerPrefs.GetInt("Screen_Resolution");
        }
    }

    public void QuitGame()
    {
        loadPoster.SetActive(true);
        Application.Quit();
    }
}
