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

    [Header("Camera to connect")]
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        postProcessingVolume.profile.TryGet(out _bloom);
        postProcessingVolume.profile.TryGet(out _filmGrain);
    }

    // Update is called once per frame
    void Update()
    {
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

    //Allows buttons and other scripts to change the inSettings variable
    public void Settings(bool enable)
    {
        inSettings = enable;
    }

    public void Confirmation(bool enable)
    {
        inConfirmation = enable;
    }

    public void setBloom(bool enable)
    {
        _bloom.active = enable;
    }

    public void setFilmGrain(bool enable)
    {
        _filmGrain.active = enable;
    }

    public void setFOV(float fov)
    {
        mainCamera.fieldOfView = fov;
    }
}
