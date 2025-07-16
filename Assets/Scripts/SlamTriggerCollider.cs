using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamTriggerCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D Player;
   private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other != null)
        {
            if (other.gameObject.tag == "Enemy")
            {
                Player.enabled = false;

                other.GetComponent<EnemyBase>().BaseHit(2,30,10);
                Player.enabled = true;
            }
        }
    }
}
