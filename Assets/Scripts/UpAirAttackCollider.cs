using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAirAttackCollider : MonoBehaviour
{
    [SerializeField] private GameObject player;
    void OnTriggerEnter2D (Collider2D other) { 
    if(other.gameObject.tag == "Enemy")
        {
            player.GetComponent<Move_Player>().doubleJumped = true;
            player.GetComponent<Move_Player>().jumpPower = 45;
            other.gameObject.GetComponent<EnemyControler>().BaseHit(1, 2, 40);
            player.GetComponent<Move_Player>().UpAirCollider.enabled = false;
        }
    }
}
