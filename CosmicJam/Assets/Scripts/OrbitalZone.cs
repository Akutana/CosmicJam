using UnityEngine;

public class OrbitalZone : MonoBehaviour
{
    [SerializeField] private float orbitSpeed = 90f;
    [SerializeField] private float baseOrbitSpeed = 90f;
    [SerializeField] private float maxOrbitSpeed = 240f;
    [SerializeField] private string planetTag = "";

    private int currentScore = 0;

    private void FixedUpdate()
    {
        int children = transform.childCount;

        if (children == 0)
            return;

        float orbitSpeed = Mathf.Min(
            baseOrbitSpeed * Mathf.Sqrt(children),
            maxOrbitSpeed
        );
        Debug.Log(orbitSpeed);

        //float speedMultiplier = 1f + 0.3f * Mathf.Log(children);
        //float currentOrbitSpeed = orbitSpeed * speedMultiplier;
        //Debug.Log(currentOrbitSpeed);

        for (int i = 0; i < children; ++i)
        {
            Transform obj = transform.GetChild(i);

            Quaternion quat = transform.rotation;

            obj.RotateAround(
                transform.position,
                Vector3.forward,
                -orbitSpeed * Time.deltaTime
            );

            //Transform obj = transform.GetChild(i);
            //Quaternion quat = obj.rotation;

            //obj.RotateAround(
            //    transform.position,
            //    Vector3.forward,
            //    -currentOrbitSpeed * Time.fixedDeltaTime
            //);
            if (obj.gameObject.layer == LayerMask.NameToLayer("Spaceship"))
            {
                Vector3 radialDirection = obj.position - transform.position;
                Vector3 moveDirection =
                    Vector3.Cross(Vector3.forward, radialDirection).normalized
                    * Mathf.Sign(-orbitSpeed);

                float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
                obj.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
            else
            {
                obj.rotation = quat;
            }
            
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
