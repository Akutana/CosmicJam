using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class WorldBorder : MonoBehaviour
{
    [SerializeField] private float radius = 20f;
    [SerializeField] private int segments = 128;

    private void Awake()
    {
        EdgeCollider2D edge = GetComponent<EdgeCollider2D>();
        Vector2[] points = new Vector2[segments + 1];

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2f * Mathf.PI / segments;

            points[i] = new Vector2(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius
            );
        }

        edge.points = points;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Vector3 previousPoint = transform.TransformPoint(
            new Vector3(radius, 0f, 0f)
        );

        for (int i = 1; i <= segments; i++)
        {
            float angle = i * 2f * Mathf.PI / segments;

            Vector3 currentPoint = transform.TransformPoint(
                new Vector3(
                    Mathf.Cos(angle) * radius,
                    Mathf.Sin(angle) * radius,
                    0f
                )
            );

            Gizmos.DrawLine(previousPoint, currentPoint);
            previousPoint = currentPoint;
        }
    }
}
