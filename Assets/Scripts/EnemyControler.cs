using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : EnemyBase
{
public LayerMask hitPlayer;
public Animator animator;
private bool isFacingRight = true;
    

private void Awake() {
    health = 100;
}
    private void Update()
    {
        //Debug.Log(transform.localScale.x);
        if (health<= 0){
            
            animator.SetBool("DIE",true);
            animator.SetBool("shouldHurt",false);
            animator.SetBool("isHurting",false);
            if(animator.GetBool("Perishes")){
               Destroy(this.gameObject);
               
            }
        }
   
        Flip();
        Move();
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    protected override void Move()
    {
        Vector3 newPlayerPos = new Vector3(player.transform.position.x, this.transform.position.y,0);
        Vector3 moveDirection = (newPlayerPos - this.transform.position).normalized;
         Vector3 distance = (newPlayerPos - this.transform.position);
       

         if (Mathf.Abs(distance.x) > 2f && animator.GetBool("shouldAttack") == true ) 
        {
            animator.SetBool("shouldHurt", false);
            // rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
            if (animator.GetBool("isHurting") == false)
            {
                rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
            }
         }else {
            if(Physics2D.Raycast(this.transform.position,new Vector2 (this.transform.localScale.x,0), 1.5f, hitPlayer)&&animator.GetBool("shouldAttack") ==true && animator.GetBool("isHurting" )== false)
            {
                rb.velocity = new Vector2(0, 0);
                animator.SetBool("shouldAttack", false);
                animator.SetTrigger("EnemyAttack");
                if (animator.GetBool("shouldHurt"))
                {                                      
                        RaycastHit2D hitResult = Physics2D.Raycast(this.transform.position, new Vector2(this.transform.localScale.x, 0), 1.5f, hitPlayer);
                        if(hitResult.collider != null)
                    {
                        hitResult.collider.gameObject.GetComponent<Health_Player>().TakeDamage(20);
                    }
                    
                }
                
            }
            //rb.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezeRotation;
         }
       
       
    }
   
    protected override void Hit(int damage, int knockBack, int knockBackUp)
    {
        health -= damage;
        

        int random = Random.Range(1, 4);
   
        
        if (random == 1)
        {
            animator.SetBool("Hurt1", false);
            animator.SetBool("Hurt2",false); 
            animator.SetBool("Hurt", true);
            
        }else if (random == 2)
        {
            animator.SetBool("Hurt2", false);
            animator.SetBool("Hurt", false);
            animator.SetBool("Hurt1", true);
        }else if (random == 3)  
        {
            animator.SetBool("Hurt", false);
            animator.SetBool("Hurt1", false);
            animator.SetBool("Hurt2", true);
        }
        Vector3 newPlayerPos = new Vector3(player.transform.position.x, this.transform.position.y, 0);
        Vector3 KnockBackDirection = (this.transform.position - newPlayerPos).normalized;
        rb.AddForce(new Vector2(KnockBackDirection.x *knockBack , 0), ForceMode2D.Impulse);
        rb.AddForce(new Vector2(0, knockBackUp), ForceMode2D.Impulse);
        animator.SetBool("shouldHurt",false);
        animator.SetBool("EnemyAttack", false);
        
    }
    private void Flip() {
        if (isFacingRight && rb.velocity.x < 0f&& animator.GetBool("isHurting") == false || !isFacingRight && rb.velocity.x >0f && animator.GetBool("isHurting") == false)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
           
        }
    }
}
