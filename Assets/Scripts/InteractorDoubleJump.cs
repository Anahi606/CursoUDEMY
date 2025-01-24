using System.Collections;
using UnityEngine;
using TMPro;

public class InteractorDoubleJump : MonoBehaviour
{
    public GameObject imageObject;
    public TMP_Text textObject;
    public string unlockMessage = "Presiona espacio 2 veces para realizar un salto doble";
    public float messageDuration = 4f;

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
            UnlockDoubleJump();
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
            textObject.text = "Presiona F para desloquear el salto doble";
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

    private void UnlockDoubleJump()
    {
        PlayerPrefs.SetInt("DoubleJumpUnlocked", 1);
        PlayerPrefs.Save();
        StartCoroutine(ShowUnlockMessage());
    }

    private IEnumerator ShowUnlockMessage()
    {
        textObject.text = unlockMessage;
        yield return new WaitForSeconds(messageDuration);
        textObject.gameObject.SetActive(false);
    }
}
