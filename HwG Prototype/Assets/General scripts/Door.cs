using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator animator;
    public AudioSource doorSound;
   
    public void OpenDoor()
    {
        animator.SetTrigger("DoorOpen");
        doorSound.Play();
    }
 
   
}
