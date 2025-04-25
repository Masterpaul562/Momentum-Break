using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamTriggerCollider : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.GetComponent<EnemyBase>().BaseHit(2);
            }
        }
    }
}
