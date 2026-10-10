using UnityEngine;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private CanvasGroup blackScreen;

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
}
