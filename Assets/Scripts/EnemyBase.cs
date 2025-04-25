using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    public float health;
    public float speed;
    public float atkSpeed;
    public GameObject player;
    public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }
    public void BaseHit(int damage)
    {
        Hit(damage);
    }
    
    protected virtual void Hit(int damage)
    {

    }
    protected virtual void Move()
    {

    }
}
