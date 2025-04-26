using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int numOfBounces;
    private bool isFacingRight = true;
    Rigidbody2D rb;


    void Start() {
rb = GetComponent<Rigidbody2D>();
    }
    void Update() {
        if (isFacingRight && rb.velocity.x < 0f|| !isFacingRight && rb.velocity.x >0f )
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
           
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
       
        if (other != null)
        {
            if (other.gameObject != null)
            {
                
                numOfBounces++;
                
                
                if (other.gameObject.tag == "Enemy")
                {
                  
                    other.gameObject.GetComponent<EnemyBase>().BaseHit(2,0,0);
                    other.gameObject.GetComponent<EnemyControler>().rb.velocity = new Vector2(0,0);
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
