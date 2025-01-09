using System.Collections;
using UnityEngine;

public class FadeEffect : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1f;

    private void Start()
    {
        StartCoroutine(FadeInOutTwice());
    }

    private IEnumerator FadeInOutTwice()
    {
        yield return StartCoroutine(FadeIn(0f, 1f));
        yield return StartCoroutine(FadeOut(1f, 0f));
        yield return StartCoroutine(FadeIn(0f, 1f));
        yield return StartCoroutine(FadeOut(1f, 0f));
    }

    private IEnumerator FadeIn(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }

    private IEnumerator FadeOut(float startAlpha, float endAlpha)
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;
    }
}
