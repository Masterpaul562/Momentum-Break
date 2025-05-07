using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowControler : MonoBehaviour
{
   public Animator animator;
  
    // Update is called once per frame
    void Update()
    {
        if(animator.GetBool("Explode"))
        {
Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
       
        if(other.gameObject.tag == "Projectile"){
animator.SetTrigger("Break");
        }
    }
}
