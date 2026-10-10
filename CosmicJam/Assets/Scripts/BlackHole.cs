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
            collision.GetComponent<PlayerController>().Die();
        }
        
        else if (collision.transform.parent.CompareTag("Planet"))
        {
            Debug.Log("planet");
            Destroy(collision.transform.parent.gameObject);
        }
    }
}
