using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int numOfBounces;

    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other != null)
        {
            if (other.gameObject != null)
            {
                
                numOfBounces++;
                
                Debug.Log(other.gameObject.name);
                if (other.gameObject.tag == "Enemy")
                {
                    Destroy(other.gameObject);
                    Destroy(this.gameObject);
                }
               if ( numOfBounces > 2)
                {
                    numOfBounces = 0;
                    Destroy(this.gameObject);
                }

            }
        }
    }
}
