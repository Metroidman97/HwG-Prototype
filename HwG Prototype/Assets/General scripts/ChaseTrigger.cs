using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTrigger : MonoBehaviour
{
    public GameObject alien;

    private Chase chase;

    // Start is called before the first frame update
    void Start()
    {
        chase = alien.GetComponent<Chase>();
        alien.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            alien.SetActive(true);
            chase.isChasing = true;
            Destroy(this.gameObject);
        }
    }
}
