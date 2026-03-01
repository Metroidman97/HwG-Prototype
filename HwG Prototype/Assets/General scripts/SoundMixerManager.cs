using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void SetMasterVolume(float level)
    {
        audioMixer.SetFloat("MasterVolume", CorrectLevel(level));
    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SFXVolume", CorrectLevel(level));
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", CorrectLevel(level));
    }

    //Makes the volume sliders work linearly
    float CorrectLevel(float level)
    {
        return Mathf.Log10(level) * 20f;
    }
}
