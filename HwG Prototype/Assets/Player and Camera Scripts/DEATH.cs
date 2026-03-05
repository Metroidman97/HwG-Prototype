using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;
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
    public float rotationSpeed = .00001f; // Rotation speed in degrees per second

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            player.GetComponent<Movement>().enabled = false;
            playerCamera.GetComponent<CameraControl>().enabled = false;
            alien.GetComponent<MonsterNav>().enabled = false;
            alien.GetComponent<NavMeshAgent>().enabled = false;
            StartCoroutine(RotateCameraToAlien(.3f)); // Rotate over 2 seconds


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
        
    }


}
