using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class FightPlayer : MonoBehaviour, IDamagable
{
    public static FightPlayer Instance { get; private set; }

    [SerializeField] private float health;
    private Animator animator;
    [SerializeField] GameObject blood;
    [SerializeField] float hitflashSpeed;
    [SerializeField] private HealthBar healthBar;
    private bool isInvincible = false;
    bool restoreTime;
    float restoreTimeSpeed;
    private SpriteRenderer sr;
    private bool isDead = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //Detener el Update si el jugador esta tieso
        if (isDead) return;
        RestoreTimeScale();
        FlashWhileInvincible();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        healthBar.InicializarBarraDeVida(health);
    }

    public void TomarDaño(float daño)
    {
        if (!isInvincible && !isDead)
        {
            health -= daño;
            healthBar.CambiarVidaActual(health);
            StartCoroutine(StopTakingDamage());

            if (health <= 0)
            {
                Muerte();
            }
        }
    }

    private IEnumerator StopTakingDamage()
    {
        isInvincible = true;
        GameObject _bloodParticles = Instantiate(blood, transform.position, Quaternion.identity);
        Destroy(_bloodParticles, 1.5f);

        animator.SetTrigger("Hurt");

        yield return new WaitForSeconds(1f);

        isInvincible = false;
    }
    void FlashWhileInvincible()
    {
        sr.material.color = isInvincible ? Color.Lerp(Color.white, Color.black, Mathf.PingPong(Time.time * hitflashSpeed, 1.0f)) : Color.white;
    }

    public void HitStopTime(float _newTimeScale, int _restoreSpeed, float _delay)
    {
        restoreTimeSpeed = _restoreSpeed;
        Time.timeScale = _newTimeScale;

        if (_delay > 0)
        {
            StopCoroutine(StartTimeAgain(_delay));
            StartCoroutine(StartTimeAgain(_delay));
        }
        else
        {
            restoreTime = true;
        }
    }

    private IEnumerator StartTimeAgain(float _delay)
    {
        restoreTime = true;
        yield return new WaitForSeconds(_delay);
    }

    private void RestoreTimeScale()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1)
            {
                Time.timeScale += Time.deltaTime * restoreTimeSpeed;
            }
            else
            {
                Time.timeScale = 1;
                restoreTime = false;
            }
        }
    }

    private void Muerte()
    {
        isDead = true;
        animator.SetTrigger("Death");
        var movimiento = GetComponent<HeroKnight>();
        if (movimiento != null)
        {
            movimiento.enabled = false;
        }
        StartCoroutine(RestartFight());
    }
    private IEnumerator RestartFight()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Boss 1");
    }
}
