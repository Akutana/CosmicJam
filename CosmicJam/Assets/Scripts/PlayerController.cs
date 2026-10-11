using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Death")]
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private ScreenFader screenFader;

    [Header("General")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private OrbitalZone orbitalZone;

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

        DisableMovement();

        GetComponentInChildren<LineRenderer>().enabled = false;

        gameManager.HideGameUI();

        int children = orbitalZone.transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform obj = orbitalZone.transform.GetChild(i);

            Destroy(obj.gameObject);
        }

        screenFader.FadeToBlack(fadeDuration);
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
}
