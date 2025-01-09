using System.Collections;
using UnityEngine;

public class DialogueAndImage : MonoBehaviour
{
    public GameObject canvasDialogos; // El Canvas que contiene los diálogos
    public GameObject dialogo1;
    public GameObject dialogo2;
    public GameObject imagen1;
    public GameObject imagen2;
    public GameObject player;

    public AudioSource audioImagen1; // Audio que se reproduce con imagen1
    public AudioSource audioDialogo2; // Audio que se reproduce con dialogo2

    private bool sequenceStarted = false;
    private bool image1Activated = false; // Controla si imagen1 ya fue activada
    private bool image2Activated = false; // Controla si imagen2 ya fue activada
    private bool playerDeactivated = false; // Controla si el player ya fue desactivado

    // Este método se debe llamar cuando se desactiva el dialogo1
    public void OnDialog1Deactivated()
    {
        if (!sequenceStarted)
        {
            sequenceStarted = true;
            StartCoroutine(SequenceCoroutine());
        }
    }

    private IEnumerator SequenceCoroutine()
    {
        // Activa imagen1 si no ha sido activada y también el audio de imagen1
        if (!image1Activated)
        {
            Debug.Log("Activando imagen1 y su audio");
            imagen1.SetActive(true);
            if (audioImagen1 != null)
            {
                audioImagen1.gameObject.SetActive(true); // Asegúrate de que el AudioSource está activo
                audioImagen1.Play(); // Reproduce el audio asociado a imagen1
            }
            image1Activated = true;
        }

        // Espera 3 segundos
        yield return new WaitForSeconds(3f);

        // Activa el Canvas, desactiva imagen1 y su audio, y desactiva el player
        if (!image2Activated)
        {
            Debug.Log("Activando Canvas de diálogos, desactivando player, imagen1 y su audio");
            canvasDialogos.SetActive(true); // Activa el Canvas en lugar de solo el dialogo2
            imagen2.SetActive(true); // Activa imagen2 cuando se activa el canvas
            image2Activated = true;
        }

        if (!playerDeactivated)
        {
            imagen1.SetActive(false);
            if (audioImagen1 != null)
            {
                audioImagen1.Stop(); // Detiene el audio asociado a imagen1
                audioImagen1.gameObject.SetActive(false); // Desactiva el AudioSource
            }
            player.SetActive(false); // Desactiva el player una sola vez
            playerDeactivated = true;
        }

        // Espera 2 segundos para que se active el diálogo dentro del canvas
        yield return new WaitForSeconds(2f);

        Debug.Log("Activando sonido del Canvas");
        if (audioDialogo2 != null && !audioDialogo2.isPlaying) // Solo reproduce el audio si no está ya en reproducción
        {
            audioDialogo2.gameObject.SetActive(true); // Asegúrate de que el AudioSource está activo
            audioDialogo2.Play(); // Reproduce el audio del Canvas
        }

        // Espera a que el dialogo2 dentro del canvas termine
        while (dialogo2.activeSelf)
        {
            yield return null; // Espera hasta que el dialogo2 dentro del canvas se desactive
        }

        // Cuando dialogo2 se desactiva, desactiva la imagen2, el audio y activa el player
        Debug.Log("Desactivando imagen2, deteniendo audio del Canvas y activando player");
        imagen2.SetActive(false); // Desactiva imagen2
        if (audioDialogo2 != null)
        {
            audioDialogo2.Stop(); // Detiene el audio
            audioDialogo2.gameObject.SetActive(false); // Desactiva el AudioSource
        }
        player.SetActive(true); // Activa el player después de que el dialogo2 termine

        sequenceStarted = false; // Permite reiniciar la secuencia si es necesario
    }

    void Update()
    {
        // Verifica cuando dialogo1 se desactiva para iniciar la secuencia
        if (!dialogo1.activeSelf && !sequenceStarted)
        {
            OnDialog1Deactivated();
        }
    }
}
