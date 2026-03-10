using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inSettings = false;
    public static bool inConfirmation = false;
    public bool canPause = true;
    public AudioLowPassFilter lowPassFilter;
    public AudioHighPassFilter highPassFilter;

    [Header("UI Elements to connect")]
    public GameObject PauseMenu;
    public Button
        SettingsMenuReturn,
        ConfirmationMenuNo,
        PauseMenuResume;
    public Slider
        masterVolume,
        musicVolume,
        sfxVolume,
        fov,
        brightness;
    public Toggle
        bloomToggle,
        filmGrainToggle;

    [Header("PostProcessing to connect")]
    public Volume postProcessingVolume;
    private Bloom _bloom;
    private FilmGrain _filmGrain;
    private ColorAdjustments _colorAdjustments;

    [Header("Camera to connect")]
    public Camera mainCamera;

    

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        // get overrides from the GobalVolume to use in settings
        postProcessingVolume.profile.TryGet(out _bloom);
        postProcessingVolume.profile.TryGet(out _filmGrain);
        postProcessingVolume.profile.TryGet(out _colorAdjustments);
        // Load settings for the game
        LoadSettings();
    }

    // Update is called once per frame
    void Update()
    {
        // Pause/Unpause unless in the settings menu or a confirmation window is up, in which case go back to the pause menu
        if (canPause && Input.GetKeyDown(KeyCode.Escape) || canPause && Input.GetKeyDown(KeyCode.Joystick1Button7 ))
        {
            if (inConfirmation)
            {
                ConfirmationMenuNo.onClick.Invoke();
            }
            else if (inSettings)
            {
                SettingsMenuReturn.onClick.Invoke();
            }
            else if (isPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    //Activates the pause menu, filters, and mouse cursor. Stops time from progressing
    void Pause()
    {
        Debug.Log("pause");
        PauseMenu.SetActive(true);
        lowPassFilter.enabled = true;
        highPassFilter.enabled = true;
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SelectInMenu(PauseMenuResume);
    }

    //deactivates pause menu, filters, and relocks mouse cursor. Time resumes
    public void Unpause()
    {
        Debug.Log("unpause");
        PauseMenu.SetActive(false);
        lowPassFilter.enabled = false;
        highPassFilter.enabled = false;
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Allows buttons and other scripts to change the inSettings/inConfirmation variables
    public void Settings(bool enable)
    {
        inSettings = enable;
    }

    public void Confirmation(bool enable)
    {
        inConfirmation = enable;
    }

    //sets bloom/film grain/postExposure in GlobalVolume
    public void setBloom(bool enable)
    {
        _bloom.active = enable;
    }

    public void setFilmGrain(bool enable)
    {
        _filmGrain.active = enable;
    }

    public void setPostExposure(float value)
    {
        _colorAdjustments.postExposure.value = value;
    }

    //Adjusts the camera's FOV within the valid slider values
    public void setFOV(float fov)
    {
        if (mainCamera != null)
        {
            mainCamera.fieldOfView = fov;
        }
    }

    //Saves settings with PlayerPrefs, which saves each of these values with a specific key String. 
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("SavedSettings", 1);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
        PlayerPrefs.SetFloat("Fov", fov.value);
        PlayerPrefs.SetFloat("brightness", brightness.value);
        PlayerPrefs.SetInt("bloom", bloomToggle.isOn ? 1 : 0);
        PlayerPrefs.SetInt("filmGrain", filmGrainToggle.isOn ? 1 : 0);
    }

    //Loads settings by invoking various UI elements in the settings menu.
    public void LoadSettings()
    {
        // If settings haven't been saved before, save the default settings.
        if (PlayerPrefs.HasKey("SavedSettings"))
        {
            masterVolume.value = PlayerPrefs.GetFloat("MasterVolume");
            musicVolume.value = PlayerPrefs.GetFloat("MusicVolume");
            sfxVolume.value = PlayerPrefs.GetFloat("SFXVolume");
            fov.value = PlayerPrefs.GetFloat("Fov");
            brightness.value = PlayerPrefs.GetFloat("brightness");
            bloomToggle.isOn = PlayerPrefs.GetInt("bloom") == 1;
            filmGrainToggle.isOn = PlayerPrefs.GetInt("filmGrain") == 1;
        }
        else
        {
            SaveSettings();
        }
    }

    //Deletes ALL saved settings and resets them to their default values. IRREVERSIBLE USE WITH CAUTION
    public void ResetSettings()
    {
        PlayerPrefs.DeleteAll();
        masterVolume.value = 1f;
        musicVolume.value = 1f;
        sfxVolume.value = 1f;
        fov.value = 84f;
        brightness.value = 0.64f;
        bloomToggle.isOn = true;
        filmGrainToggle.isOn = true;
    }

    //Toggles whether or not the player can pause the game (used to prevent breaking stuff durring gameplay)
    public void TogglePause(bool toggle)
    {
        canPause = toggle;
    }

    //Selects the button passed into the method. Nothing happens if the button is not active or not set.
    public void SelectInMenu(Button button)
    {
        if (button != null && button.IsActive())
        {
            button.Select();
        }
    }
}
