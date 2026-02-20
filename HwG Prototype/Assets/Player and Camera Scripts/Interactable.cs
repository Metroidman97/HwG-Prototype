using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
   // this is a script I wrote using a highlight object plugin from the unity store. It's currently commented out but still available
   //we could use that plugin or find a new way to outline things without using things from the store, either way It's here if we need it
    //Outline outline;
    public string message;

    public UnityEvent onInteraction;
    void Start()
    {
        //outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        onInteraction.Invoke();
        
    }

    public void DisableOutline()
    {
      //  outline.enabled = false;
    }
    public void EnableOutline()
    {
       // outline.enabled = true;
    }





   
}
