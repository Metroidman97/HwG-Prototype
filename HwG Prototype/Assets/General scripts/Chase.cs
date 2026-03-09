using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : MonoBehaviour
{
    public bool isChasing = false;
    public float chaseSpeed = 3f;
    public DEATH deathScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
            ChasePlayer();
        if (deathScript.death == true)
        {
            isChasing = false;
        }
    }

    // travel to z = -22
    void ChasePlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -37f), chaseSpeed * Time.deltaTime);
    }
}
