using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownAirAttackCollider : MonoBehaviour
{

    [SerializeField] private GameObject player;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            
            if (other.gameObject.GetComponent<EnemyControler>().IsGrounded() == false)
            {
                
                other.gameObject.GetComponent<EnemyControler>().BaseHit(1, 2, -140);
                player.GetComponent<Move_Player>().DownAirCollider.enabled = false;
            }
        }
    }
}
