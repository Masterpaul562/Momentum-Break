using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControler : MonoBehaviour
{
    public Animator animator; 

void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            Debug.Log("YAY");
            animator.SetTrigger("Press");
        }
    }
}
