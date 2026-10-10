using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleOutline : MonoBehaviour
{
    [SerializeField] private float radius = 5f;
    [SerializeField] private int segments = 100;

    private void Awake()
    {
        LineRenderer line = GetComponent<LineRenderer>();

        line.useWorldSpace = false;
        line.loop = true;
        line.positionCount = segments;
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;

        for (int i = 0; i < segments; i++)
        {
            float angle = i * 2f * Mathf.PI / segments;

            line.SetPosition(i, new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f
            ));
        }
    }
}