using UnityEngine;
using System.Collections.Generic;

public class AttractionZone : MonoBehaviour
{
    [SerializeField] private float playerAttractionForce = 10f;
    [SerializeField] private float planetAttractionForce = 10f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerOrbitalZone;

    private List<GameObject> objectsToAttract = new List<GameObject>();
    private List<GameObject> destroyedObjects = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");

        if (collision.CompareTag("Player"))
        {
            objectsToAttract.Add(collision.gameObject);
        }

        else if (collision.transform.parent != null 
            && collision.transform.parent.CompareTag("Planet"))
        {
            Debug.Log("collision planet");
            Transform planet = collision.transform.parent;

            if (planet.transform.parent == playerOrbitalZone.transform
                && !planet.GetComponent<Planet>().GetIsExploding())
            {
                Debug.Log("collision planet not exploding");
                planet.transform.parent = null;

                playerOrbitalZone.GetComponent<OrbitalZone>().RemovePoints(
                    planet.GetComponent<Planet>().GetPointsValue()
                );

                objectsToAttract.Add(planet.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            objectsToAttract.Remove(collision.gameObject);
        }
    }

    private void FixedUpdate()
    {
        Debug.Log("FixedUpdate");
        if (objectsToAttract.Count == 0)
            return;
        Debug.Log("count not 0");
        foreach (GameObject obj in objectsToAttract)
        {
            AttractObject(obj);
        }

        foreach (GameObject obj in destroyedObjects)
        {
            objectsToAttract.Remove(obj);
        }

        destroyedObjects.Clear();
    }

    private void AttractObject(GameObject obj)
    {
        Debug.Log("attract");
        if (obj == null)
        {
            Debug.Log("null");
            destroyedObjects.Add(obj);
            return;
        }

        if (obj != player && obj.GetComponentInParent<PlayerController>() != null)
        {
            Debug.Log("ggjfhdsklghfjsdkl");
            destroyedObjects.Add(obj);
            return;
        }
        
        if (obj.GetComponent<Rigidbody2D>().bodyType == RigidbodyType2D.Dynamic)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();

            Vector2 direction = (Vector2)transform.position - rb.position;

            float distance = direction.magnitude;
            float force = playerAttractionForce / Mathf.Max(distance * distance, 0.25f);

            rb.AddForce(direction.normalized * force);
            Debug.Log("if 1");
        }

        else
        {
            Transform tr = obj.transform;

            Vector2 direction = (Vector2)transform.position - (Vector2)tr.position;
            float distance = direction.magnitude;

            if (distance < 0.0001f)
                return;

            float speed = planetAttractionForce / Mathf.Max(distance * distance, 0.25f);

            tr.position += (Vector3)(direction.normalized * speed * Time.fixedDeltaTime);

            Debug.Log("if 2");
        }
    }
}
