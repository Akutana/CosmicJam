using System.Collections.Generic;
using UnityEngine;

public class OrbitalZone : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;
    [SerializeField] private string planetTag = "";

    private void FixedUpdate()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform obj = transform.GetChild(i);

            obj.RotateAround(
                transform.position,
                Vector3.forward,
                -orbitSpeed * Time.deltaTime
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Transform obj = collision.gameObject.transform.parent;

        if (obj == null || !obj.CompareTag(planetTag))
            return;

        if (!obj.transform.IsChildOf(transform))
        {
            obj.transform.parent = transform;
        }
    }
}
