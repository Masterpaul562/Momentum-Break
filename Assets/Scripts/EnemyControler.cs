using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControler : EnemyBase
{

    private void Update()
    {
        Move();
    }

    protected override void Move()
    {
        Vector3 newPlayerPos = new Vector3(player.transform.position.x, this.transform.position.y,0);
        Vector3 moveDirection = (newPlayerPos - this.transform.position).normalized;
        rb.velocity = new Vector2(moveDirection.x * speed, rb.velocity.y);
    }
    protected override void Hit()
    {
        Destroy(this.gameObject);
    }

}
