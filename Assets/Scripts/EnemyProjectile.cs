using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IDeflectable
{
    [SerializeField] private float daño = 10f;
    public Collider2D EnemyColl { get; set; }
    [field: SerializeField] public float ReturnSpeed { get; set; } = 10f;

    private Collider2D coll;
    private Rigidbody2D rb;
    private bool isDeflected = false;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        IgnoreCollisionWithEnemyToggle();

        Destroy(gameObject, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDeflected && collision.CompareTag("Enemigo"))
        {
            IDamagable iDamageable = collision.GetComponent<IDamagable>();
            if (iDamageable != null)
            {
                iDamageable.TomarDaño(daño);
                Destroy(gameObject);
            }
        }
        else if (!isDeflected && collision.CompareTag("Player"))
        {
            IDamagable iDamageable = collision.GetComponent<IDamagable>();
            if (iDamageable != null)
            {
                iDamageable.TomarDaño(daño);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }

    private void IgnoreCollisionWithEnemyToggle()
    {
        if (EnemyColl != null && coll != null)
        {
            Physics2D.IgnoreCollision(coll, EnemyColl, true);
        }
    }

    public void Deflect(Vector2 direction)
    {
        isDeflected = true;
        rb.velocity = direction.normalized * ReturnSpeed;

    }
}
