using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossOne : MonoBehaviour, IDamagable
{
    private Animator animator;
    public Rigidbody2D rb2D;
    public Transform jugador;
    private bool mirandoDerecha = true;
    private EnemyShoot enemyShoot;
    private SpriteRenderer sp;

    [Header("Vida")]
    [SerializeField] private float health;
    private bool isDead = false;

    [Header("Ataque")]
    [SerializeField] private Transform AttackController;
    [SerializeField] private float radioAtaque;
    [SerializeField] private float dañoAtaque;

    [Header("Dash")]
    [SerializeField] private float distanciaCercana = 2f;
    [SerializeField] private float tiempoCercaParaDash = 3f;
    private float tiempoCercaJugador = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyShoot = GetComponent<EnemyShoot>();
        sp = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador);

        if (distanciaJugador <= distanciaCercana)
        {
            tiempoCercaJugador += Time.deltaTime;

            if (tiempoCercaJugador >= tiempoCercaParaDash)
            {
                animator.SetTrigger("Dash");
                tiempoCercaJugador = 0f;
            }
        }
        else
        {
            tiempoCercaJugador = 0f;
        }
    }

    public void TomarDaño(float daño)
    {
        if (isDead) return;

        health -= daño;
        StartCoroutine(FlashWhite());
        if (health <= 0)
        {
            isDead = true;
            animator.SetTrigger("Death");
        }
    }
    private IEnumerator FlashWhite()
    {
        sp.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        sp.color = Color.white;
    }
    public float GetHealth()
    {
        return health;
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }

    public void MirarJugador()
    {
        if ((jugador.position.x > transform.position.x && !mirandoDerecha) || (jugador.position.x < transform.position.x && mirandoDerecha))
        {
            mirandoDerecha = !mirandoDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        }
    }

    public void Ataque()
    {
        if (isDead) return;

        //Activa el metodo shoot
        enemyShoot?.Shoot();

        Collider2D[] objetos = Physics2D.OverlapCircleAll(AttackController.position, radioAtaque);

        foreach (Collider2D collision in objetos)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<FightPlayer>().TomarDaño(dañoAtaque);
                FightPlayer.Instance.HitStopTime(0, 5, 0.5f);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackController.position, radioAtaque);
    }
}
