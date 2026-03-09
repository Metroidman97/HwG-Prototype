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
    public GameObject Alien1;
    public GameObject Alien2;
    public GameObject Alien3;
    public MonsterNav monsterNav1;
    public MonsterNav monsterNav2;
    public MonsterNav monsterNav3;


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
        if (Input.GetKeyDown(KeyCode.X) && isPlaying == true     || Input.GetKeyDown(KeyCode.JoystickButton2) && isPlaying == true)
        {
            timelineObject.SetActive(false);
            cameraObject.SetActive(false);
            playerObject.SetActive(true);
            skipText.SetActive(false);
            GameObject[] gameObjectArray = GameObject.FindGameObjectsWithTag("Delete");

            foreach (GameObject go in gameObjectArray)
            {
                go.SetActive(false);
            }
            Alien1.SetActive(true);
            monsterNav1.isWaiting = false;

            Alien2.SetActive(true);
            monsterNav2.isWaiting = false;
            Alien3.SetActive(true);
            monsterNav3.isWaiting = false;

        }
    }
        
}
