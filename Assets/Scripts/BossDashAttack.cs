using System.Collections;
using UnityEngine;

public class BossDashAttack : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 20f;
    [SerializeField] private float dashDuration = 0.3f;
    [SerializeField] private float cooldownDash = 4f;
    [SerializeField] private float dañoDash = 15f;
    [SerializeField] private float preDashWarningTime = 1.5f;

    private Rigidbody2D rb2D;
    private Transform jugador;
    private bool isDashing = false;
    private bool canDash = true;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private BossOne bossOne;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        bossOne = GetComponent<BossOne>();
    }

    private void Update()
    {
        if (canDash && bossOne != null && bossOne.GetHealth() <= 300)
        {
            StartCoroutine(PrepareForDash());
        }
    }

    private IEnumerator PrepareForDash()
    {
        canDash = false;

        float elapsedTime = 0f;
        while (elapsedTime < preDashWarningTime)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            elapsedTime += 0.2f;
        }

        animator.SetTrigger("Dash");

        yield return new WaitForSeconds(0.1f);

        StartCoroutine(Dash());
    }

    private IEnumerator Dash()
    {
        isDashing = true;

        Vector2 direccionDash = (jugador.position - transform.position).normalized;

        rb2D.velocity = direccionDash * dashSpeed;

        yield return new WaitForSeconds(dashDuration);

        rb2D.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(cooldownDash);
        canDash = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDashing && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<FightPlayer>().TomarDaño(dañoDash);

            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
                playerRb.AddForce(knockbackDirection * dashSpeed, ForceMode2D.Impulse);
            }
        }
    }
}
