using UnityEngine;

public class asteroidMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    private Transform player;
        
    public void SetTarget(Transform target)
    {
        player = target;
    }

    void Update()
    {
        if (player == null) return;
        Vector3 moveDirection = (player.position - gameObject.transform.position).normalized;
        // Debug.Log("player position: " + player.position);
        // Debug.Log("move direction: " + moveDirection);
        // transform.rotation = Quaternion.LookRotation(moveDirection);

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);

        transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Planet")
            && collision.GetComponentInParent<PlayerController>() != null)
        {
            Planet planet = collision.GetComponentInParent<Planet>();
            if (planet != null)
            {
                planet.Explode(gameObject, GetComponent<Collider2D>());
            }
            Debug.Log(collision.gameObject.name);
            Debug.Log(gameObject.name);
        }

        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            // Planet.Explode(gameObject, GetComponent<Collider2D>());
        }
    }

    
}
