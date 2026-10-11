using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private Collider2D childCollider;

    private void Awake()
    {
        explosion.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Planet")
            && collision.GetComponentInParent<PlayerController>() != null)
        {
            StartCoroutine(ExplodePlanet(collision.gameObject, collision));
            Debug.Log(collision.gameObject.name);
            Debug.Log(gameObject.name);
        }
    }

    IEnumerator ExplodePlanet(GameObject collisionPlanet, Collider2D collider)
    {
        explosion.SetActive(true);
        collider.enabled = false;
        Collider2D selfCollider = GetComponent<Collider2D>();
        childCollider.enabled = false;
        if (selfCollider != null)
        {
            selfCollider.enabled = false;
        }


        yield return new WaitForSeconds(0.15f);
        
        SpriteRenderer planetSprite = collisionPlanet.GetComponent<SpriteRenderer>();
        if (planetSprite != null)
        {
            planetSprite.enabled = false;
        }
        SpriteRenderer selfSpite = gameObject.GetComponent<SpriteRenderer>();
        if (selfSpite != null)
        {
            selfSpite.enabled = false;
        }

        yield return new WaitForSeconds(0.70f);
        explosion.SetActive(false);
        Destroy(collisionPlanet);
        Destroy(gameObject);
    }
}
