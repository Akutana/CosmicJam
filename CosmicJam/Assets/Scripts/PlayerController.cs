using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Death")]
    [SerializeField] private CanvasGroup blackScreen;
    [SerializeField] private float fadeDuration = 2f;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool isDead = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = movement.normalized;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = movement * moveSpeed;
    }

    public void Die()
    {
        if (isDead)
            return;

        isDead = true;

        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;

        enabled = false;

        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.blocksRaycasts = true;

        float elapsed = 0f;
        blackScreen.alpha = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            blackScreen.alpha = Mathf.Clamp01(elapsed / fadeDuration);

            yield return null;
        }

        blackScreen.alpha = 1f;
    }
}
