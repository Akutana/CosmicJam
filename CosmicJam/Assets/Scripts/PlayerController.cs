using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Death")]
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private Image flashbangImage;
    [SerializeField] private Image gameOverImage;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] private float gameOverDisplayDuration = 2f;
    [SerializeField] private float gameOverFadeDuration = 1.5f;

    [Header("General")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private OrbitalZone orbitalZone;


    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip soundEffect;

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

    public IEnumerator Die()
    {
        Debug.Log("Die: début");

        if (isDead)
            yield break;

        isDead = true;

        Debug.Log("Die: avant MakeVisible");
        MakeVisible(false);

        enabled = false;

        Debug.Log("Die: avant DisableMovement");
        DisableMovement();

        Debug.Log("Die: avant LineRenderer");
        GetComponentInChildren<LineRenderer>().enabled = false;

        Debug.Log("Die: avant GameManager");
        gameManager.HideGameUI();

        Debug.Log("Die: avant OrbitalZone");
        int children = orbitalZone.transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform obj = orbitalZone.transform.GetChild(i);

            Destroy(obj.gameObject);
        }

        screenFader.FadeToBlack(fadeDuration);

        yield return new WaitForSeconds(fadeDuration);

        audioSource.PlayOneShot(soundEffect);
        yield return new WaitForSeconds(0.15f);
        gameOverImage.gameObject.SetActive(true);
        flashbangImage.gameObject.SetActive(true);
         
        gameOverCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(2.5f);

        float elapsed = 0f;

        while (elapsed < gameOverFadeDuration)
        {
            elapsed += Time.deltaTime;

            gameOverCanvasGroup.alpha =
                Mathf.Lerp(1f, 0f, elapsed / gameOverFadeDuration);

            yield return null;
        }

        gameOverCanvasGroup.alpha = 0f;

        restartGameButton.gameObject.SetActive(true);
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

    public void DisableMovement()
    {
        movement = Vector2.zero;
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
    }

    public int GetCurrentScore()
    {
        return orbitalZone.GetCurrentScore();
    }
}
