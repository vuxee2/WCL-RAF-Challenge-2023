using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;

    public GameObject mainMenuCanvas;
    public GameObject OptionsGO;
    
    public GameObject escCanvasGO;

    public Slider MainVolumeSlider;
    public Slider MusicVolumeSlider;
    public Slider SFXVolumeSlider;
    public Slider SensSlider;

    void Start()
    {
        UpdateSliders();

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i=0;i<resolutions.Length;i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height + " @ " + resolutions[i].refreshRate + "hz";
            options.Add(option);
            
            if(resolutions[i].width == Screen.currentResolution.width &&
               resolutions[i].height == Screen.currentResolution.height)
                {currentResolutionIndex = i;}
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("music",volume);
        audioMixer.SetFloat("sfx",volume);

        PlayerPrefs.SetFloat("mainVolume",volume);
        PlayerPrefs.SetFloat("musicVolume",volume);
        PlayerPrefs.SetFloat("sfxVolume",volume);

        MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
    }
    public void SetMusicVolume (float volume)
    {
        audioMixer.SetFloat("music",volume);

        PlayerPrefs.SetFloat("musicVolume",volume);
    }
    public void SetSFXVolume (float volume)
    {
        audioMixer.SetFloat("sfx",volume);

        PlayerPrefs.SetFloat("sfxVolume",volume);
    }
    public void SetSensitivity (float sens)
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");
        PlayerPrefs.SetFloat("sens", sens);
        playerCam.updateSensitivity = true;   
    }

    public void SetQuality (int qualityIndex)
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        Screen.fullScreen =  isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void BackButton()
    {
        FindObjectOfType<audioManager>().Play("ButtonClick");
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            OptionsGO.SetActive(false);
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            escCanvasGO.SetActive(false);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
            playerCam.isEscCanvasAcive = false;
        }
    }

    public void UpdateSliders()
    {
        MainVolumeSlider.value = PlayerPrefs.GetFloat("mainVolume");
        MusicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXVolumeSlider.value =PlayerPrefs.GetFloat("sfxVolume");
        SensSlider.value = PlayerPrefs.GetFloat("sens");
    }
}
