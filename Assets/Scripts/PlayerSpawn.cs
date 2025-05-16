using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject Enemy;
    public Transform endPosition;
    public Animator animator;
    public Animator Enemyanimator;
    
    void Start()
    {
        player.transform.position = this.gameObject.transform.position;
    }

   
    void Update()
    {
        if(Input.GetKeyDown(player.GetComponent<Move_Player>().specailAtkKey))
        {
            animator.SetTrigger("BreakOut");
        }

    }
    public void MoveThis(){
        transform.position = endPosition.position;
        Enemy.transform.position = endPosition.position;
        Enemyanimator.SetTrigger("Die");
    }
    public void destroyThis(){
        player.SetActive(true);
        player.transform.position = this.gameObject.transform.position;
        Destroy(this.gameObject);
        Destroy(Enemy);
    }
}
