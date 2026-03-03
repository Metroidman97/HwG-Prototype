using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool inSettings = false;
    public static bool inConfirmation = false;
    public  AudioLowPassFilter lowPassFilter;
    public AudioHighPassFilter highPassFilter;

    [Header("UI Elements to connect")]
    public GameObject PauseMenu;
    public Button SettingsMenuReturn;
    public Button ConfirmationMenuNo;

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
    }

    // Update is called once per frame
    void Update()
    {
        // Pause/Unpause unless in the settings menu or a confirmation window is up, in which case go back to the pause menu
        if(Input.GetKeyDown(KeyCode.Escape))
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

    //Toggles bloom/film grain in GlobalVolume
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
        ;
        _colorAdjustments.postExposure.value = value;
    }

    //Adjusts the camera's FOV within the valid slider values
    public void setFOV(float fov)
    {
        mainCamera.fieldOfView = fov;
    }
}
