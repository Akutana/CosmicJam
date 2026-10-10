using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float timerDuration = 100f;
    [SerializeField] private PlayerController player;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private TextMeshProUGUI gameEndText;
    [SerializeField] private Button restartGameButton;

    private float timeRemaining;
    private bool isRunning = true;
    private float score = 0f;

    private void Start()
    {
        timeRemaining = timerDuration;
        scoreText.text = "0";
        timerText.text = timerDuration.ToString();
        gameEndText.gameObject.SetActive(false);
        restartGameButton.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!isRunning)
            return;

        timeRemaining -= Time.deltaTime;
        float rounded = UnityEngine.Mathf.Round(timeRemaining);
        timerText.text = rounded.ToString();

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            isRunning = false;
            OnTimerFinished();
        }
    }

    private void OnTimerFinished()
    {
        player.DisableMovement();

        scoreText.gameObject.SetActive(false);
        timerText.gameObject.SetActive(false);
        gameEndText.gameObject.SetActive(true);
        gameEndText.text = "Your score: " + score;
        restartGameButton.gameObject.SetActive(true);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
}
