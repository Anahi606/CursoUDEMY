using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FightPlayer : MonoBehaviour, IDamagable
{
    public static FightPlayer Instance { get; private set; }

    [SerializeField] private float health;
    private Animator animator;
    [SerializeField] GameObject blood;
    [SerializeField] float hitflashSpeed;
    [SerializeField] private HealthBar healthBar;
    private bool isInvincible = false;
    private bool restoreTime;
    private float restoreTimeSpeed;
    private SpriteRenderer sr;
    private bool isDead = false;
    [SerializeField] private float enemyDamage = 10f;
    [SerializeField] private string sceneToLoad = "Boss 1";

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            TomarDaño(enemyDamage);
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

        if (SceneManager.GetActiveScene().name == "02 - Level1")
        {
            PlayerPrefs.DeleteKey("DoubleJumpUnlocked");
        }

        StartCoroutine(RestartFight());
    }

    private IEnumerator RestartFight()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneToLoad);
    }
}
