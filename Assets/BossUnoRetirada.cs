using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUnoRetirada : StateMachineBehaviour
{
    private BossOne jefe;
    private Rigidbody2D rb2D;
    private Transform jugador;
    [SerializeField] private float velocidadDash;
    [SerializeField] private float dañoDash;
    [SerializeField] private float empujeDash;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        jefe = animator.GetComponent<BossOne>();
        rb2D = jefe.rb2D;
        jugador = jefe.jugador;

        jefe.MirarJugador();
        Vector2 direccionDash = (jugador.position - animator.transform.position).normalized;
        rb2D.velocity = direccionDash * velocidadDash;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Opcional: Aquí puedes agregar efectos visuales o sonidos adicionales durante el dash
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb2D.velocity = Vector2.zero; // Detener el dash al salir del estado
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hacer daño al jugador y aplicarle empuje
            collision.gameObject.GetComponent<FightPlayer>().TomarDaño(dañoDash);
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                Vector2 direccionEmpuje = (collision.transform.position - jefe.transform.position).normalized;
                playerRb.AddForce(direccionEmpuje * empujeDash, ForceMode2D.Impulse);
            }
        }
    }
}
