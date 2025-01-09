using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float timeBtwAttacks = 1f;

    private float shootTimer;

    private Collider2D coll;

    private void Update()
    {
        shootTimer = Time.deltaTime;
        if (shootTimer >= timeBtwAttacks)
        {
            shootTimer = 0;

            Shoot();
        }
    }

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

    public void Shoot()
    {
        Rigidbody2D bulletRB = Instantiate(bulletPrefab, transform.position, transform.rotation);

        Vector2 shootDirection = GetShootDirection();
        bulletRB.velocity = shootDirection * bulletSpeed;

        EnemyProjectile enemyProjectile = bulletRB.gameObject.GetComponent<EnemyProjectile>();
        enemyProjectile.EnemyColl = coll;
    }

    public Vector2 GetShootDirection()
    {
        Transform playertrans = GameObject.FindGameObjectWithTag("Player").transform;
        return (playertrans.position - transform.position).normalized;
    }
}
