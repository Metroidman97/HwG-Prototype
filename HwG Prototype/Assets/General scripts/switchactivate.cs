using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class switchactivate : MonoBehaviour
{
    public Animator animator;
    public PlayableDirector Timeline;
    public AudioSource switchSound;
    public AudioSource music;
    public void flipSwitch()
    {
        animator.SetTrigger("Flip");
        RenderSettings.fog = false;
        switchSound.Play();
        music.Stop();
        Timeline.Play();
        StartCoroutine(goMainMenu());
    }

    IEnumerator goMainMenu()
    {
        yield return new WaitForSeconds(6.7f);
        SceneLoader sceneLoader = FindObjectOfType<SceneLoader>();
        sceneLoader.LoadNextLevel();
    }
}
