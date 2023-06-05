using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Audio;
using UnityEngine.UI;

public class QuickSettingsLogic : MonoBehaviour
{
    [Header("Graphics and Resolution")]
    [SerializeField] TMP_Dropdown fullScreenDropdown;

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

    [Header("Sensitivity")]
    [SerializeField] Slider sensitivitySlider;

    private void Start()
    {
        if (PlayerPrefs.HasKey("hasPresetSettings"))
        {
            voicelinesSlider.value = PlayerPrefs.GetFloat("vc_Audio");
            footstepsSlider.value = PlayerPrefs.GetFloat("ft_Audio");
            vehiclesSlider.value = PlayerPrefs.GetFloat("vh_Audio");
            megaphoneSlider.value = PlayerPrefs.GetFloat("mf_Audio");
            musicSlider.value = PlayerPrefs.GetFloat("mc_Audio");
            soundEffectsSlider.value = PlayerPrefs.GetFloat("sfx_Audio");
        }

        if (PlayerPrefs.HasKey("cameraSensitivity"))
        {
            sensitivitySlider.value = PlayerPrefs.GetFloat("cameraSensitivity");
        }
    }

    public void IfInFullScreen()
    {
        if (fullScreenDropdown.value == 0)
        {
            PlayerPrefs.SetString("Full_Screen", "Yes");
            Screen.fullScreen = true;
        }
        else if (fullScreenDropdown.value == 1)
        {
            PlayerPrefs.DeleteKey("Full_Screen");
            Screen.fullScreen = false;
        }
    }

    private void Update()
    {
        SetMixerVoiceThoughtSlider(voicelinesSlider, voicelines);
        SetMixerFootThoughtSlider(footstepsSlider, footsteps);
        SetMixerVehicleThoughtSlider(vehiclesSlider, vehicles);
        SetMixerMegaphoneThoughtSlider(megaphoneSlider, megaphones);
        SetMixerMusicThoughtSlider(musicSlider, music);
        SetMixerSoundThoughtSlider(soundEffectsSlider, sfx);
    }

    public void SetSensitivity(Slider slide)
    {
        PlayerPrefs.SetFloat("cameraSensitivity", slide.value);
        PlayerPrefs.SetString("hasPresetSettings", "yes");
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

    public void SetMixerMegaphoneThoughtSlider(Slider slide, AudioMixer mixer)
    {
        mixer.SetFloat("volume", slide.value);
        PlayerPrefs.SetFloat("mf_Audio", slide.value);
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
}