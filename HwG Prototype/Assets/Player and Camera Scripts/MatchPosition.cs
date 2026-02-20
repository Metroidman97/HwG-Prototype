using UnityEngine;

public class MatchPosition : MonoBehaviour
{

    public Transform cameraHolder;
  
    void Start()
    {
        // Set the initial position of this object to match the camera holder's position 
        // In all honesty I don't remember why I made this script since Camera holder does the same thing
        transform.position = cameraHolder.position;
    }

   
}
