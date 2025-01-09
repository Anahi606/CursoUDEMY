using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCaC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoGolpe;

    private HeroKnight heroKnight;

    private void Start()
    {
        heroKnight = GetComponent<HeroKnight>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Golpe();
        }

        if (heroKnight != null)
        {
            controladorGolpe.localPosition = new Vector3(heroKnight.m_facingDirection * Mathf.Abs(controladorGolpe.localPosition.x), controladorGolpe.localPosition.y, controladorGolpe.localPosition.z);
        }
    }

    private void Golpe()
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                colisionador.transform.GetComponent<BossOne>().TomarDaño(dañoGolpe);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (controladorGolpe != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
        }
    }
}
