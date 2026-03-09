using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchactivate : MonoBehaviour
{
    public Animator animator;

    public void flipSwitch()
    {
        animator.SetTrigger("Flip");
    }
}
