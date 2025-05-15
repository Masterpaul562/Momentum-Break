using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    
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
    public void destroyThis(){
        //player.SetActive(true);
       // player.transform.position = this.gameObject.transform.position;
      //  Destroy(this.gameObject);
    }
}
