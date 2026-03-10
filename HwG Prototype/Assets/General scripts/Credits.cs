using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public SceneLoader Scenetransition;
    public SoundMixerManager SoundMixerManager;

    private void Start()
    {
        SoundMixerManager.DistortMusic();
        SoundMixerManager.SetMusicVolume(0.2f);
    }
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Scenetransition.LoadMainMenu();
        }
    }
}
