using UnityEngine;

public class AttractionZone : MonoBehaviour
{
    [SerializeField] private float attractionForce = 10f;
    [SerializeField] private Rigidbody2D player;

    private bool isApplyingForce = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player"))
        {
            isApplyingForce = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("Player"))
        {
            isApplyingForce = false;
        }
    }

    private void FixedUpdate()
    {
        if (!isApplyingForce)
            return;

        Vector2 direction = (Vector2)transform.position - player.position;

        float distance = direction.magnitude;
        float force = attractionForce / Mathf.Max(distance * distance, 0.25f);

        player.AddForce(direction.normalized * force);
    }
}
