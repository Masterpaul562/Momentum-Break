using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : EnemyBase
{
public LayerMask hitPlayer;
public Animator animator;
private bool isFacingRight = true; 


private void Awake() {
    health = 6;
}
    private void Update()
    {
        if (health<= 0){
            animator.SetBool("DIE",true);
            animator.SetBool("shouldHurt",false);
            if(animator.GetBool("Perishes")){
               Destroy(this.gameObject);
            }
        }
        Debug.DrawRay(transform.position,new Vector2(transform.localScale.x, 0)*1.5f);
        Flip();
        Move();
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    protected override void Move()
    {
        Vector3 newPlayerPos = new Vector3(player.transform.position.x, this.transform.position.y,0);
        Vector3 moveDirection = (newPlayerPos - this.transform.position).normalized;
         Vector3 distance = (newPlayerPos - this.transform.position);
         if (Mathf.Abs(distance.x) > 2f && animator.GetBool("shouldAttack") == true)
        {
            animator.SetBool("shouldHurt", false);
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
             rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
         }else {
            if(Physics2D.Raycast(this.transform.position,new Vector2 (this.transform.localScale.x,0), 1.5f, hitPlayer)&&animator.GetBool("shouldAttack") ==true)
            {
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
            rb.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezeRotation;
         }
       
       
    }
   
    protected override void Hit(int damage)
    {
        health -= damage;
        animator.SetBool("Hurt",true);
        animator.SetBool("shouldHurt",false);
    }
    private void Flip() {
        if (isFacingRight && rb.velocity.x < 0f|| !isFacingRight && rb.velocity.x >0f )
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
           
        }
    }
}
