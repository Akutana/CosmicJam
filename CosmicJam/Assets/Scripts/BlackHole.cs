using UnityEngine;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private Collider2D deathZone;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!deathZone.IsTouching(collision))
            return;

        if (collision.CompareTag("Player"))
        {
            StartCoroutine(collision.GetComponent<PlayerController>().Die());
        }
        
        else if (collision.transform.parent != null
            && collision.transform.parent.CompareTag("Planet"))
        {
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
