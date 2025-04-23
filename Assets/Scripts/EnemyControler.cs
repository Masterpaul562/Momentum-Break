using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : EnemyBase
{
public LayerMask enemy;
public Animator animator;
private bool isFacingRight = true; 


    private void Update()
    {
        Debug.DrawRay(transform.position,new Vector2(transform.localScale.x, 0)*2);
        Flip();
        Move();
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    protected override void Move()
    {
        Vector3 newPlayerPos = new Vector3(player.transform.position.x, this.transform.position.y,0);
        Vector3 moveDirection = (newPlayerPos - this.transform.position).normalized;
         Vector3 distance = (newPlayerPos - this.transform.position);
         if (Mathf.Abs(distance.x) > 2 ){
            rb.constraints = ~RigidbodyConstraints2D.FreezePositionX;
             rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
         }else {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezeRotation;
         }
       
       
    }
    protected override void Hit()
    {
        Destroy(this.gameObject);
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
