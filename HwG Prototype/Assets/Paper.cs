using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paper : MonoBehaviour
{
    public GameObject canvas;
    public GameObject player;
    public GameObject cameraController;
    public bool isLooking;
    public AudioSource paperSound;
    public AudioClip paperSoundClip;
    public AudioClip paperdown;
    public PauseManager pause;
    // Start is called before the first frame update

    public void lookAtPage()
    {
        canvas.SetActive(true);
        player.SetActive(false);
        cameraController.GetComponent<CameraControl>().enabled = false;
        isLooking = true;
        paperSound.PlayOneShot(paperSoundClip);
        pause.canPause = false;

    }
    public void Update()
    {
        if (isLooking)
        {

            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape))
            {
                cameraController.GetComponent<CameraControl>().enabled = true;
                canvas.SetActive(false);
                player.SetActive(true);
                isLooking=false;
                pause.canPause = true;
                paperSound.PlayOneShot(paperdown);
            }
        }

    }
}
