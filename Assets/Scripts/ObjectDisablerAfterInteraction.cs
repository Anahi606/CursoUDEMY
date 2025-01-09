using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDisablerAfterInteraction : MonoBehaviour
{
    public Interactor interactorScript; // El script de interacción que ya tienes
    public GameObject objectToDeactivate1; // Primer objeto que deseas desactivar
    public GameObject objectToDeactivate2; // Segundo objeto que deseas desactivar

    void Update()
    {
        // Verifica si ya has interactuado con el objeto
        if (interactorScript != null && interactorScript.hasInteracted)
        {
            // Desactiva ambos objetos
            objectToDeactivate1.SetActive(false);
            objectToDeactivate2.SetActive(false);

            // Desactiva este script para que no siga verificando
            this.enabled = false;
        }
    }
}
