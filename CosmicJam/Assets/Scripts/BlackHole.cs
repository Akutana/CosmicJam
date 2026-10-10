using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private Collider2D deathZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (!deathZone.IsTouching(collision))
                return;

            collision.GetComponent<PlayerController>().Die();
        }
    }
}
