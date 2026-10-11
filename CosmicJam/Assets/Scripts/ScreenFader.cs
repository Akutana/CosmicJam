using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private CanvasGroup TimeUpImage;

    private IEnumerator FadeToBlackCoroutine(float duration)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.blocksRaycasts = true;

        float elapsed = 0f;
        blackScreen.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            blackScreen.alpha = Mathf.Clamp01(elapsed / duration);

            yield return null;
        }

        blackScreen.alpha = 1f;
    }

    public void FadeToBlack(float duration)
    {
        StartCoroutine(FadeToBlackCoroutine(duration));
    }

    public void HideBlackScreen()
    {
        blackScreen.gameObject.SetActive(false);
    }

    public IEnumerator FadeInTimeUpScreenCoroutine(float duration)
    {
        TimeUpImage.gameObject.SetActive(true);
        TimeUpImage.blocksRaycasts = true;

        float elapsed = 0f;
        TimeUpImage.alpha = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            TimeUpImage.alpha = Mathf.Clamp01(elapsed / duration);

            yield return null;
        }

        TimeUpImage.alpha = 1f;
    }

    public void FadeInTimeUpScreen(float duration)
    {
        StartCoroutine(FadeInTimeUpScreenCoroutine(duration));
    }
}
