using UnityEngine;

public class Wormhole : MonoBehaviour
{
    [SerializeField] private GameObject wormhole;

    Wormhole wormholeScript;
    private bool canTeleport = true;

    private void Awake()
    {
        wormholeScript = wormhole.GetComponent<Wormhole>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && wormholeScript.CanTeleport())
        {
            collision.transform.position = wormhole.transform.position;
            canTeleport = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            wormholeScript.SetCanTeleport(true);
        }
    }

    public bool CanTeleport()
    {
        return canTeleport;
    }

    public void SetCanTeleport(bool value)
    {
        canTeleport = value;
    }
}
