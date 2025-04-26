using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health_Player : MonoBehaviour
{
    public HealthBar healthBar;
    public Animator animator;
    private int currentHealth;
    public int maxHealth;
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        animator.SetTrigger("TakeDamage");
        healthBar.SetHealth(currentHealth);
    }
}
