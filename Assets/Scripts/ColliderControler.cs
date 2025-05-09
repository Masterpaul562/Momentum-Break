using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderControler : MonoBehaviour
{
    public Animator animator; 
    public GameObject wall;

void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectile")
        {
            
            animator.SetTrigger("Press");
            Destroy(wall);
        }
    }
}
