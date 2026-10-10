using UnityEngine;

public class Planet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Planet")
            && collision.GetComponentInParent<PlayerController>() != null)
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
