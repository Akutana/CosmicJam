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

        MakeVisible(false);

        enabled = false;

        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;

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

    public void MakeVisible(bool visible)
    {
        MakeObjectVisible(visible, transform);

        int children = transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            MakeObjectVisible(visible, transform.GetChild(i));
        }
    }

    private void MakeObjectVisible(bool visible, Transform obj)
    {
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();

        if (sprite == null)
            return;

        sprite.color = new Color(sprite.color.r,
                                 sprite.color.g,
                                 sprite.color.b,
                                 visible ? 1.0f : 0.0f);
    }
}
