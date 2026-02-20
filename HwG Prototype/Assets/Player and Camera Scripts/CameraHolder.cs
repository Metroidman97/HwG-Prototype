using UnityEngine;

public class CameraHolder : MonoBehaviour
{

    public Transform cameraPosition;
  
    void Update()
    {
        // This script is used to make the camera follow the player, it is attached to the orientation object which is a child of the player object
        transform.position = cameraPosition.position;
    }
}
