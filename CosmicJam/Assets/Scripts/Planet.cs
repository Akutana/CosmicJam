using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    public void Destroy()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        rb.simulated = false;
    }
}
