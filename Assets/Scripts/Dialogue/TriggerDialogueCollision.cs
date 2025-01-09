using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueCollision : MonoBehaviour
{
    public GameObject dialogueObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!dialogueObject.activeInHierarchy)
            {
                dialogueObject.SetActive(true);

                Dialogue dialogueScript = dialogueObject.GetComponent<Dialogue>();
                if (dialogueScript != null)
                {
                    dialogueScript.StartDialogue();
                }
            }
        }
    }
}
