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
    }
    public void BaseHit()
    {
        Hit();
    }
    
    protected virtual void Hit()
    {

    }
    protected virtual void Move()
    {

    }
}
