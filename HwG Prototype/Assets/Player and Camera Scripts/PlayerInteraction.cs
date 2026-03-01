using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;

    //ALL OF THIS SCRIPT IS RAYCAST STUFF TO MAKE YOU BE ABLE TO LOOK AT INTERACTABLES AND INTERACT, IT IS CURRENTLY NOT IN USE
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
            Debug.Log("Interacted with " + currentInteractable.gameObject.name);
        }
    }

    void CheckInteraction()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.tag == "Interactable")
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (currentInteractable && newInteractable != currentInteractable)
                {
                    {
                        currentInteractable.DisableOutline();
                    }
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }

            }
            else
            {
                DisableCurrentInteractable();
            }

        }
        else 
        {
            DisableCurrentInteractable();
        }
    }


    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
       // HudController.instance.EnableInteractionText(currentInteractable.message);
    }
    void DisableCurrentInteractable()
    {
       // HudController.instance.DisableInteractionText();
        if(currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }
    }
}
