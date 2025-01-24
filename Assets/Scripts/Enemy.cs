using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    [SerializeField] private float health = 20f;
    private Animator animator;
    private Rigidbody2D rb;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TomarDaño(float daño)
    {
        if (isDead) return;

        health -= daño;
        animator.SetTrigger("Hit");

        if (health <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        isDead = true;
        animator.SetTrigger("Death");
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
        Destroy(gameObject, 1.5f);
    }
}
