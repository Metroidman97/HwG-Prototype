using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CutsceneTrigger : MonoBehaviour
{

    public PlayableDirector timeline;
    public Collider timelineCollider;
    public GameObject timelineObject;
    public GameObject cameraObject;
    public GameObject playerObject;
    public GameObject skipText;
    private bool isPlaying;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.Play();
            timelineCollider.enabled = false;
            isPlaying = true;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isPlaying == true || Input.GetKeyDown(KeyCode.JoystickButton2) && isPlaying == true)
        {
            timelineObject.SetActive(false);
            cameraObject.SetActive(false);
            playerObject.SetActive(true);
            skipText.SetActive(false);

        }
    }
        
}
