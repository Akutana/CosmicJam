using UnityEngine;
using System.Collections;

public class asteroidMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject explosion;
    [SerializeField] private Collider2D childCollider;
    private Transform player;
    private bool isExploding;

        
    public void SetTarget(Transform target)
    {
        player = target;
    }

    private void Awake()
    {
        explosion.SetActive(false);
    }

    void Update()
    {
        if (player == null) return;
        Vector3 moveDirection = (player.position - gameObject.transform.position).normalized;
        // Debug.Log("player position: " + player.position);
        // Debug.Log("move direction: " + moveDirection);

        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        if (!isExploding)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, angle + 90);
            transform.position = transform.position + moveDirection * moveSpeed * Time.deltaTime;
        }
        
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
        }

        if (collision.CompareTag("Player"))
        {
            asteroidExplode();
            Debug.Log("exploded asteroid");
        }
    }

    public void asteroidExplode()
    {
        if (isExploding) return;
        isExploding = true;
        StartCoroutine(ExplodeAsteroid());
        Debug.Log("coroutine started");
    }

    IEnumerator ExplodeAsteroid()
    {
        explosion.SetActive(true);
        fire.SetActive(false);
        //disabling colliders
        Collider2D selfCollider = GetComponent<Collider2D>();
        childCollider.enabled = false;
        if (selfCollider != null)
        {
            selfCollider.enabled = false;
        }


        yield return new WaitForSeconds(0.15f);

        //making asteroid invisible
        SpriteRenderer selfSpite = gameObject.GetComponent<SpriteRenderer>();
        if (selfSpite != null)
        {
            selfSpite.enabled = false;
        }

        yield return new WaitForSeconds(0.70f);
        explosion.SetActive(false);
        Destroy(gameObject);
        Debug.Log("ast destroyed");
    }

    
}
