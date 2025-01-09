using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInCameraRange : MonoBehaviour
{
    public Transform player; // Asigna aquí el transform del player
    public Canvas dialogueCanvas; // El Canvas que contiene el diálogo
    public Vector3 offset; // Offset para ajustar la posición del diálogo respecto al player
    private Camera mainCamera; // Referencia a la cámara principal

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // Convertir la posición del jugador a las coordenadas de la pantalla
        Vector3 screenPosition = mainCamera.WorldToScreenPoint(player.position + offset);

        // Actualizar la posición del Canvas en la pantalla
        dialogueCanvas.transform.position = screenPosition;
    }
}
