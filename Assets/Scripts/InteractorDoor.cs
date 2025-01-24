using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InteractorDoor : MonoBehaviour
{
    public GameObject imageObject;
    public TMP_Text textObject;
    public string sceneToLoad;

    private bool isNearObject = false;
    public bool hasInteracted = false;

    void Start()
    {
        imageObject.SetActive(false);
        textObject.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isNearObject && !hasInteracted && Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(sceneToLoad);
            hasInteracted = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            isNearObject = true;
            imageObject.SetActive(true);
            textObject.gameObject.SetActive(true);
            textObject.text = "Presiona F para entrar";
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearObject = false;
            imageObject.SetActive(false);
            textObject.gameObject.SetActive(false);
        }
    }
}
