using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    

    public Animator transition;
    public float transitionTime = 2f;
    public AudioSource music;
    public AudioSource transitionSFX;
    public string transitionTrigger;

    
    //Loads Next Level in build
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    public void ReloadLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Stop music if music is set
        if (music != null)
        {
            music.Stop();
        }
        //Play Transition Animation
        transition.SetTrigger(transitionTrigger);
        //Play Transition Audio if set
        if (transitionSFX != null)
        {
            transitionSFX.Play();
        }
        //Wait
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        SceneManager.LoadScene(levelIndex);
    }
}
