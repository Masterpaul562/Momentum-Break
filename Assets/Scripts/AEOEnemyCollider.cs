using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AEOEnemyCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D AEOCollider;
    [SerializeField] private BoxCollider2D enemyCollider;
    
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other != null)
        {
            if (other.gameObject.tag == "Enemy")
            {


                enemyCollider.enabled =false;
                    other.GetComponent<EnemyBase>().BaseHit(1, 10, 10);
                enemyCollider.enabled = true;

            }
        }
    }
}
