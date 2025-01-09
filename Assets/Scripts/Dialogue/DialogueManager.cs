using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Dialogue firstDialogue;
    public Dialogue secondDialogue;
    public GameObject objectToActivateAfterFirst;
    public GameObject objectToDeactivateAfterSecond;
    public AudioSource audioSourceForSecondDialogue;

    private bool firstDialogueCompleted = false;
    private bool secondDialogueCompleted = false;

    void Start()
    {
        firstDialogue.gameObject.SetActive(true);
        secondDialogue.gameObject.SetActive(false);
        objectToActivateAfterFirst.SetActive(false);
        objectToDeactivateAfterSecond.SetActive(false);
        audioSourceForSecondDialogue.gameObject.SetActive(false); // Asegurarse de que el AudioSource está desactivado
    }

    void Update()
    {
        // Si el primer diálogo se ha completado y no se ha procesado antes
        if (!firstDialogueCompleted && !Dialogue.isDialogueActive && firstDialogue.gameObject.activeSelf == false)
        {
            firstDialogueCompleted = true;
            ActivateSecondDialogue();
        }

        // Si el segundo diálogo se ha completado y no se ha procesado antes
        if (!secondDialogueCompleted && !Dialogue.isDialogueActive && secondDialogue.gameObject.activeSelf == false)
        {
            secondDialogueCompleted = true;
            DeactivateAfterSecondDialogue();
        }
    }

    void ActivateSecondDialogue()
    {
        // Activar el GameObject que debería aparecer tras el primer diálogo
        objectToActivateAfterFirst.SetActive(true);

        // Activar el segundo diálogo
        secondDialogue.gameObject.SetActive(true);
        secondDialogue.StartDialogue();

        // Activar el AudioSource para el segundo diálogo
        audioSourceForSecondDialogue.gameObject.SetActive(true);
        audioSourceForSecondDialogue.Play();
    }

    void DeactivateAfterSecondDialogue()
    {
        // Desactivar el GameObject después de que el segundo diálogo termine
        objectToDeactivateAfterSecond.SetActive(false);

        // Detener y desactivar el AudioSource para el segundo diálogo
        audioSourceForSecondDialogue.Stop();
        audioSourceForSecondDialogue.gameObject.SetActive(false);
    }
}
