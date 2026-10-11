using UnityEngine;

public class OrbitalZone : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;
    [SerializeField] private string planetTag = "";

    private int currentScore = 0;

    private void FixedUpdate()
    {
        int children = transform.childCount;

        for (int i = 0; i < children; ++i)
        {
            Transform obj = transform.GetChild(i);

            Quaternion quat = transform.rotation;

            obj.RotateAround(
                transform.position,
                Vector3.forward,
                -orbitSpeed * Time.deltaTime
            );

            obj.rotation = quat;
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

            currentScore += obj.GetComponent<Planet>().GetPointsValue();
        }
    }

    public void RemovePoints(int amount)
    {
        currentScore -= amount;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
