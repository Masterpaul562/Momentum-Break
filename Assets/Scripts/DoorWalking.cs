using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorWalking : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Transform exit;
    private float y;
    public bool isExit;
    public int enemiesAlive;
    public GameObject Spawner;
    public Animator animator;
    private bool once;
   

    private void Start()
    {
        exit = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        y= Input.GetAxisRaw("Vertical");
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (player.GetComponent<Move_Player>().doorExplode == true)
        {
            animator.SetBool("explode",true);
        }
        
       if (player.GetComponent<Move_Player>().animator.GetBool("ShouldWalk"))
        {
            player.transform.position = exit.position;
            player.GetComponent<Move_Player>().doorExplode = false;
            player.GetComponent<Move_Player>().isAttacking = false;
            player.GetComponent<Move_Player>().animator.SetBool("ShouldWalk", false);
        }
        if(y ==1 )
        {
           
            if (!isExit)
            {
                player.GetComponent<Move_Player>().hasEnteredRoom = true;
                player.GetComponent<Move_Player>().animator.SetBool("DoorBreak", true);
                player.GetComponent<Move_Player>().rb.velocity = new Vector2(0, 0); 
                player.GetComponent<Move_Player>().isAttacking = true;


            }
            else
            {
                if (Spawner.GetComponent<EnemySpawner>().count == 0 && isExit)
                {                 
                    player.GetComponent<Move_Player>().hasEnteredRoom = false;
                    player.GetComponent<Move_Player>().animator.SetBool("DoorBreak", true);
                    player.GetComponent<Move_Player>().rb.velocity = new Vector2(0, 0);
                    player.GetComponent<Move_Player>().isAttacking = true;
                }
            }
        }
    }
    
        
        
       
       // player.transform.position = exit.position;
    
}
