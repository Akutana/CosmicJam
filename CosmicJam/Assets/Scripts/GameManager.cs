using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timerDuration = 100f;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameEndText;
    [SerializeField] private Image timeIsUpImage;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private ScreenFader screenFader;

    private float timeRemaining;
    private bool isRunning = true;
    private float currentScore = 0f;

    public static event Action OnTimerFinishedEvent;

    private void Start()
    {
        timeRemaining = timerDuration;
        scoreText.text = currentScore.ToString();
        timerText.text = timerDuration.ToString();
        gameEndText.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isRunning)
            return;

        timeRemaining -= Time.deltaTime;
        float rounded = Mathf.Round(timeRemaining);
        timerText.text = rounded.ToString();

        currentScore = player.GetCurrentScore();
        scoreText.text = currentScore.ToString();

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            StartCoroutine(OnTimerFinished());
        }
    }

    private IEnumerator OnTimerFinished()
    {
        OnTimerFinishedEvent.Invoke();

        player.DisableMovement();

        //scoreText.gameObject.SetActive(false);
        //timerText.gameObject.SetActive(false);
        gameEndText.text = currentScore.ToString();

        float fadeDuration = 2f;

        screenFader.FadeToBlack(fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        screenFader.FadeInTimeUpScreen(fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        screenFader.HideBlackScreen();
        timeIsUpImage.gameObject.SetActive(true);
        gameEndText.gameObject.SetActive(true);

        yield return new WaitForSeconds(2);

        restartGameButton.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void HideGameUI()
    {
        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
    }
}
