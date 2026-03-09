using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    public void Start()
    {
        UnDistortMusic();
    }

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

    public void DistortMusic()
    {
        audioMixer.SetFloat("MusicPitch", 1.5f);
        audioMixer.SetFloat("MusicDistortion", 0f);
    }

    public void UnDistortMusic()
    {
        audioMixer.SetFloat("MusicPitch", 1f);
        audioMixer.SetFloat("MusicDistortion", -80f);
    }

    //Makes the volume sliders work linearly
    float CorrectLevel(float level)
    {
        return Mathf.Log10(level) * 20f;
    }
}
