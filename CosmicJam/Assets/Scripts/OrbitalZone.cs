using UnityEngine;

public class OrbitalZone : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;

    private void Update()
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
        GameObject obj = collision.gameObject;

        if (!obj.transform.parent.transform.IsChildOf(transform))
        {
            obj.transform.parent.transform.parent = transform;
        }
    }
}
