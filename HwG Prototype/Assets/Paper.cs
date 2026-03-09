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
    // Start is called before the first frame update

    public void lookAtPage()
    {
        Debug.Log("Hello");
        canvas.SetActive(true);
        player.SetActive(false);
        cameraController.GetComponent<CameraControl>().enabled = false;
        isLooking = true;



    }
    public void Update()
    {
        if (isLooking)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                cameraController.GetComponent<CameraControl>().enabled = true;
                canvas.SetActive(false);
                player.SetActive(true);
                isLooking=false;
            }
        }

    }
}
