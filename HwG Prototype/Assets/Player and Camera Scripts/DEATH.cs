using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
using UnityEngine.Playables;
using static UnityEngine.GraphicsBuffer;

public class DEATH : MonoBehaviour
{

    public Collider playerCollider;
    public GameObject player;
    public Camera playerCamera;
    public Transform cameraTransform;
    public Collider deathCollider;
    public GameObject alien;
    public GameObject alienHead;
    public float rotationSpeed = .00001f; // Rotation speed is being weird so float is really really small for comfort
    public PlayableDirector deathTimeline;
    public Animator fadeInanimator;

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Movement>().enabled = false;
            playerCamera.GetComponent<CameraControl>().enabled = false;
            alien.GetComponent<MonsterNav>().enabled = false;
            alien.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(RotateCameraToAlien(.3f)); 


            Debug.Log("Player has died");
        }
    }

    private IEnumerator RotateCameraToAlien(float duration)
    {
 

        
        Transform camTransform = playerCamera.transform;

        Vector3 direction = alienHead.transform.position - camTransform.position;
        

        Quaternion startRot = playerCamera.transform.rotation;
        Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);

        float elapsed = 0f;
        

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            playerCamera.transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
            yield return null;
        }

        playerCamera.transform.rotation = targetRot;
        deathTimeline.Play();
        yield return new WaitForSeconds(4f);
        fadeInanimator.SetTrigger("FadeIn");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }


}
