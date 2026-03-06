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
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            timeline.Play();
            timelineCollider.enabled = false;
        }
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            timelineObject.SetActive(false);
            cameraObject.SetActive(false);
            playerObject.SetActive(true);
            skipText.SetActive(false);

        }
    }
        
}
